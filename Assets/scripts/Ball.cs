using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ball : MonoBehaviour {
	
	float _points = 100;		//default points
	float _speed;				//ball speed
	bool _isDead = false;		//ball state
	float _skill;				//game skill coeff
	float _destroyPos;			//position for destroy object
	
	void Start () {
		_skill = GameControl.SKILL_K;
		
		float scale = (float) Random.Range (3, 15)/10;

		//set dynamic texture
		SpriteRenderer spr = renderer as SpriteRenderer;
		float prevTexWidth = spr.sprite.texture.width;
		Texture2D texture = TextureControl.I.currentSet[scale > 1 ? 3 : scale > .8f ? 2 : scale > .5f ? 1 : 0];
		spr.sprite = Sprite.Create(texture, new Rect(0, 0, 256, 256), new Vector2(.5f, .5f));
		((CircleCollider2D)collider2D).radius *= spr.sprite.textureRect.width / prevTexWidth;

		//transform.localScale = new Vector2 (scale, scale);
		//renderer.material.color = new Color(Random.value, Random.value, Random.value, 1);

		Bounds bounds = renderer.bounds;
		_destroyPos = -bounds.size.y * scale/2;
		
		_speed = (float) Random.Range (2*_skill,5*_skill)/100;
		_speed /= scale;
		
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

	//Add points, update state
	void OnMouseDown() {
		GameUI.I.Points += (int) _points;

		int counter = GameUI.I.Points / 500;
		if (counter > GameControl.POINTS_COUNTER) {
			GameControl.POINTS_COUNTER = counter;
			GameControl.SKILL_K += GameControl.DELTA_K;
			TextureControl.I.GenerateTextures();
			Debug.Log("NEW SKILL " + GameControl.SKILL_K);
		}

		//add particle effect
		Destroy (Instantiate (LoadAssets.particlesPrefab, transform.position, Quaternion.identity),0.5f);

		_isDead = true;
	}

}

