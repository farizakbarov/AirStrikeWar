using UnityEngine;
using System.Collections;

public class wwenemydead : MonoBehaviour {
	private float b,c;
	// Update is called once per frame
	void Update () {
		
				c = c + Time.deltaTime * 50f;
				//		b = transform.eulerAngles.x + Time.deltaTime * 3f;
				b = Mathf.Lerp (b, 60, Time.deltaTime * 0.5f);
		
		transform.eulerAngles = new Vector3 (b, transform.eulerAngles.y, transform.eulerAngles.z);
				transform.Translate (transform.forward * Time.deltaTime * 20f, Space.World);
		}
}
