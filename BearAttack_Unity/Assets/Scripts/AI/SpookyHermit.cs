using UnityEngine;
using System.Collections;

public class SpookyHermit : MonoBehaviour {

	public static bool spookyModeOn = true;

	private GameObject _player;

	private Animator _anim;

	//state
	public enum HermitState {InitialFlock, Wander, ProtectTree, ChasingLogger};
	public HermitState myState;

	//initial flock
	private GameObject _flockObject; 

	//wander
	public int speed = 5;
	private Vector3 _wayPoint;
	public float fleeRadius = 10;
	private float _wanderTimer = 0;

	public float treeDetectRadius = 5;

	//protect tree
	private GameObject _treeToProtect;

	//chasing loggers
	private GameObject _loggerToChase;
	public float loggerDetectRadius = 5;
	public float hustleTime = 10f;
	public float _hustleTimer = 0;

	// Use this for initialization
	void Start () {
		_player = GameObject.Find("Player");

		myState = HermitState.InitialFlock;
		Redirect();

		_anim = GetComponentInChildren<Animator>();

		_flockObject = GameObject.Find("HermitFlock");

		//StartCoroutine(TempFlock());
	}
	
	// Update is called once per frame
	void Update() 
	{
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		
		switch(myState)
		{
			case HermitState.InitialFlock:
				Flock();
				break;

			case HermitState.Wander:
				//wander around and avoid bear
				
				//FindTreeToProtect();
				Wander();
				FindLoggerToChase();
				GetComponent<Rigidbody>().velocity = _wayPoint * speed/3f;
				break;

			case HermitState.ProtectTree:
				FindLoggerToChase();
				ProtectTree();

				break;

			case HermitState.ChasingLogger:
				ChasingLogger();
				break;

			default:
				break;
		}

		if(!spookyModeOn)
			AvoidBear();

		if(myState == HermitState.ChasingLogger)
			_anim.SetFloat("Speed", 10);
		else
			_anim.SetFloat("Speed", GetComponent<Rigidbody>().velocity.magnitude);
		
		transform.LookAt(transform.position + _wayPoint);
	}

	IEnumerator TempFlock()
	{
		if(!spookyModeOn)
			yield return new WaitForSeconds(Random.Range(6f,12f));
		else
			yield return new WaitForSeconds(60f);					

		myState = HermitState.Wander;
		Redirect();
	}

	void Flock()
	{
		Vector3 toFlock = (_flockObject.transform.position - transform.position).normalized;
		_wayPoint = toFlock;
		GetComponent<Rigidbody>().velocity = _wayPoint * speed/3f;
	}

	void Wander()
	{
		_wanderTimer += Time.deltaTime;
		if(_wanderTimer >= 5.0f)
		{
			_wanderTimer = 0;
			Redirect();
		}

		GetComponent<AudioSource>().Stop();
	}


	void FindLoggerToChase()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, loggerDetectRadius);
		int loggerLayer = LayerMask.NameToLayer("Logger");
		for(int i=0;i<hitColliders.Length; i++)
		{
			if(hitColliders[i].gameObject.layer == loggerLayer)
			{
				_loggerToChase = hitColliders[i].gameObject;
				myState = HermitState.ChasingLogger;
				break;
			}
		}
	}

	void ChasingLogger()
	{
		if(_loggerToChase == null)
		{
			Redirect();
			myState = HermitState.Wander;
			return;
		}
		
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Play();

		Vector3 toLogger = _loggerToChase.transform.position - transform.position;
		_wayPoint = (toLogger).normalized;
		GetComponent<Rigidbody>().velocity = _wayPoint * speed;

		if(toLogger.magnitude <= 3f)
		{
			_hustleTimer += Time.deltaTime;

			if(_hustleTimer >= hustleTime)
			{
				_hustleTimer = 0;
				myState = HermitState.InitialFlock;
				StartCoroutine(TempFlock());
			}
		}
	}

	void AvoidBear()
	{
		Vector3 toBear = _player.transform.position - transform.position;

		if(toBear.magnitude < fleeRadius)
		{
			//print("too close!");
			_wayPoint = -toBear.normalized;
			_wayPoint.y = 0;
			transform.LookAt(transform.position + _wayPoint);
			_treeToProtect = null;
			myState = HermitState.Wander;

			GetComponent<Rigidbody>().velocity = _wayPoint * speed;
		}

		
	}

	void FindTreeToProtect()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, treeDetectRadius);

		for(int i=0;i<hitColliders.Length; i++)
		{
			if(hitColliders[i].gameObject.tag == "Tree")
			{
				//print("protect the tree!");
				_treeToProtect = hitColliders[i].gameObject;
				_wayPoint = (_treeToProtect.transform.position - transform.position).normalized;
				myState = HermitState.ProtectTree;
				break;
			}
		}
	}

	void ProtectTree()
	{
		if(_treeToProtect == null)
		{
			_treeToProtect = null;
			myState = HermitState.Wander;
			return;
		}

		Vector3 toTree = (_treeToProtect.transform.position - transform.position);
		//print(toTree.magnitude);

		/*if(toTree.magnitude < 1.5f)
		{
			print("stop at tree");
			rigidbody.velocity = Vector3.zero;
		}
		else
		{
			_wayPoint = (_treeToProtect.transform.position - transform.position).normalized;
			rigidbody.velocity = _wayPoint * speed;
		}*/



	}
	 
	void Redirect()
	{ 
	   // does nothing except pick a new destination to go to
	    _wayPoint = Random.onUnitSphere;
	   	_wayPoint.y = 0;
	   // don't need to change direction every frame seeing as you walk in a straight line only
	    transform.LookAt(transform.position + _wayPoint);
		
	    //Debug.Log(_wayPoint + " and " + (transform.position - _wayPoint).magnitude);

	    
	}
	
	void OnCollisionStay(Collision objectHit)
	{

	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
			StartCoroutine(FuckOneShot());
	}

	IEnumerator FuckOneShot()
	{
		GetComponent<AudioSource>().Play();
		GameObject.Find("Player").GetComponentInChildren<Animator>().SetBool("fuck", true);
		//Camera.main.GetComponent<VortexEffect>().angle += 10;
		//Camera.main.GetComponent<VortexEffect>().radius.x += 0.1f;
		//Camera.main.GetComponent<VortexEffect>().radius.y += 0.1f;
		//Camera.main.GetComponent<ColorCorrectionCurves>().saturation -= 0.05f;
		//GameObject.Find("Flash").GetComponent<GUITexture>().enabled = true;
		//iTween.ScaleBy(GameObject.Find("Player"), new Vector3(2,3,2), 0.5f);
		yield return new WaitForSeconds(0.1f);
		//GameObject.Find("Flash").GetComponent<GUITexture>().enabled = false;
		//GameObject.Find("Player").GetComponentInChildren<Animator>().SetBool("fuck", false);

		//iTween.ScaleBy(GameObject.Find("Player"), new Vector3(0.6f,2,0.7f), 0.5f);

		//Destroy(gameObject);

	}


}
