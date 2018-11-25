using UnityEngine;
using System.Collections;

public class LeaveEnemy : MonoBehaviour {
    public GameObject subcontrols;
    public GameObject levelcomplete;
	public int enemyCount;
	public GUIText leaveEnemyCount;
	private float lastTime;
	private float timeInterval = 1;
	private bool missionClearFlag = false;
	// Use this for initialization
	void Start () {
		enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length + GameObject.FindGameObjectsWithTag ("AirEnemy").Length;
		leaveEnemyCount.text = "  " + enemyCount.ToString ();
		missionClearFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - lastTime > timeInterval){
			enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length + GameObject.FindGameObjectsWithTag ("AirEnemy").Length;
			leaveEnemyCount.text = "  " + enemyCount.ToString ();
			lastTime = Time.time;
		}

		if (enemyCount <= 0 && missionClearFlag == false) {
            AllGUI.unlockimg2 = true;
            subcontrols.SetActive(false);
			GameObject.Find("UI Root").GetComponent<AllGUI>().SendMessage("MissionClear");
            GameObject.Find("Player").GetComponent<RadarSystem>().enabled = false;
            missionClearFlag = true;
         
		}
	}
}
