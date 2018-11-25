//@script RequireComponent (ParticleEmitter)

public var always : boolean = false;
public var life : float = 1.0;
public var smooth : float = 0.3;
private var spawnTime : float;
private var seakTime : float;


function Start(){
	spawnTime= Time.time;
	seakTime = life * 0.4;
}

function Update () {
	if(always == false){
		for(var pm : ParticleEmitter in gameObject.GetComponentsInChildren(typeof(ParticleEmitter))){
			pm.minSize = Mathf.Lerp(pm.minSize, 0, Time.deltaTime*smooth);
			pm.maxSize = Mathf.Lerp(pm.maxSize, 0, Time.deltaTime*smooth);
			pm.minEnergy = Mathf.Lerp(pm.minEnergy, 0, Time.deltaTime*smooth);
			pm.maxEnergy = Mathf.Lerp(pm.maxEnergy, 0, Time.deltaTime*smooth);
		}
	}
	if(Time.time > spawnTime+life && !always)
		GameObject.Destroy(gameObject);
	if(transform.position.y < -26.5){
		if(always == true)
			always = false;
		spawnTime = Time.time;
	}
}

function setLife(mylife: float){
	life = mylife;
	spawnTime =Time.time;
}