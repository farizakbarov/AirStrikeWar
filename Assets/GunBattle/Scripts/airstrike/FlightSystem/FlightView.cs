using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]

public class FlightView : MonoBehaviour
{

	public GameObject Target;
	public bool chtarget = false;
	public GameObject[] Cameras;
	private int indexCamera;
	public Vector3 Offset = new Vector3 (10, 0.5f, 10);
	public bool chcameraflag = false;
}
