using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {

	// Play the music one time
	void Start () {
		AudioSource.PlayClipAtPoint(LoadAssets.themeMusic,new Vector3(0,0,0));
	}
	
	// Update is called once per frame
	void Update () {
	}
}
