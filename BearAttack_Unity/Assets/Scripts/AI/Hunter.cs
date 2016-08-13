using UnityEngine;
using System.Collections;

public class Hunter : MonoBehaviour {

	private GameObject _player;
	private Animator _anim;

	//state
	public enum HunterState {Wander, SeekBear, Shooting};
	public HunterState myState;


	//wander
	public int speed = 4;
	private Vector3 _wayPoint;
	public float seekRadius = 10;
	private float _wanderTimer = 0;
    private Rigidbody myRigid;

	//shooting
	public float firingDist = 8;
	public float aimTime = 1f;
	public GameObject firingArc;
	public GameObject gunParticle;
	public Transform particleTrans;
    
    //audio
    public AudioClip shootSound;
    private AudioSource shootSource;

	// Use this for initialization
	void Awake () 
    {
		_player = GameObject.Find("Player");

		myState = HunterState.Wander;
		Redirect(); 

		_anim = GetComponentInChildren<Animator>();

		firingArc.GetComponent<Renderer>().enabled = false;
        shootSource = gameObject.AddComponent<AudioSource>();
        shootSource.clip = shootSound;
        shootSource.spatialBlend = 1;
        myRigid = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update() 
	{
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		
		
		switch(myState)
		{
			case HunterState.Wander:
				//wander around and find a bear
				Wander();
                myRigid.velocity = _wayPoint * speed / 2f;
				SeekBear();
				//
				break;

			case HunterState.SeekBear:
				

				break;

			case HunterState.Shooting:
				
				break;

			default:
				break;
		}

		
		transform.LookAt(transform.position + _wayPoint);
        _anim.SetFloat("Speed", myRigid.velocity.magnitude);
	}

	void Wander()
	{
		_wanderTimer += Time.deltaTime;
		if(_wanderTimer >= 5.0f)
		{
			_wanderTimer = 0;
			Redirect();
		}
	}


	void SeekBear()
	{
		Vector3 toBear = _player.transform.position - transform.position;

		if(toBear.magnitude < seekRadius)
		{
			if(toBear.magnitude < firingDist)
			{
				_wayPoint = toBear.normalized;
				StartCoroutine(TakeAShot());
				return;
			}

			//print("too close!");
			_wayPoint = toBear.normalized;
			_wayPoint.y = 0;
			transform.LookAt(transform.position + _wayPoint);
            myRigid.velocity = _wayPoint * speed;
		}

		
	}
	public LayerMask shootMask;
	float fov = 20f;
	bool LineOfSight (Transform target) 
    {
		
		Vector3 toTarget = GameObject.Find("Spine2").transform.position - transform.position;

		Debug.DrawRay(transform.position, toTarget, Color.blue);
		RaycastHit[] hits;
		hits = Physics.RaycastAll(transform.position, toTarget, shootMask);
		//print(raycast);
		//print(toTarget.magnitude + " " + raycast + " " + Vector3.Angle(toTarget, transform.forward)  + " " + hit.collider.name);
		bool hitPlayer = false;
		bool hitCover = false;
		
		for(int i=0; i<hits.Length; i++)
		{
		    Collider col = hits[i].collider;
		    Vector3 toCol = col.transform.position - transform.position;
		    if (Vector3.Angle(toCol, transform.forward) <= fov && toCol.magnitude < firingDist)
			{
				print(col.name);
				if(col.tag == "Player")
					hitPlayer = true;
				else if(col.tag == "Tree" || col.name == "Tree")
					hitCover = true;
				else if(col.tag == "Food")
					col.gameObject.GetComponent<FriendlyFireGib>().FriendlyFire();
		    }
		   
		}
        print(hitPlayer + "player   " + hitCover + "cover   ");
		if(hitCover)
			return false;

		if(hitPlayer && !hitCover)
			return true;

		return false;

	}	

	IEnumerator TakeAShot()
	{
		myState = HunterState.Shooting;
		_anim.SetBool("KneelDown", true);
		firingArc.GetComponent<Renderer>().enabled = true;
		iTween.FadeFrom(firingArc, 0, aimTime);

		yield return new WaitForSeconds(aimTime);

        shootSource.Play();
		Destroy(Instantiate(gunParticle, particleTrans.position, particleTrans.rotation), 1f);
		_anim.SetBool("Shoot", true);
		if(LineOfSight(_player.transform) && _player.GetComponent<bearHealth>().health > 0)
		{
           
			Vector3 toTarget = GameObject.Find("Spine2").transform.position - transform.position;
			_player.GetComponent<bearHealth>().health = -1;
			_player.GetComponent<DeathRagdoll>().ActivateRagdoll(toTarget.normalized*8000);
		}
			

		yield return new WaitForSeconds(0.1f);
		firingArc.GetComponent<Renderer>().enabled = false;
		_anim.SetBool("Shoot", false);
		_anim.SetBool("KneelDown", false);

		yield return new WaitForSeconds(1f);
		

		myState = HunterState.Wander;
	}

	void Shooting()
	{
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		transform.LookAt(transform.position + _wayPoint);
	}


	void Redirect()
	{ 
	   // does nothing except pick a new destination to go to
	    _wayPoint = Random.onUnitSphere;
	   	_wayPoint.y = 0;
	   // don't need to change direction every frame seeing as you walk in a straight line only
		
	    //Debug.Log(_wayPoint + " and " + (transform.position - _wayPoint).magnitude);

	    
	}
	
	void OnCollisionStay(Collision objectHit)
	{
		int obstacleLayer = LayerMask.NameToLayer("Obstacle");
		int hermitLayer = LayerMask.NameToLayer("Hermit");
		if(objectHit.gameObject.layer == obstacleLayer && myState == HunterState.Wander)
		{
			Redirect();
            myRigid.velocity = _wayPoint * speed;
		}
		else if(objectHit.gameObject.layer == hermitLayer)
		{
			//print("hit a hermit, stop!");
			//StopChopppingTree();
		}
	}

}
