using UnityEngine;
using System.Collections;

public class SetFlyF16_Carrier : MonoBehaviour {
	private float setTime;
	private bool setFlag;
	public Transform point;
	private Quaternion rndRotVec;
	private bool rotFlag;
	// Use this for initialization
	void Start () {
		setFlag = false;
		rotFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time - setTime) > 4 && setFlag == true) {
			setFlag = false;
			rotFlag = true;
			setTime = Time.time;
		}
		if (rotFlag) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(point.position - transform.position), Time.deltaTime * 0.3f);		
			if((Time.time - setTime) > 5){
				GetComponent<AIController>().enabled = true;
				Destroy(this);
			}
		}
	}

	public void	SetStart(){

		GetComponent<FlightSystem> ().enabled = true;
		GetComponent<FlightSystem> ().Sangsung = true;
		setTime = Time.time;
		setFlag = true;
//		GetComponent<FlightSystem> ().FollowTarget = true;
	}
}
