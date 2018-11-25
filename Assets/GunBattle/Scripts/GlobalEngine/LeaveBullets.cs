using UnityEngine;
using System.Collections;

public class LeaveBullets : MonoBehaviour {
	public GUIText bulletText;
	public GUIText missileText;
	public GUIText guideMissileText;
 //   public GUIText MediummissileText;
 //  public GUIText MediumguidemissileText;
	// Use this for initialization
	void Start () {
		bulletText.text = GlobalGameState.c_bulletCount.ToString();
		missileText.text = GlobalGameState.c_missileCount.ToString();
		guideMissileText.text = GlobalGameState.c_guideMissileCount.ToString();
    //    MediummissileText.text = Countdownmissile.c_meddiummissilecount.ToString();
     //   MediumguidemissileText.text = Countdownmissile.c_meddiumguidemissilecount.ToString();
	}

	public void SetBulletCount(int current, int count){
		switch (current) {
		case 1:
			bulletText.text = GlobalGameState.c_bulletCount.ToString();
			break;
		case 2:
			missileText.text = GlobalGameState.c_missileCount.ToString();
			break;
		case 3:
			guideMissileText.text = GlobalGameState.c_guideMissileCount.ToString();
			break;
       //     case 4:
      //      MediummissileText.text = Countdownmissile.c_meddiummissilecount.ToString();
        //   break;
       //    case 5:
       //    MediumguidemissileText.text = Countdownmissile.c_meddiumguidemissilecount.ToString();
//break;
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
