using UnityEngine;
using System.Collections;

public class UImanager : MonoBehaviour {
    public static UImanager instance;
    public GameObject easyRocketMisile;
    public static bool erocket = false;
    public GameObject easyAirToAir;
    public GameObject MediumRocketMisile;
    public static bool Mrocket = false;
    public GameObject MediumAirToAir;
    public GameObject hardRocketMisile;
   public static bool Hrocket = false;
    public GameObject hardAirToAir;
	// Use this for initialization
	void Start () {
      
	
	}
	
	// Update is called once per frame
	void Update () {
        if (erocket == true) {
            easyAirToAir.SetActive(true);
            easyRocketMisile.SetActive(true);
            MediumAirToAir.SetActive(false);
            MediumRocketMisile.SetActive(false);
            hardAirToAir.SetActive(false);
            hardRocketMisile.SetActive(false);

        }
       if (Mrocket == true) {
            MediumRocketMisile.SetActive(true);
            MediumAirToAir.SetActive(true);
            easyRocketMisile.SetActive(false);
            hardAirToAir.SetActive(false);
            easyAirToAir.SetActive(false);
            hardRocketMisile.SetActive(false);

        }
      //  if (Mairto == true)
      //  {
          //  MediumAirToAir.SetActive(true);
         //   easyAirToAir.SetActive(false);
         //   hardAirToAir.SetActive(false);
       // }
	
       if (Hrocket == true) {
           hardRocketMisile.SetActive(true);
            hardAirToAir.SetActive(true);
            MediumRocketMisile.SetActive(false);
            MediumAirToAir.SetActive(false);
            easyRocketMisile.SetActive(false);
               easyAirToAir.SetActive(false);
        }
    //    if (Hairtoair== true)
      //  {
        //    hardAirToAir.SetActive(true);
        //    MediumAirToAir.SetActive(false);
         //   easyAirToAir.SetActive(false);
        }
	}

