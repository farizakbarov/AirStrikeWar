using UnityEngine;
using System.Collections;

public class DamageManager : MonoBehaviour
{
	public AudioClip[] HitSound;
	//	soundcontrol cameraScript;
	public GameObject Effect;
	public int HP = 100;
	public GameObject[] paticle;
	private float smoketime = 0.3f;
	public bool deadflag = false;
	//	GameUI gameui;

	void Start(){
		if(transform.GetComponent<enemyHealthBar>() != null)
			transform.GetComponent<enemyHealthBar> ().InitHealth (HP);
	}

	public void ApplyDamage(int damage,GameObject killer)
	{
		GameObject obj = (GameObject)Instantiate (paticle[4].gameObject,transform.position,Quaternion.identity);
		Destroy (obj, 2);
		HP -= damage;
		if( HP<60){
			if(paticle[2] != null)
				paticle[2].GetComponent<ParticleEmitter>().emit = true;
		}else if( HP<30){
			if(paticle[3] != null)
				paticle[3].GetComponent<ParticleEmitter>().emit = true;
		}
		if(transform.GetComponent<enemyHealthBar>() != null)
			transform.GetComponent<enemyHealthBar> ().RecvHealthVal (HP);

		if (HitSound.Length > 0)
		{
			AudioSource.PlayClipAtPoint(HitSound[Random.Range(0, HitSound.Length)], transform.position);
		}
		if (HP <= 0)
		{
			if(this.gameObject != null){
				if(killer.CompareTag("water")){
					Instantiate(paticle[0].gameObject,transform.position,Quaternion.identity);
					Effect = null;
					Dead();
				}else if(killer.CompareTag("Terrain")){
					Instantiate(paticle[1].gameObject,transform.position,Quaternion.identity);
					Effect = null;
					Dead();
				}else{
					Dead();
				}
			}
			
			
		}
	}
	
	private void Dead()
	{
		GameObject obj;
		if (Effect){
			if(Effect.name == "f16a_Dead_off"){
				obj = (GameObject)GameObject.Instantiate(Effect, transform.position + Vector3.up * 2, transform.rotation);
			}
			else 
				obj = (GameObject)GameObject.Instantiate(Effect, transform.position, transform.rotation);
			if(this.GetComponent<Rigidbody>()){
				if(obj.GetComponent<Rigidbody>()){
					obj.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
				}
			}
		}
		
		Destroy(this.gameObject);
	}
	
}
