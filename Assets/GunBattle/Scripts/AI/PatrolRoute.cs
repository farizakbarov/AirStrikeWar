// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class PatrolRoute : MonoBehaviour {

	public bool  pingPong = false;

	public System.Collections.Generic.List<PatrolPoint> patrolPoints = new System.Collections.Generic.List<PatrolPoint> ();
	
	public Color WaypointsColor = new Color(1,0,0,1);
	public bool  drawGizmos = true;
	
	private System.Collections.Generic.List<GameObject> activePatrollers = new System.Collections.Generic.List<GameObject> ();
	
	public void  Register ( GameObject go  ){
		activePatrollers.Add (go);
	}
	
	public void  UnRegister ( GameObject go  ){
		activePatrollers.Remove (go);
	}
	
	public int GetClosestPatrolPoint ( Vector3 pos  ){
		if (patrolPoints.Count == 0)
			return 0;
		
		float shortestDist = Mathf.Infinity;
		int shortestIndex = 0;
		for (int i = 0; i < patrolPoints.Count; i++) {
			patrolPoints[i].position = patrolPoints[i].transform.position;
			float dist = (patrolPoints[i].position - pos).sqrMagnitude;
			if (dist < shortestDist) {
				shortestDist = dist;
				shortestIndex = i;
			}
		}
		
		// If going towards the closest point makes us go in the wrong direction,
		// choose the next point instead.
		if (!pingPong || shortestIndex < patrolPoints.Count - 1) {
			int nextIndex = (shortestIndex + 1) % patrolPoints.Count;
			float angle = Vector3.Angle (
				patrolPoints[nextIndex].position - patrolPoints[shortestIndex].position,
				patrolPoints[shortestIndex].position - pos
				);
			if (angle > 120)
				shortestIndex = nextIndex;
		}
		
		return shortestIndex;
	}
	
	public void  OnDrawGizmos (){
		if (patrolPoints.Count == 0 || drawGizmos == false)
			return;
		
		
		Vector3 lastPoint = patrolPoints[0].transform.position;
		int loopCount= patrolPoints.Count;
		if (pingPong)
			loopCount--;
		for (int i = 0; i < loopCount; i++) {
			if (!patrolPoints[i])
				break;
			Vector3 newPoint= patrolPoints[(i + 1) % patrolPoints.Count].transform.position;
			Gizmos.color = new Color (0.5f, 0.5f, 1.0f, 1.0f);
			Gizmos.DrawLine (lastPoint, newPoint);
			lastPoint = newPoint;
			
			Gizmos.DrawSphere( patrolPoints[i].transform.position, 0.5f );
			Gizmos.color = WaypointsColor;
			Gizmos.DrawWireSphere ( patrolPoints[i].transform.position, 3.0f );
		}
	}
	
	public int GetIndexOfPatrolPoint ( PatrolPoint point  ){
		for (int i = 0; i < patrolPoints.Count; i++) {
			if (patrolPoints[i] == point)
				return i;
		}
		return -1;
	}
	
	public GameObject InsertPatrolPointAt ( int index  ){
		GameObject go = new GameObject ("PatrolPoint"+index, typeof(PatrolPoint));
		go.transform.parent = transform;
		int count = patrolPoints.Count;
		
		if (count == 0) {
			go.transform.localPosition = Vector3.zero;
			patrolPoints.Add(go.GetComponent<PatrolPoint>());
		}
		else {
			if (!pingPong || (index > 0 && index < count) || count < 2) {
				index = index % count;
				int prevIndex = index - 1;
				if (prevIndex < 0)
					prevIndex += count;
				
				go.transform.position = (
					patrolPoints[prevIndex].transform.position
					+ patrolPoints[index].transform.position
					) * 0.5f;
			}
			else if (index == 0) {
				go.transform.position = (
					patrolPoints[0].transform.position * 2
					- patrolPoints[1].transform.position
					);
			}
			else {
				go.transform.position = (
					patrolPoints[count-1].transform.position * 2
					- patrolPoints[count-2].transform.position
					);
			}
			patrolPoints.Insert(index, go.GetComponent<PatrolPoint>());
		}
		
		return go;
	}
	
	public void  RemovePatrolPointAt ( int index  ){
		GameObject go = patrolPoints[index].gameObject;
		patrolPoints.RemoveAt (index);
		DestroyImmediate (go);
	}
	
}