using UnityEngine;
using System.Collections;

public class AntiAirGunDestroyer : MonoBehaviour {
	public float health = 100;
	public float blameHealth = 20;	//blameHealth > health => setblame, movestop, rigidbody(true), changeMap, shoot stop
	
	//health<0 => rigidbody.addforce(rnd), destroyParticle, destroy(10seconds)
	public float recvForce = 1;
	public Transform[] blamePos;
	private bool blameFlag = false;
	public GameObject blameParticle;
	public GameObject destroyParticle;
	
	public Renderer[] beChangedRenderer;
	public Texture[] destroyedTexture;

	public Transform[] sBullet;
	// Use this for initialization
	void Start () {
		transform.GetComponent<enemyHealthBar> ().InitHealth (health);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetDamage(int damage){
		health -= damage;
		if (health < blameHealth && blameFlag == false) {
			blameFlag = true;
			SetBlame();
		}
		
		if(health <= 0){
			DeadTruck();
		}
		transform.GetComponent<enemyHealthBar> ().RecvHealthVal (health);
	}

	void SetBlame(){
		for (int i=0; i<blamePos.Length; i++){
			GameObject.Instantiate (blameParticle, blamePos [i].position, Quaternion.identity);		
		}
		foreach (Rigidbody rd in transform.GetComponentsInChildren<Rigidbody>()){
			rd.isKinematic = false;
		}
		for (int j=0; j<beChangedRenderer.Length; j++) {
			beChangedRenderer[j].material.mainTexture = destroyedTexture[j];		
		}
	}
	
	void DeadTruck(){
		for (int i=0; i<blamePos.Length; i++){
			if(destroyParticle != null)
				GameObject.Instantiate (destroyParticle, blamePos [i].position, Quaternion.identity);		
		}
		transform.GetComponent<AntiAirGunControl> ().enabled = false;
		transform.GetComponent<DamageControl> ().enabled = false;
		for (int i=0; i<sBullet.Length; i++) {
			sBullet[i].GetComponent<ShootBullet>().enabled = false;		
		}
		Destroy (gameObject, 10);
	}
}
