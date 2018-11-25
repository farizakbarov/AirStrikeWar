using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GlobalEngine.currentScene == 1)
            Application.LoadLevel("Sea");
        else if (GlobalEngine.currentScene == 2)
            Application.LoadLevel("Sea");
        else if (GlobalEngine.currentScene == 3)
            Application.LoadLevel("Sea");
        else if (GlobalEngine.currentScene == 4)
            Application.LoadLevel("Desert");
        else if (GlobalEngine.currentScene == 5)
            Application.LoadLevel("Desert");
        else if (GlobalEngine.currentScene == 6)
            Application.LoadLevel("Desert");
        else if (GlobalEngine.currentScene == 7)
            Application.LoadLevel("ArabianCity");
        else if (GlobalEngine.currentScene == 8)
            Application.LoadLevel("ArabianCity");
        else if (GlobalEngine.currentScene == 9)
            Application.LoadLevel("ArabianCity");
        else if (GlobalEngine.currentScene == 10)
            Application.LoadLevel("Snow");
        else if (GlobalEngine.currentScene == 11)
            Application.LoadLevel("Snow");
        else if (GlobalEngine.currentScene == 12)
            Application.LoadLevel("Snow");
      else if (GlobalEngine.currentScene == 13)
          Application.LoadLevel("DarkOcean");
        else if (GlobalEngine.currentScene == 14)
            Application.LoadLevel("DarkOcean");
        else if (GlobalEngine.currentScene == 15)
            Application.LoadLevel("DarkOcean");
        else if (GlobalEngine.currentScene == 16)
            Application.LoadLevel("Sea 1");
        else if (GlobalEngine.currentScene == 17)
            Application.LoadLevel("Sea 1");
        else if (GlobalEngine.currentScene == 18)
            Application.LoadLevel("Sea 1");
        else if (GlobalEngine.currentScene == 19)
            Application.LoadLevel("Desert 1");
        else if (GlobalEngine.currentScene == 20)
            Application.LoadLevel("Desert 1");
        else if (GlobalEngine.currentScene == 21)
            Application.LoadLevel("Desert 1");
        else if (GlobalEngine.currentScene == 22)
            Application.LoadLevel("Sea 2");
        else if (GlobalEngine.currentScene == 23)
            Application.LoadLevel("Sea 2");
        else if (GlobalEngine.currentScene == 24)
            Application.LoadLevel("Sea 2");
        else if (GlobalEngine.currentScene == 25)
            Application.LoadLevel("Snow 1");
        else if (GlobalEngine.currentScene == 26)
            Application.LoadLevel("Snow 1");
        else if (GlobalEngine.currentScene == 27)
            Application.LoadLevel("Snow 1");
        else if (GlobalEngine.currentScene == 28)
            Application.LoadLevel("DarkOcean 1");
        else if (GlobalEngine.currentScene == 29)
            Application.LoadLevel("DarkOcean 1");
        else if (GlobalEngine.currentScene == 30)
            Application.LoadLevel("DarkOcean 1");
 
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
