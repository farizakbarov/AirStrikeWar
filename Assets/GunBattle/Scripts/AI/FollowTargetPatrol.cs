using UnityEngine;
using System.Collections;

public class FollowTargetPatrol : MonoBehaviour {
	public Transform player;
	public float rotateSpeed = 1;
	// Use this for initialization
	void Start () {
		if (!player) {
			player = GameObject.Find("Player").transform;		
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.position;
		transform.Rotate (Vector3.up, Time.deltaTime * rotateSpeed);
	}
}
