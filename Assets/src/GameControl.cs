using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public static float SKILL_K = 1;		//Коеффициент усложнения игры
	public static float POINTS_COUNTER = 0;	//Счетчик увеличения очков, на основе которого будет усложняться игра
	
	public GameObject pref;					//Префаб игрового объекта
	public float minTimeBetweenSpawns;		//Минимальное значение между созданием нового объекта
	public float maxTimeBetweenSpawns;		//Максимальное значение между созданием нового объекта
	float _posY;							//Точка появления шара по Y
	float _leftSpawnPosX;					//Левая граница появления шара по X
	float _rightSpawnPosX;					//Правая граница появления шара по X

	// Use this for initialization
	void Start () {
		
	}

	public void InitGame() {
		// Set the random seed so it's not the same each game.
		Random.seed = System.DateTime.Today.Millisecond;
		Bounds screenBounds = transform.renderer.bounds;
		Bounds ballBounds = pref.renderer.bounds;
		_posY = screenBounds.max.y + ballBounds.size.y;
		_leftSpawnPosX = screenBounds.min.x + ballBounds.size.x / 2;
		_rightSpawnPosX = screenBounds.max.x - ballBounds.size.x / 2;
		
		//Стартуем повторяющееся событие
		StartCoroutine("AddBall");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//добавляем шар через определенный случайный промежуток времени
	IEnumerator AddBall() {
		// Create a random wait time before the prop is instantiated.
		float waitTime = Random.Range(minTimeBetweenSpawns/SKILL_K, maxTimeBetweenSpawns/SKILL_K);
		
		// Wait for the designated period.
		yield return new WaitForSeconds(waitTime);


		float posX = Random.Range (_leftSpawnPosX, _rightSpawnPosX);
		GameObject ball = Instantiate (pref, new Vector3 (posX, _posY, 0), Quaternion.identity) as GameObject;
		ball.AddComponent ("Ball");

		StartCoroutine(AddBall());
	}
}
