using UnityEngine;
using System.Collections;

public enum AIState
{
	Idle,
	Patrol,
	Attacking,
	TurnPosition,
	FlyUP,
}

public enum TargetBehavior
{
	Static,
	Moving,
	Flying,
}

public class AIController : MonoBehaviour
{
	public bool attakingflag = false ;
	public string[] TargetTag;
	public GameObject Target;
	public float TimeToLock;
	public float AttackDirection = 0.5f;
	public float DistanceLock = float.MaxValue;
	public float DistanceAttack = 300;
	public float FlyDistance = 10000;
	public Transform CenterOfBattle;
	public AIState AIstate = AIState.Patrol;
	[HideInInspector]
	public int WeaponSelected = 0;
	public int AttackRate = 80;
	private float timestatetemp;
	private float timetolockcount;
	private FlightSystem flight;
	private bool attacking , Sang360 = false ,Urofalg = false;
	private Vector3 directionTurn;
	private TargetBehavior targetHavior;
	private Vector3 targetpositionTemp;
	private float buho =1,SangValuex , SangValuey, SangValuez;
	public 	bool Gunfirfalg = false,timefalg = false;
	private Transform player;
	private Transform myTransform;

	void Start ()
	{
		timetolockcount = Time.time;
		flight = this.GetComponent<FlightSystem> ();
		timestatetemp = 0;
		player = GameObject.Find ("Player").transform;
		myTransform = transform;
	}
	
	void TargetBehaviorCal ()
	{
		if (Target) {
			Vector3 delta = (targetpositionTemp - Target.transform.position);
			float deltaheight = Mathf.Abs (targetpositionTemp.y - Target.transform.position.y); 
			targetpositionTemp = Target.transform.position;
			
			if (delta == Vector3.zero) {
				targetHavior = TargetBehavior.Static;	
			} else {
				targetHavior = TargetBehavior.Moving;
				if (deltaheight > 0.5f) {
					targetHavior = TargetBehavior.Flying;	
				}
			}
		}
	}
	
	private float dot;
	private Vector3 battleposition;
	
	void Update ()
	{
		if (player != null && flight.enabled == false) {
			if(Vector3.Distance(player.position, myTransform.position) < 900){
				flight.enabled = true;
			}		
		}
		if (player != null && Vector3.Distance (player.position, myTransform.position) > 1000) {
			myTransform.LookAt(player.position);		
		}
		RaycastHit hit;
		if (Physics.Raycast (myTransform.position, myTransform.forward,out hit ,100)) {
			if(hit.transform != null && !hit.transform.CompareTag("missile") && !hit.transform.CompareTag("bullet")){
				Urofalg = true;
			}
		}
//		else if (Physics.Raycast (myTransform.position, -myTransform.up,out hit ,50)) {
//			if(hit.transform != null && !hit.transform.CompareTag("bullet") && !hit.transform.CompareTag("missile")){
//				Urofalg = true;
//			}
//		}
		else if (myTransform.position.y < 100) {
			Urofalg = true;		
		}
		if (Physics.Raycast (myTransform.position, myTransform.forward,out hit ,3)) {
			if(hit.transform.name != "Player"){
				flight.Sangsung = false;
			}
		}


		if(Urofalg==true){
			AIstate = AIState.FlyUP;
			Urofalg = false;
			timefalg = true;
			SangValuex = myTransform.eulerAngles.x;
			SangValuey = myTransform.eulerAngles.y;
			SangValuez = myTransform.eulerAngles.z;
		
			flight.Sangsung = true ;
			Sang360 = false;
		}
		if(attakingflag == true && AIstate != AIState.Attacking){
			AIstate = AIState.Attacking;
			flight.SpeedUp();
			attakingflag = false;

		}else if(attakingflag == true && AIstate == AIState.Attacking){
			AIstate = AIState.Patrol;
//			for (int t = 0; t<TargetTag.Length; t++) {
//			GameObject[] objqs = GameObject.FindGameObjectsWithTag (TargetTag [t]);
//				Target = objqs[Random.Range(0,TargetTag.Length)];
//			}
			if(player != null)
				Target = player.gameObject;
			attakingflag = false;
		}

		if (!flight)
			return;
		 
		battleposition = flight.transform.position;   
		TargetBehaviorCal ();

		switch (AIstate) {
		case AIState.Patrol:  
			if(player != null){
				float distance = int.MaxValue;
				if (timetolockcount + TimeToLock < Time.time) {
					float dis = Vector3.Distance (player.position, myTransform.position);
					if (DistanceLock > dis) {   //DistanceLock = 3.402823E+38
						if (!Target) {
							if (distance > dis && Random.Range (0, 100) > 80) {
								distance = dis;
								Target = player.gameObject;   
								flight.FollowTarget = true;  
								AIstate = AIState.Attacking;
								timestatetemp = Time.time;
								WeaponSelected = Random.Range (0, flight.WeaponControl.WeaponLists.Length);
							}
						}
					}
				}
				shootTarget (player.position);
			}else {
				AIstate = AIState.TurnPosition;
			}
			break;
		case AIState.Idle:
			if (Vector3.Distance (flight.PositionTarget, myTransform.position) <= FlyDistance) {
				AIstate = AIState.Patrol;
				timestatetemp = Time.time;
			}
			
			break;
		case AIState.Attacking:
			if (Target) {
				flight.relx = 0.3f;
				flight.rely = 0.3f;
				flight.RotationSpeed = 1;
				flight.PositionTarget = Target.transform.position;
				if (!shootTarget (flight.PositionTarget)) {
					if (attacking) {
						if (Time.time > timestatetemp + 10) {
							turnPosition ();
						}	
					} else {
						if (Time.time > timestatetemp + 15) {
							turnPosition ();
						}		
					}
				}
				
			} else {
				AIstate = AIState.Patrol;
				timestatetemp = Time.time;
			}
			if (Vector3.Distance (battleposition, myTransform.position) > FlyDistance) {
				gotoCenter ();
			}
			break;
		case AIState.TurnPosition:
			flight.relx = 0.5f;
			flight.rely = 0.5f;
			flight.RotationSpeed = 0.2f;

			if (Time.time > timestatetemp + Random.Range(25,30)) { 
				timestatetemp = Time.time;
				AIstate = AIState.Attacking;
			}
			if (Vector3.Distance (battleposition, myTransform.position) > FlyDistance) {
				gotoCenter ();
			}

			float height = flight.PositionTarget.y;
			if (targetHavior == TargetBehavior.Static) {
				directionTurn.y = 0;
				flight.PositionTarget += (myTransform.forward + directionTurn);
				if(Target != null){
					int b = Random.Range(0,2);
					if(b == 0){
						buho = 1;
					}else{
						buho = -1;
					}
				}
			} else {
				flight.PositionTarget += (myTransform.forward + directionTurn);  
			}
		break;
		case AIState.FlyUP:
			SangValuez = Mathf.Lerp(SangValuez , 0 ,Time.deltaTime*0.5f);
			if(SangValuex>0&&SangValuex<90&&Sang360 == false){
				SangValuex = Mathf.Lerp(SangValuex , 0 , Time.deltaTime*2);
				if(SangValuex>0&&SangValuex<0.3f){
					SangValuex = 359.9f;
					Sang360 = true ;
				}
			}else if(SangValuex <= 360 && SangValuex>300&&Sang360 == false){
				Sang360 = true;
			}
			if(Sang360 == true){
				SangValuex = Mathf.Lerp(SangValuex , 340 , Time.deltaTime*1);

			}
			myTransform.eulerAngles = new Vector3(SangValuex,myTransform.eulerAngles.y,SangValuez);

			if(myTransform.position.y>150){
				AIstate= AIState.TurnPosition;
				flight.relx = 1;
				flight.rely = 1;
				flight.RotationSpeed = 1;

				flight.Sangsung = false;
			}
			break;
		}

	}
	
	bool shootTarget (Vector3 targetPos)
	{
		Vector3 dir = (targetPos - myTransform.position).normalized;
		dot = Vector3.Dot (dir, myTransform.forward);
	
		float distance = Vector3.Distance (targetPos, myTransform.position);	
		if (distance <= DistanceAttack) { // DistanceAttack = 300
			if (dot >= AttackDirection) { //attackDirection = 0.5
				attacking = true;
				if (Random.Range (0, 100) > AttackRate) {   //attackrate = 80
					Gunfirfalg = true;
					flight.WeaponControl.LaunchWeapon (WeaponSelected);  //Shoot

				}else{
					Gunfirfalg = false;
				}
				if (distance < DistanceAttack /3) {
					turnPosition ();
					flight.rspeed = 1;
					Gunfirfalg = false;
				}
			}
		} else {

			flight.SpeedUp ();
		}
		return true;
	}

	void turnPosition ()
	{
		directionTurn = new Vector3(Random.Range(-2,1)+0.2f,Random.Range(-2,2)+0.2f,Random.Range(-2,1)+0.2f);
		AIstate = AIState.TurnPosition;
		timestatetemp = Time.time;
		attacking = false;
	}

	void gotoCenter ()
	{
		flight.PositionTarget = battleposition;	
		timestatetemp = Time.time;
		Target = null;
		AIstate = AIState.Idle;	
	}
}
