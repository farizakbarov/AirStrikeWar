using UnityEngine;
using System.Collections;

public class ShipDestroyer : MonoBehaviour {
	public float health = 100;
	public float blameHealth = 20;	//blameHealth > health => setblame, movestop, rigidbody(true), changeMap

	public Transform[] blamePos;
	private bool blameFlag = false;
	public GameObject blameParticle;
	public GameObject destroyParticle;

	public Renderer[] beChangedRenderer;
	public Texture[] destroyedTexture;

	public Transform[] sBullet;
	public Transform[] turret;

	private bool deadFlag = false;
	private Transform myTransform;
	void Start () {
		myTransform = transform;
		transform.GetComponent<enemyHealthBar> ().InitHealth (health);
	}
	
	// Update is called once per frame
	void Update () {
		if (!deadFlag)
			return;
		
		myTransform.position -= new Vector3 (0, Time.deltaTime * 1.5f, 0);
		myTransform.Rotate (myTransform.forward, Time.deltaTime * 5);
		
		if (myTransform.position.y < -10) 
			Destroy (gameObject);
	}

	public void SetDamage(int damage){
		health -= damage;
		if (health < blameHealth && blameFlag == false) {
			blameFlag = true;
			SetBlame();
		}
		
		if(health <= 0){
			DeadShip();
		}

		myTransform.GetComponent<enemyHealthBar> ().RecvHealthVal (health);
	}

	void SetBlame(){
		for (int i=0; i<blamePos.Length; i++){
			GameObject.Instantiate (blameParticle, blamePos [i].position, Quaternion.identity);		
		}
		if(gameObject.GetComponent<PatrolMoveController>() != null)
			gameObject.GetComponent<PatrolMoveController> ().enabled = false;
		for (int j=0; j<beChangedRenderer.Length; j++) {
			beChangedRenderer[j].material.mainTexture = destroyedTexture[j];		
		}
	}

	void DeadShip(){
		for (int i=0; i<blamePos.Length; i++){
			if(destroyParticle != null){
				GameObject obj = (GameObject)Instantiate(destroyParticle, blamePos [i].position, Quaternion.identity);	
				obj.transform.parent = myTransform;
				Destroy (obj, 3);
			}
		}
		GameObject.Find ("water_foam").GetComponent<ParticleEmitter>().emit = false;
		if (sBullet.Length != 0) {
			for(int i=0;i<sBullet.Length;i++)
				sBullet[i].GetComponent<ShootBullet>().enabled = false;	
		}
		if (turret.Length != 0) {
			for(int i=0;i<turret.Length;i++)
				turret[i].GetComponent<RotateTurret>().enabled = false;		
		}
		myTransform.GetComponent<PatrolMoveController> ().enabled = false;

		deadFlag = true;
//		Destroy (gameObject, 6);
	}
}
