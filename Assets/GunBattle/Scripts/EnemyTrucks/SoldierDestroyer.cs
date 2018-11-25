using UnityEngine;
using System.Collections;

public class SoldierDestroyer : MonoBehaviour {
	public float health = 3;
	public float recvForce = 1;
	public GameObject[] deadSoldier;
	private bool bFlag = false;
	// Use this for initialization
	void Start () {
		transform.GetComponent<enemyHealthBar> ().InitHealth (health);
	}
	
	public void SetDamage(int damage){
		health -= damage;
		if (health < 0 && bFlag == false) {
			DeadSoldier();
			bFlag = true;
		}
		transform.GetComponent<enemyHealthBar> ().RecvHealthVal (health);
	}

	void DeadSoldier(){
		GameObject obj = (GameObject)GameObject.Instantiate (deadSoldier[Random.Range(0,2)], transform.position, transform.rotation);
		Destroy (obj, 5);
		Destroy (gameObject);
	}
}
