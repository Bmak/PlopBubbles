using UnityEngine;
using System;
using System.Collections;

public class CreateWorld : MonoBehaviour {



	// Use this for initialization
	void Start () {
		Caching.CleanCache();

		StartCoroutine (DownloadAndCache());
	}

	IEnumerator DownloadAndCache (){
		// Wait for the Caching system to be ready
		while (!Caching.ready)
			yield return null;

		string path = "file://" + Application.dataPath + "/PlopBubbles.unity3d";
		// Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
		using(WWW www = WWW.LoadFromCacheOrDownload (path, 1)){
			yield return www;
			if (www.error != null)
				throw new Exception("WWW download had an error:" + www.error);
			AssetBundle bundle = www.assetBundle;


			GameObject bkg = bundle.Load ("bkg", typeof(GameObject)) as GameObject;
			GameObject ui = bundle.Load ("ui", typeof(GameObject)) as GameObject;
			
			GameObject background = Instantiate (bkg) as GameObject;
			Vector3 pos = transform.position;
			pos.z = 1;
			background.transform.position = pos;
			background.transform.localScale = new Vector2(2,1);

			GameObject uiobj = Instantiate (ui) as GameObject;
			uiobj.transform.position = new Vector3(0,0,0);


			background.AddComponent("GameControl");
			GameControl gameC = background.GetComponent<GameControl>();
			gameC.minTimeBetweenSpawns = 0.3f;
			gameC.maxTimeBetweenSpawns = 2;
			gameC.pref = bundle.Load ("ball", typeof(GameObject)) as GameObject;
			gameC.InitGame();

			// Unload the AssetBundles compressed contents to conserve memory
			bundle.Unload(false);
		} // memory is freed from the web stream (www.Dispose() gets called implicitly)
	}
	
	
	// Update is called once per frame
	void Update () {
		
	}
}
