using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace AssemblyCSharp {

	public class Ball : MonoBehaviour {
		
		public Text tf;
		
		float _points = 100;
		float _speed;
		bool _isDead = false;
		float _skill;
		float _destroyPos;
		
		void Start () {
			tf = GameObject.FindGameObjectWithTag("Points").GetComponent<Text>();
			_skill = GameControl.SKILL_K;


			
			float scale = (float) Random.Range (3, 12)/10;
			transform.localScale = new Vector2 (scale, scale);
			gameObject.renderer.material.color = new Color(Random.value, Random.value, Random.value, 1);

			Bounds bounds = renderer.bounds;
			_destroyPos = -bounds.size.y * scale/2;
			
			_speed = (float) Random.Range (1*_skill,3*_skill)/100;
			_speed /= scale;
			Debug.Log ("SPEED " + _speed);
			
			_points = 150 - _points*scale;
		}
		
		// Update is called once per frame
		void Update () {
			if (!_isDead) {
				Vector3 pos = transform.position;
				pos.y -= _speed;
				if (pos.y <= _destroyPos) {
					_isDead = true;
				}
				transform.position = pos;
			} else {
				Destroy(gameObject);
			}
		}

		//Добавляем очки при клике и меняем состояние
		void OnMouseDown() {
			float newPoints = int.Parse(tf.text) + _points;
			tf.text = newPoints.ToString();

			int counter = (int)newPoints / 500;
			if (counter > GameControl.POINTS_COUNTER) {
				GameControl.POINTS_COUNTER = counter;
				GameControl.SKILL_K += 0.1f;
				Debug.Log("SKILL " + GameControl.SKILL_K);
			}

			_isDead = true;
		}

	}

}

