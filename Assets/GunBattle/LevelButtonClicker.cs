using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LevelButtonClicker : MonoBehaviour 
{
	public int loadcampaign = 1 ;
	public string levelname;
	public int loadlevel;

	public Sprite bgnormalTex;
	public Sprite bghoverTex; 

	public AudioClip btnClick;

	void OnMouseEnter () 
	{
		GetComponent<Image>().sprite = bghoverTex;		
	}
	
	void OnMouseExit()
	{
		GetComponent<Image>().sprite = bgnormalTex;
	}

	void Start()
	{
		if(loadcampaign == 1)
		{
			levelname = "Level1";
		}
		else if(loadcampaign == 2)
		{
			levelname = "Level2";
		}
//		loadlevel = levelNum; 
		
	}	

	
	public void LoadLevel()
	{
		if(PlayerPrefs.GetInt("soundfx") == 1)
		{
			GetComponent<AudioSource>().PlayOneShot(btnClick);
		}
		
		
		PlayerPrefs.SetInt ("currentLevel",loadlevel);
		
		#if UNITY_ANDROID
		Handheld.SetActivityIndicatorStyle(AndroidActivityIndicatorStyle.Large);
		#endif
		Handheld.StartActivityIndicator();
		
		Application.LoadLevel (levelname);
		
	}

}
