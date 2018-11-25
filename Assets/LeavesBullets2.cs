using UnityEngine;
using System.Collections;

public class LeavesBullets2 : MonoBehaviour {

    public GUIText bulletText;
    public GUIText MediummissileText;
    public GUIText MediumguideMissileText;
    // Use this for initialization
    void Start()
    {
        bulletText.text = GlobalGameState.c_bulletCount.ToString();
        MediummissileText.text = GlobalGameState.c_missileCount.ToString();
        MediumguideMissileText.text = GlobalGameState.c_guideMissileCount.ToString();
    }

    public void SetBulletCount(int current, int count)
    {
        switch (current)
        {
            case 1:
                bulletText.text = GlobalGameState.c_bulletCount.ToString();
                break;
            case 2:
                MediummissileText.text = GlobalGameState.c_missileCount.ToString();
                break;
            case 3:
                MediumguideMissileText.text = GlobalGameState.c_guideMissileCount.ToString();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
