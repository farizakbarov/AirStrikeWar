using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class AllGUI : MonoBehaviour {
    public GameObject objpanel;
    public GameObject exitpanel;
    public static AllGUI instance;
    public GameObject player;
    public GameObject guibuttons;
    public GameObject healthbar;
    public GameObject subcontrolss;
    public GameObject touchablecontrol;
    public GameObject pausebutton;
    public GameObject pausepanel;
    public GameObject leevelcomplete;
    public GameObject leevelfailed;
    public GameObject soundon;
    public GameObject soundoff;
    //public GameObject musicon;
    //public GameObject musicoff;
    public static bool unlockimg2=false; 
    public GameObject img1, img2, img3, img4, img5, img6, img7, img8, img9, img10;
    public GameObject menupanel;
    public GameObject Level1mission;
    public GameObject Level2mission;
    public GameObject Level3mission;
    public GameObject Level4mission;
    public GameObject Level5mission;
    public GameObject Level6mission;
    public GameObject Level7mission;
    public GameObject Level8mission;
    public GameObject Level9mission;
    public GameObject Level10mission;
    public GameObject levelselectpanel;
	private GameObject auidoListen;
	public GameObject loadingSequence;
    public GameObject missionbreifing10easy;
    public GameObject missionbreifing10medium;
    public GameObject missionbreifing10hard;
    public GameObject missionbreifing9easy;
    public GameObject missionbreifing9medium;
    public GameObject missionbreifing9hard;
    public GameObject missionbreifing8easy;
    public GameObject missionbreifing8hard;
    public GameObject missionbreifing8medium;
    public GameObject missionbreifing7easy;
    public GameObject missionbreifing7medium;
    public GameObject missionbreifing7hard;
    public GameObject missionbreifing6Easy;
    public GameObject missionbreifing6Medium;
    public GameObject missionbreifing6hard;
	public enum GameState {MainMenu, Exit, LevelSelect, InGame, Pause, Fail, Complete, SequenceScreen, MissionBriefing, PreState};
	public GameState currentState;
 //   public GameObject Mission2panel;
	// Use this for initialization
    public void objfuntion() {
        objpanel.SetActive(false);
        Time.timeScale = 1;
    
    }
	void Start () {
        Time.timeScale = 0;
        instance = this;
//		PlayerPrefs.DeleteAll ();
//		PlayerPrefs.SetInt ("MaxLevel", 3);
		auidoListen = GameObject.Find ("Music Audio").gameObject;
		if(Application.loadedLevelName == "main menu"){

			currentState = GameState.MainMenu;

            if (PlayerPrefs.GetInt("Sound", 1) == 1)
            {
                GlobalEngine.soundEnable = true;
                soundon.SetActive(true);
                soundoff.SetActive(false);
                	auidoListen.transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                GlobalEngine.soundEnable = false;
                soundoff.SetActive(true);
                soundon.SetActive(false);
                	auidoListen.transform.localPosition = new Vector3(0, -10000, 0);
            }
			
            //if (PlayerPrefs.GetInt ("Music", 1) == 1){
            //    GlobalEngine.musicEnable = true;
              
            //    auidoListen.audio.enabled = true;
            //}
            //else{
             
            //    GlobalEngine.musicEnable = false;
            //    auidoListen.audio.enabled = false;
            //}

            GlobalEngine.maxLevel = PlayerPrefs.GetInt("MaxLevel", 1);

            if (GlobalEngine.maxLevel >= 2)
            {
                img1.SetActive(false);
                img2.SetActive(false);
                img3.SetActive(true);
                img4.SetActive(true);
                img5.SetActive(true);
                img6.SetActive(true);
                img7.SetActive(true);
                img8.SetActive(true);
                img9.SetActive(true);
                img10.SetActive(true);
   
            }
            if (GlobalEngine.maxLevel >= 5)
            {
                img2.SetActive(false);
                img3.SetActive(false);
                img4.SetActive(true);
                img5.SetActive(true);
                img6.SetActive(true);
                img7.SetActive(true);
                img8.SetActive(true);
                img9.SetActive(true);
                img10.SetActive(true);
           
            }
            if (GlobalEngine.maxLevel >= 8)
            {
                img2.SetActive(false);
                img3.SetActive(false);
                img4.SetActive(false);
                // img5.SetActive(true);
                img6.SetActive(true);
                img7.SetActive(true);
                img8.SetActive(true);
                img9.SetActive(true);
                img10.SetActive(true);
               
            }
            if (GlobalEngine.maxLevel >= 11)
            {
                img2.SetActive(false);
                img3.SetActive(false);
                img4.SetActive(false);
                img5.SetActive(false);
                //  img6.SetActive(true);
                img7.SetActive(true);
                img8.SetActive(true);
                img9.SetActive(true);
                img10.SetActive(true);
            
            }
            if (GlobalEngine.maxLevel >= 14)
            {
                img2.SetActive(false);
                img3.SetActive(false);
                img4.SetActive(false);
                img5.SetActive(false);
                img6.SetActive(false);
                img7.SetActive(true);
                img8.SetActive(true);
                img9.SetActive(true);
                img10.SetActive(true);
            
            }
            if (GlobalEngine.maxLevel >= 17)
            {
                img2.SetActive(false);
                img3.SetActive(false);
                img4.SetActive(false);
                img5.SetActive(false);
                img6.SetActive(false);
                img7.SetActive(false);
                img8.SetActive(true);
                img9.SetActive(true);
                img10.SetActive(true);
          
            }
            if (GlobalEngine.maxLevel >= 20)
            {
                img2.SetActive(false);
                img3.SetActive(false);
                img4.SetActive(false);
                img5.SetActive(false);
                img6.SetActive(false);
                img7.SetActive(false);
                img8.SetActive(false);
                img9.SetActive(true);
                img10.SetActive(true);
               
            }
            if (GlobalEngine.maxLevel >= 23)
            {
                img2.SetActive(false);
                img3.SetActive(false);
                img4.SetActive(false);
                img5.SetActive(false);
                img6.SetActive(false);
                img7.SetActive(false);
                img8.SetActive(false);
                img9.SetActive(false);
               
            }
            if (GlobalEngine.maxLevel >= 26)
            {
                img2.SetActive(false);
                img3.SetActive(false);
                img4.SetActive(false);
                img5.SetActive(false);
                img6.SetActive(false);
                img7.SetActive(false);
                img8.SetActive(false);
                img9.SetActive(false);
                img10.SetActive(false);
              
            }
			if(GlobalEngine.soundEnable){
		
			}
			else{
			
			}

			if(GlobalEngine.musicEnable){
			
			}
			else{
			
			}
		}
		else{
			currentState = GameState.InGame;

			if (GlobalEngine.soundEnable){
				auidoListen.transform.localPosition = new Vector3(0, 0, 0);
                soundon.SetActive(true);
                soundoff.SetActive(false);
			}
			else{
				auidoListen.transform.localPosition = new Vector3(0, -10000, 0);
                soundoff.SetActive(true);
                soundon.SetActive(true);
			}
			
			if (GlobalEngine.musicEnable){
				auidoListen.GetComponent<AudioSource>().mute = false;
              
			}
			else{
				auidoListen.GetComponent<AudioSource>().mute = true;
               
			}
		}
	}
    public void playeroff() {
     //   player.SetActive(false);
    
    }
    public void mission2setup() {
        Level2mission.SetActive(false);
    
    }
    public void pausesetup() {
        Time.timeScale = 0;
        pausebutton.SetActive(false);
        pausepanel.SetActive(true);
        touchablecontrol.SetActive(false);
        subcontrolss.SetActive(false);
        healthbar.SetActive(false);
        guibuttons.SetActive(false);
    }
	void Update () {
       
		if (Input.GetKeyUp (KeyCode.Escape)) {
			switch (currentState) {
			case GameState.Complete:
				break;
			case GameState.Exit:
				currentState = GameState.MainMenu;
				PressedNoExitGame();
				break;
			case GameState.Fail:
				currentState = GameState.MainMenu;
				PressedMenuLevelFailed();
				break;
			case GameState.InGame:
				currentState = GameState.Pause;
				PressedPause();
				break;
			case GameState.LevelSelect:
				currentState = GameState.MainMenu;
				PressedBackSelectLevel();
				break;
			case GameState.MissionBriefing:
				//				GameObject.Find("BriefingObjs").SendMessage("SkipIntro");
				//				currentState = GameState.InGame;
				currentState = GameState.LevelSelect;
				PressedBackMissionBriefing1();
				break;
			case GameState.MainMenu:
				currentState = GameState.Exit;
				PressedExitMainMenu();
				break;
			case GameState.Pause:
				currentState = GameState.InGame;
				PressedResumePause ();
				break;
			}
		}
	}

	public void PressedExit1(){
		GameObject.Find("UI Root/Camera/Exit Game1").GetComponent<SpringPanel>().target = new Vector3(0,0,0);		
		GameObject.Find("UI Root/Camera/Exit Game1").GetComponent<SpringPanel>().enabled = true;
		GameObject.Find ("Player").GetComponent<Indicator> ().enabled = false;
		Time.timeScale = 0;
		GameObject.Find("Player").GetComponent<AudioSource>().Pause();
		GameObject.Find("Music Audio").GetComponent<AudioSource>().Pause();
		
		GameObject.Find("_Touch Controller").GetComponent<TouchController>().enabled = false;
		GameObject.Find ("GUI/pause").GetComponent<GUITexture> ().enabled = false;
	}

	public void PressedPause(){
		GameObject.Find("UI Root/Camera/Pause").GetComponent<SpringPanel>().target = new Vector3(0,0,0);		
		GameObject.Find("UI Root/Camera/Pause").GetComponent<SpringPanel>().enabled = true;
		GameObject.Find ("Player").GetComponent<Indicator> ().enabled = false;
		Time.timeScale = 0;
		GameObject.Find("Player").GetComponent<AudioSource>().Pause();
		GameObject.Find("Music Audio").GetComponent<AudioSource>().Pause();
		
		GameObject.Find("_Touch Controller").GetComponent<TouchController>().enabled = false;

		GameObject.Find ("GUI/pause").GetComponent<GUITexture> ().enabled = false;

		GameObject.Find("UI Root").GetComponent<AllGUI>().currentState = AllGUI.GameState.Pause;
	}

	public void SettingSound(){
		GlobalEngine.soundEnable = !GlobalEngine.soundEnable;
		if(GlobalEngine.soundEnable){
			PlayerPrefs.SetInt("Sound",1);
            soundon.SetActive(true);
            soundoff.SetActive(false);
		}
		else{
           
			PlayerPrefs.SetInt("Sound", 0);

            //soundon.SetActive(false);
            //soundoff.SetActive(true);
           
		}
	}

	public void SettingMusic(){
		GlobalEngine.musicEnable = !GlobalEngine.musicEnable;
		if(GlobalEngine.musicEnable){
           
		//	GameObject.Find("MainMenuBG/Music").GetComponent<UISprite>().spriteName = "btn_music_normal";
//GameObject.Find("MainMenuBG/Music").GetComponent<UIButton>().normalSprite = "btn_music_normal";
			PlayerPrefs.SetInt ("Music", 1);
     
			auidoListen.GetComponent<AudioSource>().enabled = true;
            
		}
		else{
		//	GameObject.Find("MainMenuBG/Music").GetComponent<UISprite>().spriteName = "btn_music_pressed";
		//	GameObject.Find("MainMenuBG/Music").GetComponent<UIButton>().normalSprite = "btn_music_pressed";
			PlayerPrefs.SetInt("Music", 0);
			auidoListen.GetComponent<AudioSource>().enabled = false;
     
		}
	}

	public void PressedMore(){
		Debug.Log ("More button Pressed");
		Application.OpenURL ("");
	}

	public void PressedRate(){
		Debug.Log ("Rate button Pressed");
		Application.OpenURL ("");
	}

	public void PressedExitMainMenu(){
		currentState = GameState.Exit;
     //   menupanel.SetActive(false);
        exitpanel.SetActive(true);

	//	GameObject.Find ("MainMenu").GetComponent<SpringPanel> ().target = new Vector3 (-2000,0,0);
	//	GameObject.Find ("MainMenu").GetComponent<SpringPanel> ().enabled = true;	
	//	GameObject.Find ("Exit Game").GetComponent<SpringPanel> ().target = new Vector3 (0,0,0);
	//	GameObject.Find ("Exit Game").GetComponent<SpringPanel> ().enabled = true;	
        //#if UNITY_ANDROID && !UNITY_EDITOR
        //GSSdk.Instance.ShowSequence (5);
        //#endif
	}
    public void pressedbackMainmenu() {
        levelselectpanel.SetActive(false);
        menupanel.SetActive(true);
        SceneManager.LoadScene("main menu");
    }
	public void PressedPlayMainMenu(){
        menupanel.SetActive(false);
        levelselectpanel.SetActive(true);
		currentState = GameState.SequenceScreen;
		StartCoroutine ("CallSelectLevelFunc", 10);
    }

	IEnumerator CallSelectLevelFunc(float delayTime){
		loadingSequence.SetActive (true);
		yield return new WaitForSeconds (delayTime);
		loadingSequence.SetActive(false);
		currentState = GameState.LevelSelect;
		GameObject.Find ("SelectLevel").GetComponent<SpringPanel> ().target = new Vector3 (0,0,0);
		GameObject.Find ("SelectLevel").GetComponent<SpringPanel> ().enabled = true;
	}

	public void PressedBackSelectLevel(){
		currentState = GameState.MainMenu;
		GameObject.Find ("SelectLevel").GetComponent<SpringPanel> ().target = new Vector3 (2000,0,0);
		GameObject.Find ("SelectLevel").GetComponent<SpringPanel> ().enabled = true;
		GameObject.Find ("MainMenu").GetComponent<SpringPanel> ().target = new Vector3 (0,0,0);
		GameObject.Find ("MainMenu").GetComponent<SpringPanel> ().enabled = true;	
//#if UNITY_ANDROID && !UNITY_EDITOR
//        GSSdk.Instance.ShowSequence(0);
//#endif
    }
    public void selectionlevel10() {
        missionbreifing10easy.SetActive(true);
        missionbreifing10medium.SetActive(true);
        missionbreifing10hard.SetActive(true);
        missionbreifing9easy.SetActive(false);
        missionbreifing9medium.SetActive(false);
        missionbreifing9hard.SetActive(false);
        missionbreifing8easy.SetActive(false);
        missionbreifing8hard.SetActive(false);
        missionbreifing8medium.SetActive(false);
        missionbreifing6Easy.SetActive(false);
        missionbreifing6Medium.SetActive(false);
        missionbreifing6hard.SetActive(false);
        missionbreifing7easy.SetActive(false);
        missionbreifing7hard.SetActive(false);
        missionbreifing7medium.SetActive(false);
    
    
    }
    public void selectionlevel9() {
        missionbreifing10easy.SetActive(false);
        missionbreifing10medium.SetActive(false);
        missionbreifing10hard.SetActive(false);
        missionbreifing9easy.SetActive(true);
        missionbreifing9medium.SetActive(true);
        missionbreifing9hard.SetActive(true);
        missionbreifing8easy.SetActive(false);
        missionbreifing8hard.SetActive(false);
        missionbreifing8medium.SetActive(false);
        missionbreifing6Easy.SetActive(false);
        missionbreifing6Medium.SetActive(false);
        missionbreifing6hard.SetActive(false);
        missionbreifing7easy.SetActive(false);
        missionbreifing7hard.SetActive(false);
        missionbreifing7medium.SetActive(false);
    
    
    
    }
    public void selectionlevel8() {
        missionbreifing10easy.SetActive(false);
        missionbreifing10medium.SetActive(false);
        missionbreifing10hard.SetActive(false);
        missionbreifing9easy.SetActive(false);
        missionbreifing9medium.SetActive(false);
        missionbreifing9hard.SetActive(false);
        missionbreifing8easy.SetActive(true);
        missionbreifing8hard.SetActive(true);
        missionbreifing8medium.SetActive(true);
        missionbreifing6Easy.SetActive(false);
        missionbreifing6Medium.SetActive(false);
        missionbreifing6hard.SetActive(false);
        missionbreifing7easy.SetActive(false);
        missionbreifing7hard.SetActive(false);
        missionbreifing7medium.SetActive(false);
    
    
    }
    public void selectionlevel6() {
        missionbreifing10easy.SetActive(false);
        missionbreifing10medium.SetActive(false);
        missionbreifing10hard.SetActive(false);
        missionbreifing9easy.SetActive(false);
        missionbreifing9medium.SetActive(false);
        missionbreifing9hard.SetActive(false);
        missionbreifing8easy.SetActive(false);
        missionbreifing8hard.SetActive(false);
        missionbreifing8medium.SetActive(false);
        missionbreifing6Easy.SetActive(true);
        missionbreifing6Medium.SetActive(true);
        missionbreifing6hard.SetActive(true);
        missionbreifing7easy.SetActive(false);
        missionbreifing7hard.SetActive(false);
        missionbreifing7medium.SetActive(false);
    
    
    }
    public void selectionlevel() {
        missionbreifing10easy.SetActive(false);
        missionbreifing10medium.SetActive(false);
        missionbreifing10hard.SetActive(false);
        missionbreifing9easy.SetActive(false);
        missionbreifing9medium.SetActive(false);
        missionbreifing9hard.SetActive(false);
        missionbreifing8easy.SetActive(false);
        missionbreifing8hard.SetActive(false);
        missionbreifing8medium.SetActive(false);
        missionbreifing6Easy.SetActive(false);
        missionbreifing6Medium.SetActive(false);
        missionbreifing6hard.SetActive(false);
        missionbreifing7easy.SetActive(true);
        missionbreifing7hard.SetActive(true);
        missionbreifing7medium.SetActive(true);
    
    
    }
    public void backlevelpanel() {
        levelselectpanel.SetActive(true);
        Level1mission.SetActive(false);
        Level2mission.SetActive(false);
        Level3mission.SetActive(false);
        Level4mission.SetActive(false);
        Level5mission.SetActive(false);
        Level6mission.SetActive(false);
        Level7mission.SetActive(false);
        Level8mission.SetActive(false);
        Level9mission.SetActive(false);
        Level10mission.SetActive(false);


    }
    public void level1() {
        levelselectpanel.SetActive(false);
        Level1mission.SetActive(true);
      
    
    }
    public void level2()
    {
        levelselectpanel.SetActive(false);
        Level2mission.SetActive(true);


    }
    public void level3()
    {
        levelselectpanel.SetActive(false);
        Level3mission.SetActive(true);


    }
    public void level4()
    {
        levelselectpanel.SetActive(false);
        Level4mission.SetActive(true);


    }
    public void level5()
    {
        levelselectpanel.SetActive(false);
        Level5mission.SetActive(true);


    }
    public void level6()
    {
        levelselectpanel.SetActive(false);
        Level6mission.SetActive(true);


    }
    public void level7()
    {
        levelselectpanel.SetActive(false);
        Level7mission.SetActive(true);


    }
    public void level8()
    {
        levelselectpanel.SetActive(false);
        Level8mission.SetActive(true);


    }
    public void level9()
    {
        levelselectpanel.SetActive(false);
        Level9mission.SetActive(true);


    }
    public void level10()
    {
        levelselectpanel.SetActive(false);
        Level10mission.SetActive(true);


    }
	int sellevel;
	public void SelectLevel(GameObject obj){
       
        if (obj.name == "Sea")
            sellevel = 1;
        
        else if (obj.name == "Desert")
            sellevel = 2;
        else if (obj.name == "ArabianCity")
            sellevel = 3;
        else if (obj.name == "Snow")
        {
            sellevel = 4;
        }
        else if (obj.name == "DarkOcean")
            sellevel = 5;
       else if (obj.name == "Sea 1")
          sellevel = 6;
        else if (obj.name == "Desert 1")
            sellevel = 6;
        else if (obj.name == "Sea 2")
            sellevel = 6;
        else if (obj.name == "Snow 1")
            sellevel = 6;
        else if (obj.name == "DarkOcean 1")
            sellevel = 6;
        
        if (sellevel > GlobalEngine.maxLevel)
			return;
		else{
			GameObject.Find ("SelectLevel").GetComponent<SpringPanel> ().target = new Vector3 (-2000,0,0);
			GameObject.Find ("SelectLevel").GetComponent<SpringPanel> ().enabled = true;
			GameObject.Find("Mission" +sellevel.ToString() + "Briefing").GetComponent<SpringPanel>().target = new Vector3(0,0,0);
			GameObject.Find("Mission" +sellevel.ToString() + "Briefing").GetComponent<SpringPanel>().enabled = true;
			currentState = GameState.MissionBriefing;
            
            //#if UNITY_ANDROID && !UNITY_EDITOR
            //GSSdk.Instance.ShowSequence (2);
            //#endif
		}
	}

	public void PressedBackMissionBriefing(GameObject obj){
		obj.GetComponent<SpringPanel> ().target = new Vector3 (2000,0,0);
		obj.GetComponent<SpringPanel> ().enabled = true;
		GameObject.Find ("SelectLevel").GetComponent<SpringPanel> ().target = new Vector3 (0,0,0);
		GameObject.Find ("SelectLevel").GetComponent<SpringPanel> ().enabled = true;
	}

	public void PressedBackMissionBriefing1(){
		for (int i=1; i<8; i++) {
			GameObject.Find("Mission"+i.ToString() + "Briefing").GetComponent<SpringPanel> ().target = new Vector3 (2000,0,0);
			GameObject.Find("Mission"+i.ToString() + "Briefing").GetComponent<SpringPanel> ().enabled = true;
		}
		GameObject.Find ("SelectLevel").GetComponent<SpringPanel> ().target = new Vector3 (0,0,0);
		GameObject.Find ("SelectLevel").GetComponent<SpringPanel> ().enabled = true;
	}
    
	public void PressedPlayMissionBriefing(GameObject obj){
		if (obj.name == "Mission1Briefing") {
			GlobalEngine.currentScene = 1;
        }
       

        else if (obj.name == "Mission2Briefing")
        {
          
            GlobalEngine.currentScene = 2;
         
        }
        else if (obj.name == "Mission3Briefing")
        {
            GlobalEngine.currentScene = 3;
            
        }
        else if (obj.name == "Mission4Briefing")
        {
            GlobalEngine.currentScene = 4;

            
        }
        else if (obj.name == "Mission5Briefing")
        {
            GlobalEngine.currentScene = 5;
        
        }
        else if (obj.name == "Mission6Briefing")
        {
            GlobalEngine.currentScene = 6;
       
        }
        else if (obj.name == "Mission7Briefing")
        {
            GlobalEngine.currentScene = 7;
          
        }
        else if (obj.name == "Mission8Briefing")
        {
            GlobalEngine.currentScene = 8;
            
        }
        else if (obj.name == "Mission9Briefing")
        {
            GlobalEngine.currentScene = 9;
       
        }
    
        else if (obj.name == "Mission10Briefing")
        {
            GlobalEngine.currentScene = 10;
            
        }
        else if (obj.name == "Mission11Briefing")
        {
            GlobalEngine.currentScene = 11;
            
        }
        else if (obj.name == "Mission12Briefing")
        {
            GlobalEngine.currentScene = 12;
           
        }
        else if (obj.name == "Mission13Briefing")
        {
            GlobalEngine.currentScene = 13;
            
        }
        else if (obj.name == "Mission14Briefing")
        {
            GlobalEngine.currentScene = 14;
          
        }
        else if (obj.name == "Mission15Briefing")
        {
            GlobalEngine.currentScene = 15;
            
        }
        else if (obj.name == "Mission16Briefing")
        {
            GlobalEngine.currentScene = 16;
          
        }
        else if (obj.name == "Mission17Briefing")
        {
            GlobalEngine.currentScene = 17;
           
        }
        else if (obj.name == "Mission18Briefing")
        {
            GlobalEngine.currentScene = 18;
        
        }
        else if (obj.name == "Mission19Briefing")
        {
            GlobalEngine.currentScene = 19;

        }
        else if (obj.name == "Mission20Briefing")
        {
            GlobalEngine.currentScene = 20;

        }
        else if (obj.name == "Mission21Briefing")
        {
            GlobalEngine.currentScene = 21;

        }
        else if (obj.name == "Mission22Briefing")
        {
            GlobalEngine.currentScene = 22;

        }
        else if (obj.name == "Mission23Briefing")
        {
            GlobalEngine.currentScene = 23;

        }
        else if (obj.name == "Mission24Briefing")
        {
            GlobalEngine.currentScene = 24;

        }
        else if (obj.name == "Mission25Briefing")
        {
            GlobalEngine.currentScene = 25;

        }
        else if (obj.name == "Mission26Briefing")
        {
            GlobalEngine.currentScene = 26;

        }
        else if (obj.name == "Mission27Briefing")
        {
            GlobalEngine.currentScene = 27;

        }
        else if (obj.name == "Mission28Briefing")
        {
            GlobalEngine.currentScene = 28;

        }
        else if (obj.name == "Mission29Briefing")
        {
            GlobalEngine.currentScene = 29;

        }
        else if (obj.name == "Mission30Briefing")
        {
            GlobalEngine.currentScene = 30;

        }
        Time.timeScale = 1;

//#if UNITY_ANDROID && !UNITY_EDITOR
//        GSSdk.Instance.HideBanner();
//#endif
     //   AdScript.ADInstance.showInterstitialAd();
        SceneManager.LoadScene("Loading");
		currentState = GameState.InGame;
	}

	public void PressedYesExitGame(){
		Application.Quit ();
	}

	public void PressedNoExitGame1(){
        exitpanel.SetActive(false);
	//	GameObject.Find ("Exit Game1").GetComponent<SpringPanel> ().target = new Vector3 (2000,0,0);
		//GameObject.Find ("Exit Game1").GetComponent<SpringPanel> ().enabled = true;
		Time.timeScale = 1;
		//GameObject.Find ("Player").GetComponent<Indicator> ().enabled = true;
	//	GameObject.Find("Player").GetComponent<AudioSource>().Play();
	//	GameObject.Find("Music Audio").GetComponent<AudioSource>().Play();

		EnableGUIandControl ();
	}

	public void PressedYesExitGame1(){
        SceneManager.LoadScene("main menu");
		Time.timeScale = 1;
	}
	
	public void PressedNoExitGame(){
		currentState = GameState.MainMenu;
		GameObject.Find ("Exit Game").GetComponent<SpringPanel> ().target = new Vector3 (2000,0,0);
		GameObject.Find ("Exit Game").GetComponent<SpringPanel> ().enabled = true;
		GameObject.Find ("MainMenu").GetComponent<SpringPanel> ().target = new Vector3 (0,0,0);
		GameObject.Find ("MainMenu").GetComponent<SpringPanel> ().enabled = true;	
	}

	public void PressedResumePause(){
        pausebutton.SetActive(true);
        touchablecontrol.SetActive(true);
        subcontrolss.SetActive(true);
        pausepanel.SetActive(false);
        healthbar.SetActive(true);
        guibuttons.SetActive(true);
		Time.timeScale = 1;
		GameObject.Find ("Player").GetComponent<Indicator> ().enabled = true;
		GameObject.Find("Player").GetComponent<AudioSource>().Play();
		GameObject.Find("Music Audio").GetComponent<AudioSource>().Play();
		Debug.Log ("Resume");

		EnableGUIandControl ();
		currentState = GameState.InGame;
	}

	public void EnableGUIandControl(){
		GameObject.Find ("GUI/pause").GetComponent<GUITexture>().enabled = true;
	}

	public void DisableGUIandControl(){
		GameObject.Find ("_Touch Controller").GetComponent<TouchController> ().enabled = false;
	}

	public void PressedReplayPause(){
        pausepanel.SetActive(false);
		Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		//Application.LoadLevel (Application.loadedLevelName);
		currentState = GameState.InGame;
	}

	public void PressedMenuPause(){
        pausepanel.SetActive(false);
		Time.timeScale = 1;
        SceneManager.LoadScene("main menu");
		currentState = GameState.MainMenu;
        //#if UNITY_ANDROID && !UNITY_EDITOR
        //GSSdk.Instance.ShowBanner ();
        //#endif
	}

	public void PressedReplayLevelFailed(){
        leevelfailed.SetActive(false);
		Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		//Application.LoadLevel (Application.loadedLevelName);
		currentState = GameState.InGame;
	}

	public void PressedMenuLevelFailed(){
        leevelfailed.SetActive(false);
		Time.timeScale = 1;
        SceneManager.LoadScene("main menu");
		currentState = GameState.MainMenu;
//#if UNITY_ANDROID && !UNITY_EDITOR
//        GSSdk.Instance.ShowBanner ();
//#endif
	}
   
	public void PressedNextLevelLevelClear(){
       
      //  Application.LoadLevel(1);
    //	Time.timeScale = 1;
		//maxLevel인가 판별 
	//	if (Application.loadedLevelName == "DarkOcean") {
		//	Application.LoadLevel(Application.loadedLevel);	
	//	}
        leevelcomplete.SetActive(false);
        Level2mission.SetActive(true);
//GameObject.Find ("Level Clear").GetComponent<SpringPanel> ().target = new Vector3 (0, 1000, 0);
    //	GameObject.Find ("Level Clear").GetComponent<SpringPanel> ().enabled = true;
        
	//	GameObject.Find("Mission" +(Application.loadedLevel - 1).ToString() + "Briefing").GetComponent<SpringPanel>().target = new Vector3(0,0,0);
//	GameObject.Find("Mission" +(Application.loadedLevel - 1).ToString() + "Briefing").GetComponent<SpringPanel>().enabled = true;
		currentState = GameState.InGame;
      
        }
    
	

	public void PressedReplayLevelClear(){
        leevelcomplete.SetActive(false);
		Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		//Application.LoadLevel (Application.loadedLevelName);
		currentState = GameState.InGame;
	}

	public void PressedMenuLevelClear(){
		Time.timeScale = 1;
        leevelcomplete.SetActive(false);
        SceneManager.LoadScene("main menu");
		currentState = GameState.MainMenu;
        //#if UNITY_ANDROID && !UNITY_EDITOR
        //GSSdk.Instance.ShowBanner ();
        //#endif
	}

	public void MissionFail(){
       
		currentState = GameState.PreState;
		StartCoroutine("Fail");
		DisableGUIandControl ();
     //   AdScript.ADInstance.showInterstitialAd();
	}

	IEnumerator Fail(){      
        leevelfailed.SetActive(true);
        //#if UNITY_ANDROID && !UNITY_EDITOR
        //GSSdk.Instance.ShowSequence (4);
        //#endif
		yield return new WaitForSeconds (5);
       
		Time.timeScale = 0;
     //   touchcontrrrl.SetActive(false);
        leevelfailed.SetActive(true);
       
     //   guibuttons.SetActive(false);
	//	GameObject.Find ("Level Failed").GetComponent<SpringPanel> ().target = new Vector3 (0, 0, 0);
	//	GameObject.Find ("Level Failed").GetComponent<SpringPanel> ().enabled = true;
        GameObject.Find("Player").GetComponent<Indicator>().enabled = false;
        
		currentState = GameState.Fail;
	}

	public void MissionClear(){
      
       // Advertisement.Show();
		currentState = GameState.PreState;
		DisableGUIandControl ();
		StartCoroutine ("Clear");
         
	}

	IEnumerator Clear()
    {
        leevelcomplete.SetActive(true);
        touchablecontrol.SetActive(false);
        subcontrolss.SetActive(false);
        healthbar.SetActive(false);
        guibuttons.SetActive(false);
        pausebutton.SetActive(false);

        yield return new WaitForSeconds (1);
      
		Time.timeScale = 0;
        
        GameObject.Find("Player").GetComponent<Indicator>().enabled = false;

GlobalEngine.maxLevel = Mathf.Min (Mathf.Max(GlobalEngine.currentScene + 1, GlobalEngine.maxLevel), 30);
     PlayerPrefs.SetInt ("MaxLevel", GlobalEngine.maxLevel);
        
		GameObject.Find ("Player").GetComponent<Indicator> ().enabled = false;
		GameObject.Find("Player").GetComponent<AudioSource>().Pause();
		GameObject.Find("Music Audio").GetComponent<AudioSource>().Pause();

		currentState = GameState.Complete;
      	}

   
    
    

    }
