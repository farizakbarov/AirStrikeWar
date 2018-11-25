using UnityEngine;
using System.Collections;

public class waterparticletime : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject.Destroy (this.gameObject , 1f);
	}
}
