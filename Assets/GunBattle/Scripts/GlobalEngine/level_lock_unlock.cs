using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_lock_unlock : MonoBehaviour
{
    //   public GameObject[] locked;
    public GameObject[] LevelButton;
    public GameObject img1, img2, img3, img4, img5, img6, img7, img8, img9, img10;
    //  public GameObject[] unlocked;
    //    public int unlockedCount;
    // Use this for initialization

    void Awake()
    {
        //            PlayerPrefs.SetInt ("campaign1Completed", 1);
        //       if (PlayerPrefs.GetInt("campaign1Completed") == 0)
        //      {
        //          PlayerPrefs.SetInt("campaign1Completed", 1);
        //       }
        //       unlockedCount = PlayerPrefs.GetInt("campaign1Completed");
    }

    void Start()
    {
        PlayerPrefs.SetInt("level", 1);
        if (PlayerPrefs.GetString("level1Status") == "unlock")
        {

            //  LevelButton[1].GetComponent<Button>().interactable = true;
            PlayerPrefs.SetInt("level", 2);
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
        if (PlayerPrefs.GetString("level2Status") == "unlock")
        {

            img2.SetActive(false);
            //    LevelButton[2].GetComponent<Button>().interactable = true;
            PlayerPrefs.SetInt("level", 3);
            img3.SetActive(false);
            img4.SetActive(true);
            img5.SetActive(true);
            img6.SetActive(true);
            img7.SetActive(true);
            img8.SetActive(true);
            img9.SetActive(true);
            img10.SetActive(true);
        }
        if (PlayerPrefs.GetString("level3Status") == "unlock")
        {

            //   LevelButton[3].GetComponent<Button>().interactable = true;
            PlayerPrefs.SetInt("level", 4);
            img2.SetActive(false);
            img3.SetActive(false);
            img4.SetActive(false);
            img5.SetActive(true);
            img6.SetActive(true);
            img7.SetActive(true);
            img8.SetActive(true);
            img9.SetActive(true);
            img10.SetActive(true);
        }
        if (PlayerPrefs.GetString("level4Status") == "unlock")
        {

            //     LevelButton[4].GetComponent<Button>().interactable = true;
            PlayerPrefs.SetInt("level", 5);
            img2.SetActive(false);
            img3.SetActive(false);
            img4.SetActive(false);
            img5.SetActive(false);
            img6.SetActive(true);
            img7.SetActive(true);
            img8.SetActive(true);
            img9.SetActive(true);
            img10.SetActive(true);
        }

        if (PlayerPrefs.GetString("level5Status") == "unlock")
        {

            //    LevelButton[5].GetComponent<Button>().interactable = true;
            PlayerPrefs.SetInt("level", 6);
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
        if (PlayerPrefs.GetString("level6Status") == "unlock")
        {

            //  LevelButton[6].GetComponent<Button>().interactable = true;
            PlayerPrefs.SetInt("level", 7);
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
        if (PlayerPrefs.GetString("level7Status") == "unlock")
        {

            //  LevelButton[6].GetComponent<Button>().interactable = true;
            PlayerPrefs.SetInt("level", 8);
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
        if (PlayerPrefs.GetString("level8Status") == "unlock")
        {

            //  LevelButton[6].GetComponent<Button>().interactable = true;
            PlayerPrefs.SetInt("level", 9);
            img2.SetActive(false);
            img3.SetActive(false);
            img4.SetActive(false);
            img5.SetActive(false);
            img6.SetActive(false);
            img7.SetActive(false);
            img8.SetActive(false);
            img9.SetActive(false);
            img10.SetActive(true);
        }
        if (PlayerPrefs.GetString("level9Status") == "unlock")
        {

            //  LevelButton[6].GetComponent<Button>().interactable = true;
            PlayerPrefs.SetInt("level", 10);
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


        //for (int i = 1; i <= 9; i++)
        // {
        //    if (i <= unlockedCount)
        //    {
        //      unlocked[i - 1].SetActive(true);
        //  locked[i - 1].SetActive(false);
        //  }
        //  else
        //  {
        //    unlocked[i - 1].SetActive(false);
        //     locked[i - 1].SetActive(true);
        //  }
        // }
        //}
    }
}

