using UnityEngine;
using System.Collections;

public class ShootBullet : MonoBehaviour {
	public Transform player;
	public GameObject bullet;
	public float rndRange = 3;
	public float shootInterval = 0.2f;	
	public float shootDuringTime = 1.0f;
	public float shootRestTime = 2.0f;	
	public int kind = 0;

	private float shootableDistance;
	private float lastRestTime;
	private float shootingTime;	
	private float lastShootTime;
	private bool restFlag;

	private Transform myTransform;
	// Use this for initialization
	void Start () {
		if (player == null)
			player = GameObject.Find ("Player").transform;
		myTransform = transform;
		if (kind == 0)
			shootableDistance = GlobalGameState.S_DESTROYRANGE;
		else if (kind == 1)
			shootableDistance = GlobalGameState.S_BOATRANGE;
	}
 

	// Update is called once per frame
	void Update () {
		if (player == null)
						return;
		if (Vector3.Distance (myTransform.position, player.position) > shootableDistance)
			return;

		RaycastHit hit;
		if (Physics.Raycast (myTransform.position, player.position - myTransform.position, out hit, shootableDistance)){
			if (LayerMask.LayerToName (hit.collider.gameObject.layer) != "Player")
				return;
		}

		if ((Time.time - lastRestTime > shootRestTime) && restFlag == true) {
			restFlag = false;
			shootingTime = Time.time;
		}
		else if((Time.time - shootingTime > shootDuringTime) && restFlag == false){
			restFlag = true;
			lastRestTime = Time.time;
		}
		if(restFlag == false){
			if(Time.time - lastShootTime > shootInterval){
				lastShootTime = Time.time;
				Quaternion rot = Quaternion.LookRotation(player.position + new Vector3(0, 2, 0) - myTransform.position + Random.insideUnitSphere * rndRange);
				GameObject.Instantiate(bullet, myTransform.position, rot);
			}
		}
	}
}
