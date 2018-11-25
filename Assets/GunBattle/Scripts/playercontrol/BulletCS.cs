using UnityEngine;

public class BulletCS : MonoBehaviour 
{
	public GameObject 		bullet;
	
	public AudioClip		shotSound;
	
	public AudioClip		emptySound;
	
	public AudioClip		reloadSound;
	
	private bool			isFireing;	
	public float			shotInterval = 0.175f;	
	
	private float			lastShotTime;	
	public float 			spread;
	public float 			forceShoot;
	
	public bool				unlimitedAmmo	= false;
	public int 				bulletCapacity 	= 40,
	bulletCount		= 40;
	
	public Transform[] bulletpos;
	// --------------------
	private void Start()
	{
		this.isFireing = false;
	}
	
	
	// -----------------------
	public void SetTriggerState(bool fire)
	{	
		if (fire != this.isFireing)
		{
			this.isFireing = fire;
			
			if (fire)
			{
				// Fire first bullet...
				
				this.FireBullet();	
			}
			else
			{
			}
		}
	}
	
	
	
	// --------------------
	private void FixedUpdate()
	{
		//#if UNITY_EDITOR
		//this.SetTriggerState(Input.GetKey(KeyCode.F));
		//#endif
		
		if (this.isFireing)
			this.FireBullet();
		
	}
	
	
	
	// ------------------	
	public void Reload()
	{
		this.bulletCount = this.bulletCapacity;
		
		if ((this.GetComponent<AudioSource>() != null) && (this.reloadSound != null))
		{
			this.GetComponent<AudioSource>().loop = false;
			this.GetComponent<AudioSource>().PlayOneShot(this.reloadSound);
		}
	}
	
	
	// ---------------------
	private void FireBullet()
	{
		if ((Time.time - this.lastShotTime) >= this.shotInterval)
		{
			this.lastShotTime = Time.time;
			
			
			// Shoot...
			
			
			if (this.unlimitedAmmo || (this.bulletCount > 0))
			{
				if (!this.unlimitedAmmo)
					--this.bulletCount;	
				
				// Emit particles...
				
				if ((this.bullet != null) )
				{
					Vector3 Spread = new Vector3 (Random.Range (-spread, spread), Random.Range (-spread, spread), Random.Range (-spread, spread)) / 100;
					Vector3 direction = this.transform.forward +Spread*0.2f;

					for(int j=0;j<2;j++){
						GameObject bullet0 = (GameObject)Instantiate (bullet, bulletpos[j].position, bulletpos[j].rotation);
						
						if (bullet0.GetComponent<DamageBase> ()) {
							bullet0.GetComponent<DamageBase> ().Owner = this.transform.root.gameObject;
						}
						if (bullet0.GetComponent<WeaponBase> ()) {
							bullet0.GetComponent<WeaponBase> ().Owner = this.transform.root.gameObject;
//							bullet0.GetComponent<WeaponBase> ().Target = target;
						}
						bullet0.transform.forward = direction;
						if (bullet0.GetComponent<Rigidbody>()) {
							if (this.transform.root.gameObject.GetComponent<Rigidbody>()) {
								bullet0.GetComponent<Rigidbody>().velocity = this.transform.root.gameObject.GetComponent<Rigidbody>().velocity;
							}
							bullet0.GetComponent<Rigidbody>().AddForce (direction * forceShoot);	
							}
					}

//					GameObject.Instantiate(bullet, transform.position, transform.rotation);
				}
				
				
				// Play sound...
				
				if ((this.GetComponent<AudioSource>() != null) && (this.shotSound != null)) // && (!this.shotSoundLooped))
				{	
					this.GetComponent<AudioSource>().loop = false;
					this.GetComponent<AudioSource>().PlayOneShot(this.shotSound);	
				}
			}
			
			// No bullets left!!
			
			else
			{
				// Play sound...
				
				if ((this.GetComponent<AudioSource>() != null) && (this.emptySound != null)) // && (!this.emptySoundLooped))
				{	
					this.GetComponent<AudioSource>().loop = false;
					this.GetComponent<AudioSource>().PlayOneShot(this.emptySound);	
				}
			}
			
		}
	}
}
