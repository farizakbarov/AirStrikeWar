// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class PatrolMoveController : MonoBehaviour {
	
	
	// Public member data
	
	public PatrolRoute patrolRoute;
	public float patrolPointRadius = 0.5f;
	public float moveSpeed = 10;
	public float angularSpeed = 0.3f;
	
	// Private memeber data
	private Transform character;
	private int nextPatrolPoint = 0;
	private int patrolDirection = 1;
	//private	DestroyME myHealth;
	void  Start (){
		character = transform;
		if(transform.parent != null)
			patrolRoute.Register (transform.parent.gameObject);
		else 
			patrolRoute.Register(transform.gameObject);
		nextPatrolPoint = patrolRoute.GetClosestPatrolPoint (character.position);
		//	if(gameObject.name!="patrolShip")
		//		myHealth= gameObject.GetComponent<DestroyME>() as DestroyME;
		//	else
		//		myHealth=gameObject.transform.GetComponentInChildren<DestroyME>() as DestroyME;
	}
	
//	void  OnEnable (){
//		nextPatrolPoint = patrolRoute.GetClosestPatrolPoint (character.position);
//	}
	
	void  OnDestroy (){
		//	patrolRoute.UnRegister (transform.parent.gameObject);
	}
	
	void  Update (){
		// Early out if there are no patrol points
		if (patrolRoute == null || patrolRoute.patrolPoints.Count == 0)
			return;
		
		// Find the vector towards the next patrol point.
		Vector3 targetVector = patrolRoute.patrolPoints[nextPatrolPoint].position - character.position;
//		if(gameObject.tag != "enemyHeli") targetVector.y = 0;
		
		// If the patrol point has been reached, select the next one.
		if (targetVector.sqrMagnitude < patrolPointRadius * patrolPointRadius) {
			nextPatrolPoint += patrolDirection;
			if (nextPatrolPoint < 0) {
				nextPatrolPoint = 1;
				patrolDirection = 1;
			}
			if (nextPatrolPoint >= patrolRoute.patrolPoints.Count) {
				if (patrolRoute.pingPong) {
					patrolDirection = -1;
					nextPatrolPoint = patrolRoute.patrolPoints.Count - 2;
				}
				else {
					nextPatrolPoint = 0;
				}
			}
		}
		
		// Make sure the target vector doesn't exceed a length if one
		//	if (targetVector.sqrMagnitude > 1)
		targetVector.Normalize ();
		
		character.rotation = Quaternion.Lerp(character.rotation,Quaternion.LookRotation(targetVector,Vector3.up), Time.deltaTime*angularSpeed);
		//	if(myHealth.health <= 0 && moveSpeed > 0)	moveSpeed -= 0.05f;	
//		if(gameObject.tag == "enemyHeli"){
//			Vector3 heli_forward;
//			heli_forward = character.forward.normalized;
//			//		character.rotation.eulerAngles.x *= -1;
//			character.position += heli_forward * Time.deltaTime * moveSpeed;
//		}else{
			character.position += character.forward.normalized * Time.deltaTime * moveSpeed;
//		}
		//	// Set the movement direction.
		//	motor.movementDirection = targetVector;
		//	// Set the facing direction.
		//	motor.facingDirection = targetVector;
	}
	
}