using UnityEngine;
using System.Collections;

public class exitMission : MonoBehaviour {
	public GUITexture[] disableGUIs;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && GetComponent<GUITexture>().pixelInset.Contains (new Vector2(Input.mousePosition.x, Input.mousePosition.y))) {
			if(Time.timeScale != 0){
				GameObject.Find("UI Root").SendMessage("PressedExit1");
//				GameObject.Find("UI Root/Camera/Exit Game1").GetComponent<SpringPanel>().target = new Vector3(0,0,0);		
//				GameObject.Find("UI Root/Camera/Exit Game1").GetComponent<SpringPanel>().enabled = true;
//				GameObject.Find ("Player").GetComponent<Indicator> ().enabled = false;
//				Time.timeScale = 0;
//				GameObject.Find("Player").GetComponent<AudioSource>().Pause();
//				GameObject.Find("Music Audio").GetComponent<AudioSource>().Pause();
//
//				GameObject.Find("_Touch Controller").GetComponent<TouchController>().enabled = false;
//				foreach(GUITexture g in disableGUIs){
//					g.enabled = false;
//				}
			}		
		}
	}
}
