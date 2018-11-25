// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using UnityEditor;
//using System.Collections;
[CustomEditor(typeof(PatrolPoint))]
public class PatrolPointEditor : Editor {
	public override void  OnInspectorGUI (){
		PatrolPoint point = target as PatrolPoint;
		PatrolRoute route = point.transform.parent.GetComponent<PatrolRoute>();
		int thisIndex = route.GetIndexOfPatrolPoint (point);
		
		if (GUILayout.Button ("Remove This Patrol Point")) {
			route.RemovePatrolPointAt (thisIndex);
			int newSelectionIndex = Mathf.Clamp (thisIndex, 0, route.patrolPoints.Count - 1);
			Selection.activeGameObject = route.patrolPoints[newSelectionIndex].gameObject;
		}
		if (GUILayout.Button ("Insert Patrol Point Before")) {
			Selection.activeGameObject = route.InsertPatrolPointAt (thisIndex);
		}
		if (GUILayout.Button ("Insert Patrol Point After")) {
			Selection.activeGameObject = route.InsertPatrolPointAt (thisIndex + 1);
		}
		if (GUILayout.Button ("Select Patrol Point After")) {
			Selection.activeGameObject = route.patrolPoints[(thisIndex + 1)%route.patrolPoints.Count].gameObject;
		}
		if (GUILayout.Button ("Select Patrol Point Before")) {
			Selection.activeGameObject = route.patrolPoints[(thisIndex - 1+route.patrolPoints.Count)%route.patrolPoints.Count].gameObject;
		}
		if (GUILayout.Button ("Select Parent")) {
			Selection.activeGameObject = route.patrolPoints[thisIndex].gameObject.transform.parent.gameObject;
		}
	}
	
	void  OnSceneGUI (){
		PatrolPoint point = target as PatrolPoint;
		PatrolRoute route = point.transform.parent.GetComponent<PatrolRoute>();
		
		PatrolRouteEditor.DrawPatrolRoute (route);
	}
}
