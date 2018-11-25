using UnityEngine;
using System.Collections;

public class CameraSmoothFollow : MonoBehaviour {
//
//    public Transform target;
//    public float distance = 3.0f;
//    public float height = 3.0f;
//    public float damping = 5.0f;
//    public bool smoothRotation = true;
//    public bool followBehind = true;
//    public float rotationDamping = 10.0f;

	public GameObject Target;
	public Vector3 Offset = new Vector3(0,0,0);
	private Transform myTransform;

	void Start(){
		myTransform = transform;
	}
    void FixedUpdate()
    {
		if (Target == null)
						return;
//        Vector3 wantedPosition;
//        if (followBehind)
//            wantedPosition = target.TransformPoint(0, height, -distance);
//        else
//            wantedPosition = target.TransformPoint(0, height, distance);
//
//        transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.fixedDeltaTime * damping);
//
//        if (smoothRotation)
//        {
//            Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
//			transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.fixedDeltaTime * rotationDamping);
//        }
//        else transform.LookAt(target, target.up);
		Quaternion lookAtRotation = Quaternion.LookRotation (Target.transform.position);
		this.transform.LookAt (Target.transform.position + Target.transform.forward * Offset.x);
		Vector3 positionTarget = Target.transform.position + ((-Target.transform.forward + (Target.transform.up * Offset.y)) * Offset.z);
		float distance = Vector3.Distance (positionTarget, myTransform.position);
//		if(chtarget != true){
		myTransform.position = Vector3.Lerp (myTransform.position, positionTarget, Time.fixedDeltaTime * distance);
//		}
    }
}
