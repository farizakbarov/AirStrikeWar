using UnityEngine;
using System.Collections;

public class DamageControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ApplyDamage(int damage){
		transform.SendMessage ("SetDamage", damage);
	}
}
