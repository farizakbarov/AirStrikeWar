using UnityEngine;
using System.Collections;

public class HittedGUI : MonoBehaviour {
	public GUITexture[] hittedGUI;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		for (int i=0; i<hittedGUI.Length; i++) {
			if(hittedGUI[i].color.a > 0)
				hittedGUI[i].color = new Color(0.5f, 0.5f, 0.5f, hittedGUI[i].color.a - Time.deltaTime * 0.2f);
		}
	}

	public void HittedMissile(){
		for (int i=0; i<hittedGUI.Length; i++) {
			hittedGUI[i].color = new Color(0.5f, 0.5f,0.5f,0.5f);		
		}
	}

	public void HittedBullet(){
		for (int i=0; i<2; i++) {
			hittedGUI[Random.Range(0,5)].color = new Color(0.5f, 0.5f,0.5f,0.5f);		
		}
	}
}
