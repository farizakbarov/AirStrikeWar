// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class HelicopterBehaviour : MonoBehaviour {
	
	public Transform Target;
	// Public member data
	
	public PatrolRoute patrolRoute;
	public float patrolPointRadius = 0.5f;
	public float moveSpeed = 10;
	public float angularSpeed = 0.3f;
	
	// Private memeber data
	private Transform character;
	private int nextPatrolPoint = 0;
	private int patrolDirection = 1;

	void  Start (){
		character = transform;
		patrolRoute.Register (character.parent.gameObject);
		if (Target == null)
			Target = GameObject.Find ("Player").gameObject.transform;

	}
	
//	void  OnEnable (){
//		nextPatrolPoint = patrolRoute.GetClosestPatrolPoint (character.position);
//	}
	
	void  OnDestroy (){
	}
	
	void  Update (){
		if (patrolRoute == null || patrolRoute.patrolPoints.Count == 0 || Target == null)
			return;
		if (Vector3.Distance (character.position, Target.position) > 600)
						return;
		if (Vector3.Distance (character.position, Target.position) < 300) {
			character.GetComponent<HelicopterBehaviourF>().enabled = true;
			this.enabled = false;
		}

		Vector3 targetVector = patrolRoute.patrolPoints[nextPatrolPoint].position - character.position;
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
		
		targetVector.Normalize ();
		
		character.rotation = Quaternion.Lerp(character.rotation,Quaternion.LookRotation(targetVector,Vector3.up), Time.deltaTime*angularSpeed);
		character.position += character.forward.normalized * Time.deltaTime * moveSpeed;	


	}
	
}