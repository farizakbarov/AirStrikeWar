// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class TruckBehaviours : MonoBehaviour {
	
	
	// Public member data
	public GameObject player;
	public GameObject dustTrans;
	public PatrolRoute patrolRoute;
	public float patrolPointRadius = 0.5f;
	public float moveSpeed = 10;
	public float angularSpeed = 0.3f;
	
	// Private memeber data
	private Transform character;
	private int nextPatrolPoint = 0;
	private int patrolDirection = 1;
	public bool moveMode = true;
	void  Start (){
		character = transform;
		if (player == null) {
			player = GameObject.Find("Player").gameObject;		
		}
		if (dustTrans == null) {
			if(character.Find("TireDust") != null)
				dustTrans = character.Find("TireDust").gameObject;
		}
		if(character.parent != null)
			patrolRoute.Register (character.parent.gameObject);
		else
			patrolRoute.Register (character.gameObject);
		nextPatrolPoint = patrolRoute.GetClosestPatrolPoint (character.position);
	}
	
//	void  OnEnable (){
//		nextPatrolPoint = patrolRoute.GetClosestPatrolPoint (character.position);
//	}
//	
	void  Update (){
		if (patrolRoute == null || patrolRoute.patrolPoints.Count == 0)
			return;
		if (player == null)
						return;

		if (Vector3.Distance (player.transform.position, character.position) > 600){
			if(dustTrans != null && dustTrans.GetComponent<Renderer>().enabled == true){
				dustTrans.GetComponent<Renderer>().enabled =false;	
				dustTrans.GetComponent<ParticleEmitter>().enabled = false;
			}
			return;
		}
		if(moveMode){
			if (Physics.Raycast (character.position + character.up * 1, character.forward, 10))
						return;
		}

		if(dustTrans != null && dustTrans.GetComponent<Renderer>().enabled == false){
			dustTrans.GetComponent<Renderer>().enabled =true;	
			dustTrans.GetComponent<ParticleEmitter>().enabled = true;
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