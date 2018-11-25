// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class enemyHealthBar : MonoBehaviour {
	private float health;
	private float maxHealth;
	private float adjustment= 5f;
	private Vector3 worldPosition= new Vector3();
	private Vector3 screenPosition= new Vector3();
	private Transform myTransform;
	private Camera myCamera;
	private int healthBarLeft= 30;
	private int barTop= 1;
	private Texture bg, hat;
	private int width, height;
	//assign the camera to a variable so we can raycast from it
	private GameObject myCam;
	
	void  Awake (){
		myTransform = transform;
		myCamera = Camera.main;
		health = 100; //arbritrarily chosen values to show that this script works
		maxHealth = 100;
		myCam = GameObject.Find("Main Camera"); //I removed the space from the camera's name in the Unity Inspector, so you will probably need to change this
		width = Screen.width;
		height = Screen.height;
	}

	void Start(){
		hat = GlobalGameState.healthbarHat;
		bg = GlobalGameState.healthbarBG;
	}

	int interval = 0;
	bool flag = false;
	RaycastHit hit;
	void Update(){
		if (Time.timeScale == 0) {
			flag = false;
			return;
		}
		interval++;
		if (interval > 3) {
			if (Vector3.Distance (myTransform.position, myCam.transform.position) > 400)
				flag = false;
			else {
				worldPosition = new Vector3(myTransform.position.x, myTransform.position.y, myTransform.position.z);
				if (Vector3.Dot (myCamera.transform.forward, worldPosition - myCamera.transform.position) < 0.8f)
					flag = false;
				else{
					if(Physics.Linecast(myTransform.position, myCam.transform.position, out hit)){
						flag = false;
					}
					else {
						screenPosition = myCamera.WorldToScreenPoint(worldPosition);
						flag = true;
					}

				}
			}
		}
	}

	void  OnGUI (){
		if (flag == false)
						return;

		if (!new Rect (width / 2 - width / 30, height / 2 - height / 20, width / 15, height / 10).Contains (screenPosition))
						return;			
			GUI.DrawTexture(new Rect(screenPosition.x - healthBarLeft / 2, height - screenPosition.y - barTop, 30, 3), bg);
			GUI.DrawTexture(new Rect(screenPosition.x - healthBarLeft / 2, height - screenPosition.y-barTop, 30*health / maxHealth, 3), hat);
	}

	public void RecvHealthVal(float hVal){
		health = Mathf.Max(hVal, 0);
	}

	public void InitHealth(float maxVal){
		maxHealth = maxVal;
		health = maxVal;
	}
}