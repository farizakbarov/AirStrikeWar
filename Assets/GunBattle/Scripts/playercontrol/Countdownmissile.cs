using UnityEngine;
using System.Collections;

public class Countdownmissile : MonoBehaviour {

    public PlayerControl player;
    public Camera cam;
    public TouchController ctrl;

    public const int ZONE_FORWARD = 0;
    public const int ZONE_BACKWARD = 1;
    public const int ZONE_BULLET = 2;
    public const int ZONE_MISSILE = 3;
    public const int ZONE_GUIDEMISSILE = 4;

    //--------------enemy's shooting Range
    public const float S_BOATRANGE = 150;
    public const float S_DESTROYRANGE = 200;
    public const float S_SOLDIERRANGE = 80;

    //-----------------------------------

    //--------------enemy's active Range
    public const float A_BOATRANGE = 200;
    public const float A_DESTROYRANGE = 250;
    //-------------------------------------

    //-------------currnet enemy's health
    public const float H_BOATHEALTH = 10;
    public const float H_DESTROYHEALTH = 20;
    //----------------------------------------

    //-------------bullet power
    public const int BULLETPOWER = 5;
    public const int MISSILEPOWER = 20;
    //-------------------------------------------



    //--------------------current my health
    public static float maxHealth = 1000;
    public static float myhealth = 1000;
    //----------------------------------------

    //-------------------Bullet, Missile and GuideMissile Count
    public static int[] g_bulletCount = new int[5] { 1500, 1500, 1500, 1500, 1500};
    public static int[] g_missileCount = new int[5] { 300, 300, 500, 500, 500 };
    public static int[] g_guideMissileCount = new int[5] { 70, 650, 50, 50, 50 };
//    public static int[] mediumg_missileCouunt = new int[5] { 300, 500, 500, 500, 500 };
 //   public static int[] mediumg_guideMissilecount = new int[5] { 60, 500, 500, 500, 500 };

    public static int c_bulletCount;

//    public static int c_missileCount;
//    public static int c_guideMissileCount;
    public static int c_meddiummissilecount;
    public static int c_meddiumguidemissilecount;
    //---------------------------------------------------

    //---------------healthTexture---------------------//
   public static Texture healthbarBG;
    public static Texture healthbarHat;

    public Texture _healthbarBG;
    public Texture _healthbarHat;
    // Use this for initialization
    void Start()
    {
        if (this.ctrl == null)
            Debug.LogError("TouchController not assigned!");
       if (this.cam == null)
           Debug.LogError("Camera not assigned!");

   //     Manually init the controller...

       this.ctrl.InitController();
         myhealth = maxHealth;
       healthbarBG = _healthbarBG;
       healthbarHat = _healthbarHat;
    }

    // Update is called once per frame
    void Update()
    {
        this.ctrl.PollController();
        this.ctrl.UpdateController();

        if (this.player != null)
        {
        //    this.player.ControlByTouch(this.ctrl, this);
            //			this.player.UpdateChara();
        }
    }

    public void OnGUI()
    {
       if (this.ctrl != null)
            this.ctrl.DrawControllerGUI();
    }
}


