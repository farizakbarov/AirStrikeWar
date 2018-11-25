using UnityEngine;
using System.Collections;

public class ManageHealth : MonoBehaviour {
	private float health;
	public int enemykind;
	public GameObject explosionEffect;
	public Transform[]  explosionPos;

	private bool deadFlag = false; 
	// Use this for initialization
	void Start () {
		if(enemykind == 0)
			health = GlobalGameState.H_DESTROYHEALTH;
		else if(enemykind == 1)
			health = GlobalGameState.H_BOATHEALTH;
	}
	
	// Update is called once per frame
	void Update () {
		if (!deadFlag)
						return;

		transform.position -= new Vector3 (0, Time.deltaTime * 1.5f, 0);
		transform.Rotate (transform.forward, Time.deltaTime * 5);

		if (transform.position.y < -10) 
						Destroy (gameObject);
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Bullet"){
			switch (col.gameObject.name){
			case "bullet_minirocket(Clone)":
				health -= GlobalGameState.MISSILEPOWER;
				break;
			case "bullet_normal(Clone)":
				health -= GlobalGameState.BULLETPOWER;
				break;
			}
			if(health <= 0)
				Dead();
		}
	}

	void Dead(){
		for(int i=0;i<explosionPos.Length;i++){
			GameObject obj = (GameObject)Instantiate (explosionEffect, explosionPos[i].position, Quaternion.identity);
			obj.transform.parent = transform;
			Destroy (obj, 3);
		}
			deadFlag = true;

		Destroy (transform.GetComponent<PatrolMoveController> ());
		Destroy (transform.GetComponentInChildren<RotateTurret> ());
		Destroy (transform.GetComponentInChildren<ShootBullet> ());
	}
}
