using UnityEngine;
using System.Collections;

public class soundsystem : MonoBehaviour {
    public GameObject bgsound;
    public GameObject soundon;
    public GameObject soundoff;
    public GameObject musicon;
    public GameObject musicoff;
	// Use this for initialization
    void Start()
    {
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void soundsystemoff() {
        soundoff.SetActive(false);
        soundon.SetActive(true);
        bgsound.SetActive(true);
    
    }
    public void soundsystemonn()
    {
        soundoff.SetActive(true);
        soundon.SetActive(false);
        bgsound.SetActive(false);
      //  bgsound.GetComponent<AudioSource>().Pause();
      


    }
    public void musicsystemoff()
    {
        musicoff.SetActive(false);
        musicon.SetActive(true);
      
        bgsound.SetActive(true);

    }
    public void musicsystemonn()
    {
        musicoff.SetActive(true);
        musicon.SetActive(false);
        bgsound.SetActive(false);
        //  bgsound.GetComponent<AudioSource>().Pause();



    }
    public void moreaps() {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Tanglo%20Games");
    
    }
    public void Rateus() {

        Application.OpenURL("https://play.google.com/store/apps/details?id=com.tanglogames.helicopter.gunship.war3d.air.battle");
    
    }
}
