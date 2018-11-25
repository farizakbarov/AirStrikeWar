using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(BoxCollider))]
//[RequireComponent(typeof(DamageManager))]
//[RequireComponent(typeof(WeaponController))]


public class FlightSystem : MonoBehaviour {
	public float Speed = 50.0f;
	public float SpeedMax = 1000;
	private float MoveSpeed = 100;
	public float RotationSpeed = 0.2f;
	
	public float SpeedYaw = 1;
	public float DampingTarget = 10.0f;
	public bool Sangsung;
	
	public static float speed =2000.0f;     
	[HideInInspector]
	public bool FollowTarget = false;
	[HideInInspector]
	public Vector3 PositionTarget = Vector3.zero;
	[HideInInspector]
	public DamageManager DamageManage;
	[HideInInspector]
	public WeaponController WeaponControl;
	
	private Vector3 positionTarget = Vector3.zero;
	private Quaternion mainRot = Quaternion.identity;

	[HideInInspector]
	public float yaw = 0;
	public float rspeed=0,rely=1,relx=10;

	private Transform myTransform;
	private Rigidbody myRigidbody;
	private float rotx,roty,rotz;
	void Start () 
	{
		DamageManage = this.gameObject.GetComponent<DamageManager>();
		WeaponControl = this.gameObject.GetComponent<WeaponController>();
		myRigidbody = GetComponent<Rigidbody>();
		myTransform = transform;
	}
	void FixedUpdate()
	{
		if (Sangsung == false) {
			if (myRigidbody == null)
				return;
			myRigidbody.WakeUp();
			
		} else {
			myRigidbody.Sleep();	
		}

		Quaternion AddRot = Quaternion.identity;
		Vector3 velocityTarget = Vector3.zero;
		
		if(Sangsung == false){
			if (FollowTarget) {
				
				positionTarget = Vector3.Lerp (positionTarget, PositionTarget, Time.fixedDeltaTime * DampingTarget);
				Vector3 relativePoint = myTransform.InverseTransformPoint (positionTarget).normalized;
				
				mainRot = Quaternion.LookRotation (positionTarget - myTransform.position);
				
				myRigidbody.rotation = Quaternion.Lerp (myRigidbody.rotation, mainRot, Time.fixedDeltaTime * RotationSpeed);
				
				this.myRigidbody.rotation *= Quaternion.Euler (-relativePoint.y * rely, 0, -relativePoint.x * relx);
				
				velocityTarget = (myRigidbody.rotation * Vector3.forward) * (Speed + MoveSpeed);
			}
		}else{
			myTransform.Translate(myTransform.forward*40 * Time.deltaTime,Space.World);
		}
		
		if (Sangsung == false) {
			myRigidbody.velocity = Vector3.Lerp (myRigidbody.velocity, velocityTarget, Time.fixedDeltaTime * 10);

			yaw = Mathf.Lerp (yaw, 0, Time.deltaTime);
			MoveSpeed = Mathf.Lerp (MoveSpeed, Speed, Time.deltaTime);
		} 
	}
	
	public void SpeedUp(){
		MoveSpeed = Mathf.Lerp(MoveSpeed,SpeedMax,Time.deltaTime * 10);
	}
}
