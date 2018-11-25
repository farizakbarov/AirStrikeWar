using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
[RequireComponent(typeof(AudioSource))]

public class WeaponLauncher : WeaponBase
{
	public bool pl = false;
//	public string WeaponName;
	public Transform[] MissileOuter;
	public GameObject Missile;
	public float FireRate = 0.1f;
	public float Spread = 0f;
	public float ForceShoot = 8000;
	public int NumBullet = 1;
	public int Ammo = 10;
	public int AmmoMax = 10;
	public bool InfinityAmmo = false;
	public float ReloadTime = 1;
	public bool ShowHUD = true;
	public Texture2D TargetLockOnTexture;
	public Texture2D TargetLockedTexture;
	public float DistanceLock = 200;
	public float TimeToLock = 2;
	public float AimDirection = 0.8f;
	public bool Seeker;
	public GameObject Shell;
	public float ShellLifeTime = 4;
	public Transform[] ShellOuter;
	public int ShellOutForce = 300;
	public AudioClip[] SoundGun;
	public AudioClip SoundReloading;
	public AudioClip SoundReloaded;
	private float timetolockcount = 0;
	private float nextFireTime = 0;
	private GameObject target;
	private Vector3 torqueTemp;
	private float reloadTimeTemp;
	private AudioSource audio;
	[HideInInspector]
	public bool Reloading;
	[HideInInspector]
	public float ReloadingProcess;

	private LeaveBullets leaveBullet;
	private GameObject player;
	private Transform camTransform;
	private void Start ()
	{
		if (!Owner) 
			Owner = this.transform.root.gameObject;
		
		if(!audio)
			audio = this.GetComponent<AudioSource>();
		
		if(!audio)
			this.gameObject.AddComponent<AudioSource>();

		if (transform.parent.parent.name == "Player") {
			player = transform.parent.parent.gameObject;	
		}
		else {
			player = null;
		}

		camTransform = Camera.main.transform;

		leaveBullet = GameObject.Find ("GUI").GetComponent<LeaveBullets> ();
	}

	private void Update ()
	{
		if (TorqueObject) {
			TorqueObject.transform.Rotate (torqueTemp * Time.deltaTime);
			torqueTemp = Vector3.Lerp (torqueTemp, Vector3.zero, Time.deltaTime);
		}

		if (Seeker) {
			for (int t=0; t<TargetTag.Length; t++) {
				GameObject[] objs = GameObject.FindGameObjectsWithTag (TargetTag [t]);
				if(objs.Length>0)
				{
					float distance = int.MaxValue;
					for (int i = 0; i < objs.Length; i++) {
						Vector3 dir = (objs [i].transform.position - transform.position).normalized;
						float direction = Vector3.Dot (dir, transform.forward);
						float dis = Vector3.Distance (objs [i].transform.position, transform.position);
						if (direction >= AimDirection) {
							if (DistanceLock > dis) {
								if (distance > dis) {
									if (timetolockcount + TimeToLock < Time.time) {	
										distance = dis;
										target = objs [i];
									}
								}
							}
						}
					}
				}
			}
		}

		if (target) {
			float targetdistance = Vector3.Distance (transform.position, target.transform.position);
			Vector3 dir = (target.transform.position - transform.position).normalized;
			float direction = Vector3.Dot (dir, transform.forward);

			if (targetdistance > DistanceLock || direction <= AimDirection) {
				Unlock ();
			}
		}
		
		if (Reloading) {
			ReloadingProcess = ((1 / ReloadTime) * (reloadTimeTemp + ReloadTime - Time.time));
			if (Time.time >= reloadTimeTemp + ReloadTime) {
				Reloading = false;
				if (audio && SoundReloaded)
					audio.PlayOneShot(SoundReloaded);
				Ammo = AmmoMax;
			}
		} else {
			if (Ammo <= 0) {
				Unlock ();
				Reloading = true;
				reloadTimeTemp = Time.time;
				if (audio && SoundReloading)
					audio.PlayOneShot (SoundReloading);
			}
		}

	}

	private void DrawTargetLockon (Transform aimtarget, bool locked)
	{
		if (!ShowHUD)
			return;

		Vector3 dir = (aimtarget.position - camTransform.position).normalized;
		float direction = Vector3.Dot (dir, camTransform.forward);
//		if (direction > 0.9f) {
			Vector3 screenPos = camTransform.GetComponent<Camera>().WorldToScreenPoint (aimtarget.transform.position);
			float distance = Vector3.Distance (transform.position, aimtarget.transform.position);
			if (locked) {
				if (TargetLockedTexture)
					GUI.DrawTexture (new Rect (screenPos.x - TargetLockedTexture.width / 2, Screen.height - screenPos.y - TargetLockedTexture.height / 2, TargetLockedTexture.width, TargetLockedTexture.height), TargetLockedTexture);
//				GUI.Label (new Rect (screenPos.x + 40, Screen.height - screenPos.y, 200, 30), aimtarget.name + " " + Mathf.Floor (distance) + "m.");
			} else {
				if (TargetLockOnTexture)
					GUI.DrawTexture (new Rect (screenPos.x - TargetLockOnTexture.width / 2, Screen.height - screenPos.y - TargetLockOnTexture.height / 2, TargetLockOnTexture.width, TargetLockOnTexture.height), TargetLockOnTexture);
			}
    	
//		}

	}

	private void OnGUI ()
	{
		if (Seeker) {
           
			if (target) {
				DrawTargetLockon (target.transform, true);
			}
            
			for (int t=0; t<TargetTag.Length; t++) {
				if (GameObject.FindGameObjectsWithTag (TargetTag [t]).Length > 0) {
					GameObject[] objs = GameObject.FindGameObjectsWithTag (TargetTag [t]);
					for (int i = 0; i < objs.Length; i++) {
						if (objs [i]) {
							Vector3 dir = (objs [i].transform.position - transform.position).normalized;
							float direction = Vector3.Dot (dir, transform.forward);
							if (direction >= AimDirection - 0.1f) {
								float dis = Vector3.Distance (objs [i].transform.position, transform.position);
								if (DistanceLock > dis) {
									DrawTargetLockon (objs [i].transform, false);
								}
							}
						}
					}
				}
			}
		}

	}

	private void Unlock ()
	{
		timetolockcount = Time.time;
		target = null;
	}
	
	private int currentOuter = 0;

	public void Shoot ()
	{
		if (InfinityAmmo) {
			Ammo = 1;	
		}
		if (Ammo > 0) {
			if (Time.time > nextFireTime + FireRate) {
				nextFireTime += FireRate;
				switch (gameObject.name){
				case "Missile":
					if(GlobalGameState.c_missileCount <= 0) return;
					break;
				case "Bullet":
					if(GlobalGameState.c_bulletCount <= 0) return;
					break;
				case "GuideMissile":
					if(GlobalGameState.c_guideMissileCount <= 0) return;
					break;
				}
				nextFireTime = Time.time;
				torqueTemp = TorqueSpeedAxis;
				Ammo -= 1;

				if(pl == true){
					Debug.Log("NAPAM");
					for (int i = 0; i < NumBullet; i++) {

						if (Missile) {
							Vector3 spread = new Vector3 (Random.Range (-Spread, Spread), Random.Range (-Spread, Spread), Random.Range (-Spread, Spread)) / 100;
							Vector3 direction = this.transform.forward +spread*0.2f;

							for(int j=0;j<2;j++){
							GameObject bullet = (GameObject)Instantiate (Missile, MissileOuter[j].position, MissileOuter[j].rotation);
								if (bullet.GetComponent<DamageBase> ()) {
									bullet.GetComponent<DamageBase> ().Owner = Owner;
								}
								if (bullet.GetComponent<WeaponBase> ()) {
									bullet.GetComponent<WeaponBase> ().Owner = Owner;
								}
								bullet.transform.forward = direction;
								if (RigidbodyProjectile) {
									if (bullet.GetComponent<Rigidbody>()) {
										if (Owner != null && Owner.GetComponent<Rigidbody>()) {
											bullet.GetComponent<Rigidbody>().velocity = Owner.GetComponent<Rigidbody>().velocity;
										}
									}
								}
							}
							
						}
					}
				}			

				if (Shell) {
					Vector3 direct = Vector3.zero;
					if(player != null){
						Ray ray = Camera.main.ScreenPointToRay(new Vector3(player.GetComponent<Indicator>().focus.x, player.GetComponent<Indicator>().focus.y, 0));
						RaycastHit hitray;

						if(Physics.Raycast(ray, out hitray, 2000)){
							direct = hitray.point - transform.position;
						}
						else{
							direct = ray.GetPoint(1000) - transform.position;
						}

					}

					if(gameObject.name == "GuideMissile"){
						GameObject shell = (GameObject)Instantiate (Shell, ShellOuter[0].position, Quaternion.LookRotation(direct));
						shell.GetComponent<MoverMYMissile>().Target = target;

						GameObject.Destroy (shell.gameObject, ShellLifeTime);
						if (shell.GetComponent<Rigidbody>()) {
							shell.GetComponent<Rigidbody>().AddForce (direct * ShellOutForce);
						}

						GlobalGameState.c_guideMissileCount -= 1;
						if(GlobalGameState.c_guideMissileCount <= 0) 
							GlobalGameState.c_guideMissileCount = 0;
						leaveBullet.SetBulletCount(3, GlobalGameState.c_guideMissileCount);
					}
					else{
						for(int i=0;i<ShellOuter.Length;i++){
							if(player == null){
								GameObject shell = (GameObject)Instantiate (Shell, ShellOuter[i].position, ShellOuter[i].rotation);
								GameObject.Destroy (shell.gameObject, ShellLifeTime);
								if (shell.GetComponent<Rigidbody>()) {
									shell.GetComponent<Rigidbody>().AddForce (ShellOuter[i].forward * ShellOutForce);
								}
							}
							else{
								GameObject shell = (GameObject)Instantiate (Shell, ShellOuter[i].position, Quaternion.LookRotation(direct));
								GameObject.Destroy (shell.gameObject, ShellLifeTime);
								if (shell.GetComponent<Rigidbody>()) {
									shell.GetComponent<Rigidbody>().AddForce (direct * ShellOutForce);
								}
								if(gameObject.name == "Bullet"){
									GlobalGameState.c_bulletCount -= 1;
									if(GlobalGameState.c_bulletCount <= 0) 
										GlobalGameState.c_bulletCount = 0;
									leaveBullet.SetBulletCount(1, GlobalGameState.c_bulletCount);
								}
								else if(gameObject.name == "Missile"){
									GlobalGameState.c_missileCount -= 1;
									if(GlobalGameState.c_missileCount <= 0) 
										GlobalGameState.c_missileCount = 0;
									leaveBullet.SetBulletCount(2, GlobalGameState.c_missileCount);
								}
							}
						}
					}
				}
			
				if (audio && SoundGun.Length > 0) {
					audio.PlayOneShot (SoundGun [Random.Range (0, SoundGun.Length)]);
				}
			}
		}
	} 
}
