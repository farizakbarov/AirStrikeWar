using UnityEngine;
using System.Collections;

public class MoverMissile : WeaponBase
{
	public float Damping = 3;
	public float Speed = 80;
	public float SpeedMax = 80;
	public float SpeedMult = 1;
	public Vector3 Noise = new Vector3 (20, 20, 20);
	public float TargetLockDirection = 0.5f;
	public int DistanceLock = 70;
	public int DurationLock = 40;
	public bool Seeker;
	public float LifeTime = 5.0f;
	private bool locked;
	private int timetorock;
	private float timeCount = 0;
		
	private void Start ()
	{
		timeCount = Time.time;
		Destroy (gameObject, LifeTime);
	}
	
	private void FixedUpdate ()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * Speed * Time.fixedDeltaTime;
		GetComponent<Rigidbody>().velocity += new Vector3 (Random.Range (-Noise.x, Noise.x), Random.Range (-Noise.y, Noise.y), Random.Range (-Noise.z, Noise.z));
		GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	}

	private void Update ()
	{
		if (Time.time >= (timeCount + LifeTime) - 0.5f) {
			if (GetComponent<Damage> ()) {
				GetComponent<Damage> ().Active ();
			}
		}
	}
}
