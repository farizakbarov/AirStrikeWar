using UnityEngine;
using System.Collections;

public class groundtrigger : MonoBehaviour {
	public static float triggered=0;
	
	void  OnTriggerEnter ( Collider other  ){
		triggered=1;
	}
	
	void  OnTriggerExit ( Collider other  ){
		triggered=0;
	}
}



