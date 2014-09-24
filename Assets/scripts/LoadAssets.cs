using UnityEngine;
using System;
using System.Collections;

public class LoadAssets : MonoBehaviour {

	public static GameObject bkgPrefab;			//background prefab
	public static GameObject ballPrefab;		//ball prefab
	public static GameObject particlesPrefab;	//particle effect prefab
	public static AudioClip themeMusic;			//background music
	
	
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		
		StartCoroutine (LoadGame ());
	}
	
	IEnumerator LoadGame () {
		string path = "file://" + Application.dataPath + "/bundle/PlopBubbles.unity3d";

		using (WWW www = new WWW(path)) {
			yield return www;
			if (www.error != null) {
				throw new Exception ("WWW download had an error:" + www.error);
			}

			// load and retrieve the AssetBundle
			AssetBundle bundle = www.assetBundle;

			// load the objects asynchronously
			AssetBundleRequest request = bundle.LoadAsync ("bkg", typeof(GameObject));
			yield return request;
			bkgPrefab = Instantiate(request.asset) as GameObject;

			request = bundle.LoadAsync ("ball", typeof(GameObject));
			yield return request;
			ballPrefab = request.asset as GameObject;

			request = bundle.LoadAsync ("particles", typeof(GameObject));
			yield return request;
			particlesPrefab = request.asset as GameObject;

			request = bundle.LoadAsync ("theme", typeof(AudioClip));
			yield return request;
			themeMusic = request.asset as AudioClip;

			// Unload the AssetBundles compressed contents to conserve memory
			bundle.Unload (false);

			Application.LoadLevel (1);
		} 
	}

}
