/*using UnityEngine;
using System.Collections;

public class Hermit : MonoBehaviour {

	private GameObject _player;

	private Animator _anim;

	//state
	public enum HermitState {Wander, ProtectTree, ChasingLogger};
	public HermitState myState;


	//wander
	public int speed = 5;
	private Vector3 _wayPoint;
	public float fleeRadius = 10;

	public float treeDetectRadius = 5;

	//protect tree
	private GameObject _treeToProtect;

	//chasing loggers
	private GameObject _loggerToChase;
	public float loggerDetectRadius = 5;

	// Use this for initialization
	void Start () {
		_player = GameObject.Find("Player");

		myState = HermitState.Wander;
		Redirect();

		_anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update() 
	{
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		
		switch(myState)
		{
			case HermitState.Wander:
				//wander around and avoid bear
				
				FindTreeToProtect();
				FindLoggerToChase();
				rigidbody.velocity = _wayPoint * speed/3f;
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

		AvoidBear();

		_anim.SetFloat("Speed", rigidbody.velocity.magnitude);
		
		transform.LookAt(transform.position + _wayPoint);
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
			
		_wayPoint = (_loggerToChase.transform.position - transform.position).normalized;
		rigidbody.velocity = _wayPoint * speed;
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

			rigidbody.velocity = _wayPoint * speed;
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

		if(toTree.magnitude < 1.5f)
		{
			print("stop at tree");
			rigidbody.velocity = Vector3.zero;
		}
		else
		{
			_wayPoint = (_treeToProtect.transform.position - transform.position).normalized;
			rigidbody.velocity = _wayPoint * speed;
		}

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
		int layerNumber = LayerMask.NameToLayer("Obstacle");
		int loggerLayer = LayerMask.NameToLayer("Logger");
		if(objectHit.gameObject.layer == layerNumber && myState != HermitState.ProtectTree)
		{
			Redirect();
			rigidbody.velocity = _wayPoint * speed;
		}
		else if(objectHit.gameObject.layer == loggerLayer)
		{
			_loggerToChase = null;
			myState = HermitState.Wander;
			Redirect();
		}
	}
	
	void OnTriggerEnter(Collider other)
	{

	}


}
*/