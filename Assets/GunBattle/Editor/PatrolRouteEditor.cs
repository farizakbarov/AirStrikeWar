// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PatrolRoute))]
public class PatrolRouteEditor : Editor {
	public float height=5;
	
	public override void  OnInspectorGUI (){
		PatrolRoute route = target as PatrolRoute;
		
		GUILayout.Label (route.patrolPoints.Count+" Patrol Points in Route");
		route.pingPong = EditorGUILayout.Toggle ("Ping Pong", route.pingPong);
		if (GUI.changed) {
			SceneView.RepaintAll ();
		}
		
		if (GUILayout.Button("Reverse Direction")) {
			route.patrolPoints.Reverse ();
			SceneView.RepaintAll ();
		}
		
		if (GUILayout.Button("Add Patrol Point")) {
			Selection.activeGameObject = route.InsertPatrolPointAt (route.patrolPoints.Count);
		}
		route.drawGizmos = EditorGUILayout.Toggle ("DrawGizmos", route.drawGizmos);
		route.WaypointsColor = EditorGUILayout.ColorField("WaypointsColor",route.WaypointsColor);
		
		
		if (GUILayout.Button("Choose Nearest PatrolPoint")) {
			Vector3 HandleCameraPos ;
			
			HandleCameraPos = SceneView.lastActiveSceneView.camera.transform.position;
			int j=0;
			for (int i = 1; i < route.patrolPoints.Count; i++) 
				//if (!route.patrolPoints[i]){
				if(Vector3.Distance(HandleCameraPos,route.patrolPoints[i].transform.position)
				   <
				   Vector3.Distance(HandleCameraPos,route.patrolPoints[j].transform.position))j=i;
			//}
			Selection.activeGameObject = route.patrolPoints[j].gameObject;
			SceneView.lastActiveSceneView.LookAt(route.patrolPoints[j].gameObject.transform.position,SceneView.lastActiveSceneView.camera.transform.rotation,30);
		}
		
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Update Position")) {
			for (int i = 0; i < route.patrolPoints.Count; i++) 
			{
				RaycastHit rh;				
				if(Physics.Raycast(route.patrolPoints[i].transform.position,Vector3.down,out rh,100)){
					Undo.RegisterUndo(route.patrolPoints[i].transform,"Patrol Path:"+route.gameObject.name+"->"+route.patrolPoints[i].name);
					route.patrolPoints[i].transform.position = rh.point+Vector3.up*height;
				}
			}
		}
		height = EditorGUILayout.FloatField("Fixed Height",height);
		GUILayout.EndHorizontal();
		
	}
	
	void  OnSceneGUI (){
		PatrolRoute route = target as PatrolRoute;
		
		DrawPatrolRoute (route);
	}
	
	public static void  DrawPatrolRoute ( PatrolRoute route  ){
		if (route.patrolPoints.Count == 0)
			return;
		
		Vector3 lastPoint = route.patrolPoints[0].transform.position;
		
		int loopCount= route.patrolPoints.Count;
		if (route.pingPong)
			loopCount--;
		
		for (int i = 0; i < loopCount; i++) {
			if (!route.patrolPoints[i])
				break;
			
			Vector3 newPoint= route.patrolPoints[(i + 1) % route.patrolPoints.Count].transform.position;
			if (newPoint != lastPoint) {
				Handles.color = new Color (0.5f, 0.5f, 1.0f);
				DrawPatrolArrow (lastPoint, newPoint);
				if (route.pingPong) { 
					Handles.color = new Color (1.0f, 1.0f, 1.0f, 0.2f);
					DrawPatrolArrow (newPoint, lastPoint);
				}
			}
			lastPoint = newPoint;
		}
	}
	
	static void  DrawPatrolArrow ( Vector3 a ,   Vector3 b  ){
		Quaternion directionRotation = Quaternion.LookRotation(b - a);
		Handles.ConeCap (0, (a + b) * 0.5f - directionRotation * Vector3.forward * 0.5f, directionRotation, 0.7f);
	}
}
