using UnityEngine;
using System.Collections;

public class ShootGuideMissile : MonoBehaviour {
	public Transform target;
	public Transform shootPos;
	public GameObject missile;
	public bool shootable = false;

	public float shootInterval = 3;
	private float lastShootTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (shootable) {
			if(Time.time - lastShootTime > shootInterval){
				//Instantiate;
				Instantiate(missile, shootPos.position, shootPos.rotation);
				lastShootTime = Time.time;
			}		
		}
	}
}
