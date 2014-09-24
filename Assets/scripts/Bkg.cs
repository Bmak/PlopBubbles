using UnityEngine;
using System.Collections;

public class Bkg : MonoBehaviour {

	void Awake() {
		DontDestroyOnLoad(gameObject);

		GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
		transform.localScale = new Vector3(2, 1, 1);
		Vector3 pos = camera.transform.position;
		transform.position = new Vector3 (pos.x, pos.y, 10);
	}

}
