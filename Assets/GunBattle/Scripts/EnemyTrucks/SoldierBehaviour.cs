using UnityEngine;
using System.Collections;

public class SoldierBehaviour : MonoBehaviour {

	enum SoliderState {IDLE, MOVE, SHOOT}
	public Transform player;
	public GameObject bullet;
	public Transform shootPos;
	public float idleTime = 5f;
	public float moveTime = 1.1f;
	public float shootTimeRange = 5;
	public float rndRange = 5;
	private float shootTime;
	public float moveSpeed = 3;
	private float timeCounter = 0;
	private bool timeFlag = false;

	public bool moveable = true;

	private float shootableDistance;
	private SoliderState sState;

	RaycastHit hit;
	Ray forwardRay;

	private Transform myTransform;
	// Use this for initialization
	void Start () {
		myTransform = transform;
		sState = SoliderState.SHOOT;
		if (player == null)
			player = GameObject.Find ("Player").transform;
		shootableDistance = GlobalGameState.S_SOLDIERRANGE;
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null)
						return;
		if (Vector3.Distance (myTransform.position, player.position) > shootableDistance)
			return;
		
		RaycastHit hit;
		if (Physics.Raycast (shootPos.position, player.position - shootPos.position, out hit, shootableDistance)){
			if (LayerMask.LayerToName (hit.collider.gameObject.layer) != "Player"){
				return;
			}
		}

		switch (sState) {
		case SoliderState.IDLE:
			if(timeFlag == false){
				timeFlag = true;
				timeCounter = 0;
			}
			else{
				if(timeCounter > idleTime){
					sState = SoliderState.SHOOT;
					timeFlag = false;
				}
			}
			break;
		case SoliderState.MOVE:
			if(timeFlag == false){
				timeFlag = true;
				timeCounter = 0;
				myTransform.Rotate(myTransform.up, Random.Range(20, 340));
			}
			else{
				if(timeCounter > moveTime){
					sState = SoliderState.IDLE;
					timeFlag = false;
				}
				else{
					forwardRay = new Ray(shootPos.position,  player.position - shootPos.position);
					Physics.Raycast(forwardRay, out hit, Time.deltaTime * moveSpeed);

					if(hit.collider != null){
						myTransform.Rotate(myTransform.up, Random.Range(100, 260));
					}
					else{
						if(!myTransform.GetComponent<Animation>().isPlaying)
							myTransform.GetComponent<Animation>().Play();
						myTransform.position +=  myTransform.forward * Time.deltaTime * moveSpeed;
					}

				}
			}
			break;
		case SoliderState.SHOOT:
			if(timeFlag == false){
				shootTime = Random.Range(1, shootTimeRange);
				timeCounter = 0;
				timeFlag = true;
			}
			else {
				Vector3	deltaPos;
				deltaPos = new Vector3 ((player.position.x - myTransform.position.x), 0, (player.position.z - myTransform.position.z));
				Quaternion newRot = Quaternion.LookRotation (deltaPos, myTransform.up);
				myTransform.rotation = Quaternion.Slerp(myTransform.rotation, newRot, Time.deltaTime * 8);
				if(timeCounter > shootTime){
					Shoot();
					if(moveable)
						sState = SoliderState.MOVE;
					else 
						sState = SoliderState.IDLE;
					timeFlag = false;
				}
			}
			break;
		}
		timeCounter += Time.deltaTime;
	}

	void Shoot(){
		Quaternion rot = Quaternion.LookRotation(player.position - myTransform.position + Random.insideUnitSphere * rndRange);
		GameObject.Instantiate(bullet, shootPos.position, rot);
	}
}
