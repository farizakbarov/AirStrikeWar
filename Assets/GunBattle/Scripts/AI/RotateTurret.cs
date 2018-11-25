using UnityEngine;
using System.Collections;

public class RotateTurret : MonoBehaviour {
	public Transform player;
	public GameObject parent; 
	public int kind = 0;
	private float shootableDistance;
	private Vector3 deltaPos;
	// Use this for initialization
	void Start () {
		if (player == null)
			player = GameObject.Find ("Player").transform;
		if (kind == 0)
			shootableDistance = GlobalGameState.S_DESTROYRANGE;
		else if (kind == 1)
			shootableDistance = GlobalGameState.S_BOATRANGE;
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null)
						return;

		if (Vector3.Distance (transform.position, player.position) > shootableDistance)
			return;

		deltaPos = player.position - transform.position;
		deltaPos = new Vector3 (deltaPos.x, 0, deltaPos.z);
		Quaternion newRot = Quaternion.LookRotation (deltaPos, transform.up);
		transform.rotation = Quaternion.Slerp(transform.rotation, newRot, Time.deltaTime * 8);
	}
}
