using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    public GameObject guibuttons;
    public GameObject healthbar;
    public GameObject subcontrolss;
    public GameObject touchablecontrol;
  //  public GameObject pausebutton;
    public GameObject pausebutton;
    public GameObject pausescreen;
    public GameObject touchcontrrrl;
    public GameObject levlfailed;
	public float turnSpeed = 10.0f;
	public float maxTurnLean = 50.0f;
	public float maxTilt = 50.0f;

	public float sensitivity = 10.0f;
	public float forwardForce = 1.0f;

	private float normalizedSpeed = 10f;
	private Vector3 euler;

	//Touch Controller
	public TouchController			touchCtrl;
	public WeaponLauncher 			m_WeaponLauncher;
	public WeaponLauncher			b_WeaponLauncher;
	public WeaponLauncher			gm_WeaponLauncher;

	private Vector3 initAccel = Vector3.zero;

	public GUITexture healthVal;
	private float hVal;
	public GUIText healthPer;
	public HittedGUI hittedGUI;

	public GameObject deadObj;
	private ParticleEmitter pEmitter;
	private Transform myTransform;

	private RaycastHit hit;
	private float posY;
	// Use this for initialization
	void Start () {
		myTransform = transform;
		euler = myTransform.eulerAngles;
		if(m_WeaponLauncher == null)
			m_WeaponLauncher = GameObject.Find ("Player/weapon/Missile").GetComponent<WeaponLauncher> ();
		if(b_WeaponLauncher == null)
			b_WeaponLauncher = GameObject.Find ("Player/weapon/Bullet").GetComponent<WeaponLauncher> ();

		initAccel = Input.acceleration;
		hVal = healthVal.pixelInset.width;
		pEmitter = myTransform.Find("Smoke").GetComponent<ParticleEmitter> ();

		GlobalGameState.c_bulletCount = GlobalGameState.g_bulletCount[GlobalEngine.currentScene-1];	
		GlobalGameState.c_missileCount = GlobalGameState.g_missileCount[GlobalEngine.currentScene-1];
		GlobalGameState.c_guideMissileCount = GlobalGameState.g_guideMissileCount[GlobalEngine.currentScene-1];
	}
	
	// Update is called once per frame
	void Update () {

		if (GlobalGameState.c_bulletCount <= 0 && GlobalGameState.c_missileCount <= 0 && GlobalGameState.c_guideMissileCount <= 0) {
			GameObject.Find("UI Root").GetComponent<AllGUI>().MissionFail();
		}
	}

	void FixedUpdate(){
		Vector3 accelerator = Input.acceleration - initAccel;
		#if UNITY_EDITOR
		accelerator = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		#endif
		// Rotate turn based on acceleration		
		euler += new Vector3 (0,accelerator.x * turnSpeed,0);
		// Since we set absolute lean position, do some extra smoothing on it
		euler = new Vector3 (euler.x, euler.y, Mathf.Lerp(euler.z, -accelerator.x * maxTurnLean, Time.deltaTime));
		
		// Since we set absolute lean position, do some extra smoothing on it
		euler = new Vector3(Mathf.Lerp(euler.x, accelerator.y * maxTilt, Time.deltaTime), euler.y, euler.z);
		
		// Apply rotation and apply some smoothing
		Quaternion rot = Quaternion.Euler(euler);
		myTransform.rotation = Quaternion.Lerp (myTransform.rotation, rot, sensitivity);
		myTransform.position = new Vector3 (myTransform.position.x, Mathf.Clamp(myTransform.position.y, 40, 110), myTransform.position.z);
	}


	public void ControlByTouch(TouchController ctrl, GlobalGameState game)
	{
		TouchZone
			zoneforward	= ctrl.GetZone(GlobalGameState.ZONE_FORWARD),
			zonebackward	= ctrl.GetZone(GlobalGameState.ZONE_BACKWARD),
			zonemissile	= ctrl.GetZone(GlobalGameState.ZONE_MISSILE),
			zonebullet	= ctrl.GetZone(GlobalGameState.ZONE_BULLET),
			zoneguidemissile = ctrl.GetZone (GlobalGameState.ZONE_GUIDEMISSILE);
		// Get gun's trigger state by checking Fire zone's state (including mid-frame press)
		
		bool	forwardTriggerState	= zoneforward.UniPressed(true, false);
			this.SetTriggerState(forwardTriggerState, true); 

		bool	backwardTriggerState	= zonebackward.UniPressed(true, false);
			this.SetTriggerState(backwardTriggerState, false); 

		bool	missileTriggerState	= zonemissile.UniPressed(true, false);
		if(missileTriggerState){
			m_WeaponLauncher.Shoot ();
		}

		bool	bulletTriggerState	= zonebullet.UniPressed(true, false);
		if(bulletTriggerState){
			b_WeaponLauncher.Shoot(); 
		}

		bool	guidemissileTriggerState = zoneguidemissile.UniPressed(true, false);
		if(guidemissileTriggerState){
			gm_WeaponLauncher.Shoot(); 
		}	
	}

	public void SetTriggerState(bool isPressed, bool isForward){
		if (isPressed) {
			if(isForward){
				GetComponent<Rigidbody>().AddRelativeForce(0, 0, normalizedSpeed * forwardForce * Time.deltaTime);
			}
			else{
				GetComponent<Rigidbody>().AddRelativeForce(0, 0, -normalizedSpeed * forwardForce * Time.deltaTime * 0.5f);
			}
		}
	}

	public void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "bullet_enemy(Clone)" || col.gameObject.name == "bullet_enemy air(Clone)") {
			GlobalGameState.myhealth -= GlobalGameState.BULLETPOWER;
			hittedGUI.HittedBullet();
		}
		else if(col.gameObject.name == "bullet_rocket enemy(Clone)"){
			GlobalGameState.myhealth -= GlobalGameState.MISSILEPOWER;
			hittedGUI.HittedMissile();
		}

		if(GlobalGameState.myhealth >0){
			healthPer.text = ((int)(GlobalGameState.myhealth * 100 / GlobalGameState.maxHealth)).ToString() + "%";
			healthVal.pixelInset = new Rect(healthVal.pixelInset.x, healthVal.pixelInset.y, hVal * GlobalGameState.myhealth / GlobalGameState.maxHealth, healthVal.pixelInset.height);
		}
		if(GlobalGameState.myhealth <= 0){
			healthPer.text = "0 %";
            AllGUI.instance.playeroff();
            levlfailed.SetActive(true);
            pausebutton.SetActive(false);
            touchcontrrrl.SetActive(false);
            subcontrolss.SetActive(false);
            guibuttons.SetActive(false);
            healthbar.SetActive(false);
			healthVal.pixelInset = new Rect(healthVal.pixelInset.x, healthVal.pixelInset.y, hVal * GlobalGameState.myhealth / GlobalGameState.maxHealth, healthVal.pixelInset.height);
			Destroy(gameObject);
		}

		if (GlobalGameState.myhealth < GlobalGameState.maxHealth / 6f) {
			pEmitter.emit = true;
			pEmitter.minEmission = 50;
			pEmitter.maxEmission = 100;
		}
		else if(GlobalGameState.myhealth < GlobalGameState.maxHealth / 3f){
			pEmitter.emit = true;
			pEmitter.minEmission = 20;
			pEmitter.maxEmission = 40;
		}
	}

	IEnumerator MissionFail(){
		yield return new WaitForSeconds (5);
		Time.timeScale = 0;
        touchcontrrrl.SetActive(false);
        subcontrolss.SetActive(false);
        guibuttons.SetActive(false);
        touchablecontrol.SetActive(false);
        
        levlfailed.SetActive(true);
		GameObject.Find ("Player").GetComponent<Indicator> ().enabled = false;
	}
}
