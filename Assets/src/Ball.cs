using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Ball : MonoBehaviour {

	public Text tf;

	int points = 10;
	float speed;
	bool isDead;

	void Start () {
		//tf = GameObject.FindGameObjectWithTag("Points").GetComponent("Text");


		float scale = (float) Random.Range (3, 12)/10;
		transform.localScale = new Vector2 (scale, scale);
		gameObject.renderer.material.color = new Color(Random.value, Random.value, Random.value, 1);
		gameObject.renderer.sortingLayerID = 0;


		isDead = false;
		speed = (float) Random.Range (1,5)/100;
		speed /= scale;

		points *= (int)scale;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isDead) {
			Vector3 pos = transform.position;
			pos.y -= speed;
			if (pos.y <= 0) {
				isDead = true;
			}
			transform.position = pos;
		} else {
			Destroy(gameObject);
		}
	}

	void OnMouseDown() {
		//TODO add points
		//int newPoints = int.Parse(tf.text) + points;
		//tf.text = newPoints.ToString ();
		isDead = true;
	}

	void Remove() {

	}
}
