using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour
{
	#region SINGLETON
	private static GameUI _i;
	public static GameUI I
	{
		get
		{
			if (_i == null) _i = FindObjectOfType(typeof(GameUI)) as GameUI;
			return _i;
		}
	}
	private void OnApplicationQuit () { _i = null; }
	#endregion

	public int Points;

	private void OnGUI ()
	{
		// loading scene
		if (Application.loadedLevel == 0)
		{
			GUI.Box(new Rect(Screen.width / 2 - 75, Screen.height / 2, 150, 30), "Loading...");
		}
		// game scene
		else
		{
			GUI.TextField(new Rect(0, Screen.height - 60, 100, 20), "Points: " + Points);
			GUI.TextField(new Rect(0, Screen.height - 30, 100, 20), "Timer: " + Time.timeSinceLevelLoad.ToString("F1"));
		}
	}
}