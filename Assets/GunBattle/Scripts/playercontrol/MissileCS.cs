using UnityEngine;

public class MissileCS : MonoBehaviour 
{
	public GameObject 		missileObj;
	
	public AudioClip		shotSound;
	
	public AudioClip		emptySound;
	
	public AudioClip		reloadSound;
	
	private bool			isFireing;	
	public float			shotInterval = 0.175f;	
	
	private float			lastShotTime;	
	
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
				
				if ((this.missileObj != null) )
				{
					GameObject.Instantiate(missileObj, bulletpos[0].position, transform.rotation);
					GameObject.Instantiate(missileObj, bulletpos[1].position, transform.rotation);
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
