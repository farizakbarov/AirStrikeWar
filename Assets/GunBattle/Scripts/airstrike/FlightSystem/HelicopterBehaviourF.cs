// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class HelicopterBehaviourF : MonoBehaviour {
	
	public Transform Target;
	// Public member data
	
	public float moveSpeed = 10;
	public float angularSpeed = 0.3f;

	private Vector3 deltaPos;
	private Vector3 deltaPos1;
	private float dist = 0;
	private float distY = 0;
	private float angle = 0;
	
	public bool avoidFlag = false;
	private bool attackFlag = false;
	public float upTime = 5;
	private float initUpTime;
	
	public float turnTime = 4;
	private float initTurnTime;

	public float idleTime = 10;
	private float initIdleTime= 0;

	public float UDSpeed = 3;
	private Transform myTransform;
	private ShootGuideMissile sgm;
	public  enum RightState {
		Normal,
		Right,
		Left
	}
	public enum UpState {
		Normal,
		Up,
		Down
	}

	public enum HeliState{
		Attack,
		Idle,
		Turn,
		Avoid
	}

	public RightState rState = RightState.Normal; 
	public UpState uState = UpState.Normal;
	public HeliState heliState = HeliState.Attack;


	RaycastHit hit;
	RaycastHit hit1;
	void  Start (){
		myTransform = transform;
		sgm = myTransform.GetComponent<ShootGuideMissile> ();
		if (Target == null)
			Target = GameObject.Find ("Player").transform;
	}
	
	void  Update (){
		if (Target == null)
						return;
		deltaPos = Target.position - myTransform.position;
		deltaPos1 = new Vector3 (deltaPos.x, 0, deltaPos.z);
		dist = Vector3.Distance (myTransform.position, Target.position);
		distY = Vector3.Distance (new Vector3(myTransform.position.x, 0, myTransform.position.z), new Vector3(Target.position.x, 0, Target.position.z));
		angle = Vector3.Angle (myTransform.forward, new Vector3(Target.position.x - myTransform.position.x, 0, Target.position.z - myTransform.position.z));

	
		switch (heliState) {
		case HeliState.Attack:

			if ((myTransform.position.y - Target.position.y > 0) && (myTransform.position.y - Target.position.y < 20)) {
				myTransform.position += myTransform.up * Time.deltaTime * UDSpeed;	
			}
			else if((Target.position.y - myTransform.position.y > 0) && (Target.position.y - myTransform.position.y) < 20){
				myTransform.position -= myTransform.up * Time.deltaTime * UDSpeed;
			}

			if (Physics.Raycast (myTransform.position, myTransform.forward, out hit, 50)) {
				sgm.shootable = false;
				if(!hit.transform.CompareTag("missile") && !hit.transform.CompareTag("bullet")){
					heliState = HeliState.Avoid;
				}
			}
			else if (Physics.Raycast (myTransform.position, Vector3.down,out hit ,25)) {
				sgm.shootable = false;
				if(!hit.transform.CompareTag("bullet") && !hit.transform.CompareTag("missile")){
					heliState = HeliState.Avoid;
				}
			}
			else if(distY < 15 || angle > 60){
				sgm.shootable = false;
				heliState = HeliState.Idle;
				initIdleTime = Time.time;
			}
			else{
				deltaPos1.Normalize();
				myTransform.rotation = Quaternion.Lerp(myTransform.rotation, Quaternion.LookRotation(deltaPos1,Vector3.up), Time.deltaTime*angularSpeed);
				myTransform.position += myTransform.forward.normalized * Time.deltaTime * moveSpeed;	
				//Shoot
				transform.GetComponent<ShootGuideMissile>().shootable = true;
			}
			break;
		case HeliState.Idle:
			if ((myTransform.position.y - Target.position.y > 0) && (myTransform.position.y - Target.position.y < 20)) {
				myTransform.position += myTransform.up * Time.deltaTime * UDSpeed;	
			}
			else if((Target.position.y - transform.position.y > 0) && (Target.position.y - myTransform.position.y) < 20){
				myTransform.position -= myTransform.up * Time.deltaTime * UDSpeed;
			}
			sgm.shootable = false;
			if (Physics.Raycast (myTransform.position, myTransform.forward, out hit, 50)) {
				if(!hit.transform.CompareTag("missile") && !hit.transform.CompareTag("bullet")){
					heliState = HeliState.Avoid;
				}
			}
			else if (Physics.Raycast (transform.position, Vector3.down,out hit ,25)) {
				if(!hit.transform.CompareTag("bullet") && !hit.transform.CompareTag("missile")){
					heliState = HeliState.Avoid;
				}
			}
			else{
				if(Time.time - initIdleTime > idleTime){
					heliState = HeliState.Turn;
				}
				else{
					myTransform.position += myTransform.forward.normalized * Time.deltaTime * moveSpeed;
				}
			}
			break;
		case HeliState.Turn:
			if ((myTransform.position.y - Target.position.y > 0) && (myTransform.position.y - Target.position.y < 20)) {
				myTransform.position += myTransform.up * Time.deltaTime * UDSpeed;	
			}
			else if((Target.position.y - myTransform.position.y > 0) && (Target.position.y - myTransform.position.y) < 20){
				myTransform.position -= myTransform.up * Time.deltaTime * UDSpeed;
			}
			sgm.shootable = false;
			if (Physics.Raycast (myTransform.position, myTransform.forward, out hit, 50)) {
				if(!hit.transform.CompareTag("missile") && !hit.transform.CompareTag("bullet")){
					heliState = HeliState.Avoid;
				}
			}
			else if (Physics.Raycast (myTransform.position, Vector3.down,out hit ,25)) {
				if(!hit.transform.CompareTag("bullet") && !hit.transform.CompareTag("missile")){
					heliState = HeliState.Avoid;
				}
			}
			else {
				if(angle > 20){
					myTransform.Rotate(Vector3.up, Time.deltaTime * 50);
				}
				else {
					heliState = HeliState.Attack;
				}
			}
			break;
		case HeliState.Avoid:
			sgm.shootable = false;
			if (Physics.Raycast (myTransform.position, myTransform.forward, out hit, 50)) {
				if(!hit.transform.CompareTag("missile") && !hit.transform.CompareTag("bullet")){
					heliState = HeliState.Avoid;
					initTurnTime = Time.time;

					if(Physics.Raycast(myTransform.position, myTransform.right, out hit1, 40)){
						myTransform.Rotate(Vector3.up, Time.deltaTime * 10);
					}
					else{
						myTransform.Rotate(Vector3.up, -Time.deltaTime * 10);
					}
				}
			}
			else if (Physics.Raycast (myTransform.position, Vector3.down,out hit ,25)) {
				if(!hit.transform.CompareTag("bullet") && !hit.transform.CompareTag("missile")){
					initTurnTime = Time.time + 3;
					myTransform.position += new Vector3(0,Time.deltaTime * 5,0);
				}
			}
			else {
				if(Time.time - initTurnTime > turnTime)
					heliState = HeliState.Idle;
				else{
					myTransform.position += myTransform.forward * Time.deltaTime * moveSpeed;
				}
			}
			break;
		}
	}
}