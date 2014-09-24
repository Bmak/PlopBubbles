using UnityEngine;
using System.Collections;

public class TextureControl : MonoBehaviour {

	#region SINGLETON
	private static TextureControl _i;
	public static TextureControl I
	{
		get
		{
			if (_i == null) _i = FindObjectOfType(typeof(TextureControl)) as TextureControl;
			return _i;
		}
	}
	private void OnApplicationQuit () { _i = null; }
	#endregion

	public Texture2D[] currentSet;		//current set of textures
	
	private void Awake () 
	{
		currentSet = new Texture2D[4];

		GenerateTextures ();
	}

	//generate new set
	public void GenerateTextures ()
	{
		Texture2D[] tempSet = new Texture2D[4];

		Color rndC = new Color (Random.value, Random.value, Random.value, 1);
		tempSet [0] = DrawCircleTexture(32, rndC, true);
		tempSet [1] = DrawCircleTexture(64, rndC, true);
		tempSet [2] = DrawCircleTexture(128, rndC, true);
		tempSet [3] = DrawCircleTexture(256, rndC, true);

		currentSet = tempSet;
		tempSet = null;
	}

	//draw
	private Texture2D DrawCircleTexture (int size, Color32 color, bool gradient)
	{
		Texture2D texture = new Texture2D(size, size, TextureFormat.ARGB32, false);
		
		int r = size / 2; // radius
		int ox = size / 2, oy = size / 2;
		
		for (int y = -r; y <= r; y++)
		{
			for (int x = -r; x <= r; x++)
			{
				if (x * x + y * y <= r * r) texture.SetPixel(ox + x, oy + y, new Color32(color.r, color.g, color.b, gradient ? (byte)(255 - x*x) : (byte)255));
				else texture.SetPixel(ox + x, oy + y, new Color32(0, 0, 0, 0));
			}
		}
		
		texture.Apply();
		
		return texture;
	}

}
