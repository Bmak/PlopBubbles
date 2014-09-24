using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

	public static float SKILL_K;		//Коеффициент усложнения игры
	public static float DELTA_K;		//Дельта изменения сложности
	public static float POINTS_COUNTER = 0;	//Счетчик увеличения очков, на основе которого будет усложняться игра


	public float skillK;
	public float deltaK;
	public float minTimeBetweenSpawns;		//Минимальное значение между созданием нового объекта
	public float maxTimeBetweenSpawns;		//Максимальное значение между созданием нового объекта
	float _posY;							//Точка появления шара по Y
	float _leftSpawnPosX;					//Левая граница появления шара по X
	float _rightSpawnPosX;					//Правая граница появления шара по X
	GameObject _ball;						//Префаб игрового объекта

	// Use this for initialization
	void Start () {
		SKILL_K = skillK;
		DELTA_K = deltaK;

		_ball = LoadAssets.ballPrefab;

		// Set the random seed so it's not the same each game.
		Random.seed = System.DateTime.Today.Millisecond;
		Bounds screenBounds = LoadAssets.bkgPrefab.transform.renderer.bounds;
		Bounds ballBounds = _ball.renderer.bounds;
		_posY = screenBounds.max.y + ballBounds.size.y;
		_leftSpawnPosX = screenBounds.min.x + ballBounds.size.x / 2;
		_rightSpawnPosX = screenBounds.max.x - ballBounds.size.x / 2;
		
		//Стартуем повторяющееся событие
		StartCoroutine("AddBall");
	}

	IEnumerator AddBall() {
		// Create a random wait time before the prop is instantiated.
		float waitTime = Random.Range(minTimeBetweenSpawns/SKILL_K, maxTimeBetweenSpawns/SKILL_K);
		
		// Wait for the designated period.
		yield return new WaitForSeconds(waitTime);


		float posX = Random.Range (_leftSpawnPosX, _rightSpawnPosX);
		Instantiate (_ball, new Vector3 (posX, _posY, 0), Quaternion.identity);

		StartCoroutine(AddBall());
	}
}
