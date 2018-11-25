using UnityEngine;
using System.Collections;

public class DronePropellerRotate : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (transform.forward, -500 * Time.deltaTime);
	}
}
