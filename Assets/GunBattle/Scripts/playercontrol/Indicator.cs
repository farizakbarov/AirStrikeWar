using UnityEngine;
using System.Collections;

public class Indicator : MonoBehaviour {
	public Texture Crosshair;
	private int cxx, cyy;
	[HideInInspector]
	public Vector2 focus;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.DrawTexture (new Rect ((Screen.width / 2 - Crosshair.width / 2) + cxx, (Screen.height / 2 - Crosshair.height ) + cyy, Screen.height/15f,Screen.height/15f), Crosshair);
		focus = new Vector2 (Screen.width/2 - Crosshair.width/2 + cxx + Screen.height/30f, Screen.height/2+Crosshair.height-cyy-Screen.height/30f);
	}
}
