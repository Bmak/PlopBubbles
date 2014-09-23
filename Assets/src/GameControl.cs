using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {
	
	public GameObject pref;					//Префаб игрового объекта
	public float skillK = 1;				//Коеффициент усложнения игры
	public float minTimeBetweenSpawns;		//Минимальное значение между созданием нового объекта
	public float maxTimeBetweenSpawns;		//Максимальное значение между созданием нового объекта
	float _posY;
	float _leftSpawnPosX;					// The x coordinate of position if it's instantiated on the left.
	float _rightSpawnPosX;					// The x coordinate of position if it's instantiated on the right.

	// Use this for initialization
	void Start () {
		// Set the random seed so it's not the same each game.
		Random.seed = System.DateTime.Today.Millisecond;
		//TODO get size by script
		Bounds screenBounds = transform.renderer.bounds;
		Bounds ballBounds = pref.renderer.bounds;
		_posY = screenBounds.max.y + ballBounds.size.y;
		_leftSpawnPosX = screenBounds.min.x + ballBounds.size.x / 2;
		_rightSpawnPosX = screenBounds.max.x - ballBounds.size.x / 2;
		
		// Start the Spawn coroutine.
		StartCoroutine("AddBall");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator AddBall() {
		// Create a random wait time before the prop is instantiated.
		float waitTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
		
		// Wait for the designated period.
		yield return new WaitForSeconds(waitTime);

		float posX = Random.Range (_leftSpawnPosX, _rightSpawnPosX);
		Instantiate (pref, new Vector3 (posX, _posY, 0), Quaternion.identity);


		StartCoroutine(AddBall());
	}
}
