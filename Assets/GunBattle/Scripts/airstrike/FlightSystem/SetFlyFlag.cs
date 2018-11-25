using UnityEngine;
using System.Collections;

public class SetFlyFlag : MonoBehaviour {
	public Transform mplayer;
	private Transform myTransform;
	private bool activeFlag = false;
	private bool activeFlag1 = false;
	public GameObject[] f16_carrier;
	// Use this for initialization
	void Start () {
		if (mplayer == null) {
			mplayer = GameObject.Find("Player").transform;		
		}
		myTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (mplayer.position, myTransform.position) < 700 && activeFlag == false) {
			activeFlag = true;		
		}

		if (activeFlag == true && activeFlag1 == false) {
			for(int i=0;i< f16_carrier.Length;i++){
//				f16_carrier[i].SendMessage("Setstarttime", i * 4);
				Setstarttime(i);
			}		
			activeFlag1 = true;
		}
	}

	private void Setstarttime(int idx){
		StartCoroutine("SetFly", idx);
//		Debug.Log ("SSSSS");
//		yield return new WaitForSeconds (idx * 4);
//		f16_carrier[idx].SendMessage("SetStart");
	}

	IEnumerator SetFly(int idx){
		yield return new WaitForSeconds (idx * 4);
		f16_carrier[idx].SendMessage("SetStart");
	}
}
