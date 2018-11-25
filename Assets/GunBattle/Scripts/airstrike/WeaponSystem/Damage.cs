using UnityEngine;
using System.Collections;

public class Damage : DamageBase
{
    public bool Explosive;
    public float ExplosionRadius = 20;
    public float ExplosionForce = 1000;
	public bool HitedActive = true;
	public float TimeActive = 0;
	public bool RandomTimeActive;
	private float timetemp = 0;

    private void Start()
    {
		timetemp = Time.time;
		if(RandomTimeActive)
			TimeActive = Random.Range(TimeActive/2f,TimeActive);

        if (!Owner || !Owner.GetComponent<Collider>()) return;
        Physics.IgnoreCollision(GetComponent<Collider>(), Owner.GetComponent<Collider>());
		
		
    }

    private void Update()
    {
		GameObject.Destroy (this.gameObject,5f);
		if(TimeActive>0){
			if(Time.time >= (timetemp + TimeActive)){
				ActiveE();
			}
		}
    }

    public void Active()
    {
        if (Effect)
        {
            GameObject obj = (GameObject) Instantiate(Effect, transform.position, transform.rotation);
            Destroy(obj, LifeTimeEffect);
        }

        if (Explosive)
            ExplosionDamage();


        Destroy(gameObject);
    }

	public void ActiveE()
	{
		if (Effect)
		{
			GameObject obj = (GameObject) Instantiate(Effect, transform.position, transform.rotation);
			Destroy(obj, LifeTimeEffect);
		}

		Destroy(gameObject);
	}
		
	public void WaterActive(){
		if (Effect)
		{
			GameObject obj = (GameObject) Instantiate(waterEffecet, transform.position, transform.rotation);
			Destroy(obj, LifeTimeEffect);
		}
		
		if (Explosive)
			ExplosionDamage();

		Destroy(gameObject);	
	}

    private void ExplosionDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, ExplosionRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Collider hit = hitColliders[i];
            if (!hit)
                continue;
			if(hit.name == "Destroyer" || hit.name == "PatrolBoat"){
				if (hit.transform.parent.GetComponent<DamageControl>())
				{
					hit.transform.parent.GetComponent<DamageControl>().ApplyDamage(Damage);
				}
				if (hit.transform.parent.GetComponent<Rigidbody>() && hit.transform.parent.GetComponent<Rigidbody>().isKinematic == false)
					hit.transform.parent.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, 3.0f);
			}
			else if(hit.name == "f16a" || hit.name == "HelicopterEnemy" || hit.name == "Drone" || hit.name == "f16a_off" || hit.name == "f16a_carrier"){
				if(hit.transform.GetComponent<DamageManager>())
					hit.gameObject.GetComponent<DamageManager>().ApplyDamage(Damage, gameObject);
			}
			else{
				if (hit.gameObject.GetComponent<DamageControl>())
				{
					hit.gameObject.GetComponent<DamageControl>().ApplyDamage(Damage);
				}
				if (hit.GetComponent<Rigidbody>() && hit.GetComponent<Rigidbody>().isKinematic == false){
					if(Vector3.Distance(hit.transform.position, transform.position) < 5)
						hit.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, 3.0f);
					else
						hit.GetComponent<Rigidbody>().AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius, 3.0f);
				}
			}
        }

    }

    private void NormalDamage(Collision collision)
    {
		if (collision.collider.name == "Destroyer" || collision.collider.name == "PatrolBoat") {
			if (collision.collider.transform.parent.GetComponent<DamageControl>())
				collision.collider.transform.parent.GetComponent<DamageControl>().ApplyDamage(Damage);	
		}
		else if(collision.collider.name == "f16a" || collision.collider.name == "HelicopterEnemy" || collision.collider.name == "Drone" || collision.collider.name == "f16a_off" || collision.collider.name == "f16a_carrier"){
			if(collision.gameObject.GetComponent<DamageManager>())
				collision.gameObject.GetComponent<DamageManager>().ApplyDamage(Damage, gameObject);
		}
		else{
			if (collision.gameObject.GetComponent<DamageControl>())
				collision.gameObject.GetComponent<DamageControl>().ApplyDamage(Damage);
		}

    }

    private void OnCollisionEnter(Collision collision)
    {
		if(HitedActive){
			if(collision.gameObject.tag == "water"){
				WaterActive();
			}
			else if (collision.gameObject.tag != "Particle" && collision.gameObject.tag != this.gameObject.tag)
        	{
            	if (!Explosive && !collision.gameObject.CompareTag("Player"))
//				if (!Explosive)
                	NormalDamage(collision);
				if(collision.gameObject.CompareTag("Player"))
            		ActiveE();
				else 
					Active();
        	}
		}
    }
}
