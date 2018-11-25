using UnityEngine;
using System.Collections;

public class AntiAirGunControl : MonoBehaviour {
	public Transform  player;
	public Transform gunBody;
	public Transform gunTurret;
	public float shootableDistance;
	private Vector3 deltaPos, deltaPos1;
	private Quaternion newRot;
	private Transform myTransform;
	// Use this for initialization
	void Start () {
		if (player == null)
			player = GameObject.Find ("Player").transform;
		myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null)
						return;
		
		if (Vector3.Distance (myTransform.position, player.position) > shootableDistance)
			return;

		deltaPos = player.position - myTransform.position;
		deltaPos1 = new Vector3 (deltaPos.x, 0, deltaPos.z);
		newRot = Quaternion.LookRotation (deltaPos1, myTransform.up);
		gunBody.rotation = Quaternion.Slerp(gunBody.rotation, newRot, Time.deltaTime * 8);
		gunTurret.LookAt (player.position);
	}
}
