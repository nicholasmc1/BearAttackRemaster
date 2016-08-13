using UnityEngine;
using System.Collections;

public class Logger : MonoBehaviour {

	private GameObject _player;
	private Animator _anim;

	//state
	public enum LoggerState {Wander, SeekTree, ChoppingTree };
	public LoggerState myState;


	//wander
	public int speed = 5;
	private Vector3 _wayPoint;
	public float fleeRadius = 10;
	private float _wanderTimer = 0;

	public float treeDetectRadius = 5;

	//log tree
	private GameObject _treeToLog;
	private GameObject _treeToIgnore;

	//chopping
	private float _windUpTimer = 0;
	public float windUpTime = 3f;
	public GameObject deforestPrefab;
	private bool _chopping = false;


    //audio
    public AudioClip deforestSound;
    private AudioSource cutTreeSource;

	// Use this for initialization
	void Start () {
		_player = GameObject.Find("Player");

		myState = LoggerState.Wander;
		//Redirect();
		
		Vector3 toBear = _player.transform.position - transform.position;
		_wayPoint = toBear.normalized;

		_anim = GetComponentInChildren<Animator>();

        cutTreeSource = gameObject.AddComponent<AudioSource>();
        cutTreeSource.clip = deforestSound;
        cutTreeSource.spatialBlend = 1;

	}
	
	// Update is called once per frame
	void Update() 
	{
		transform.position = new Vector3(transform.position.x, 0, transform.position.z);
		
		
		switch(myState)
		{
			case LoggerState.Wander:
				//wander around and avoid bear
				Wander();
				FindTreeToChop();
				GetComponent<Rigidbody>().velocity = _wayPoint * speed;
				break;

			case LoggerState.SeekTree:
				SeekTree();

				break;

			case LoggerState.ChoppingTree:
				ChoppingTree();
				break;

			default:
				break;
		}

		AvoidBear();
		
		transform.LookAt(transform.position + _wayPoint);
		_anim.SetFloat("Speed", GetComponent<Rigidbody>().velocity.magnitude);

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



	void AvoidBear()
	{
		Vector3 toBear = _player.transform.position - transform.position;

		if(toBear.magnitude < fleeRadius)
		{
			//print("too close!");
			StopChopppingTree();
			_wayPoint = -toBear.normalized;
			_wayPoint.y = 0;
			transform.LookAt(transform.position + _wayPoint);
			GetComponent<Rigidbody>().velocity = _wayPoint * speed*2f;
		}

		
	}

	void FindTreeToChop()
	{
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, treeDetectRadius);

		for(int i=0;i<hitColliders.Length; i++)
		{
			if(hitColliders[i].gameObject.tag == "Tree" && hitColliders[i].gameObject != _treeToIgnore)
			{
				//print("log the tree!");
				_treeToLog = hitColliders[i].gameObject;
				_wayPoint = (_treeToLog.transform.position - transform.position).normalized;
				myState = LoggerState.SeekTree;
				break;
			}
		}
	}

	void SeekTree()
	{
		if(_treeToLog == null)
		{
			StopChopppingTree();
			return;
		}
			
		Vector3 toTree = (_treeToLog.transform.position - transform.position);
		////print(toTree.magnitude);

		if(toTree.magnitude < 1.5f)
		{
			//print("chop!");
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			myState = LoggerState.ChoppingTree;
			_anim.SetBool("SwingUp", true);
		}
		else
		{
			_wayPoint = (_treeToLog.transform.position - transform.position).normalized;
			GetComponent<Rigidbody>().velocity = _wayPoint * speed*1f;
		}

	}

	void ChoppingTree()
	{
		if(_treeToLog == null && !_chopping)
		{
			StopChopppingTree();
			return;
		}
			

		//wind up swing over 3 seconds
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		_windUpTimer += Time.deltaTime;
		////print(_windUpTimer);

		if(_windUpTimer >= windUpTime &&  !_chopping && _treeToLog != null)
		{
			_chopping = true;
			StartCoroutine(ChopTree());
		}
	}

	IEnumerator ChopTree()
	{
		_anim.SetBool("SwingDown", true);
		yield return new WaitForSeconds(0.5f);

		if(_treeToLog != null)
		{
            cutTreeSource.Play();
			//chop tree, deforest land
			GameObject treeLand = _treeToLog.transform.parent.gameObject;
			GameObject temp = Instantiate(deforestPrefab, treeLand.transform.position, Quaternion.identity) as GameObject;
			//temp.transform.FindChild("stump").transform.position = treeLand.transform.FindChild("Tree").transform.position;
			//temp.transform.FindChild("stump").transform.rotation = treeLand.transform.FindChild("Tree").transform.rotation;
			//temp.transform.FindChild("stump").transform.localScale = treeLand.transform.FindChild("Tree").transform.localScale*15;
			Destroy(treeLand);
		}

		//Destroy(_treeToLog);

		yield return new WaitForSeconds(0.5f);
		StopChopppingTree();
		_chopping = false;

		//stop ignoring
		_treeToIgnore = null;
	}

	void StopChopppingTree()
	{
		_windUpTimer = 0;
		myState = LoggerState.Wander;
		//ignore this tree, find another
		_treeToIgnore = _treeToLog;
		_treeToLog = null;
		_anim.SetBool("SwingUp", false);
		_anim.SetBool("SwingDown", false);
		Redirect();
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
		if(objectHit.gameObject.layer == obstacleLayer && myState == LoggerState.Wander)
		{
			Redirect();
			GetComponent<Rigidbody>().velocity = _wayPoint * speed;
		}
		else if(objectHit.gameObject.layer == hermitLayer)
		{
			//print("hit a hermit, stop!");
			StopChopppingTree();
		}
	}

	void OnCollisionEnter(Collision collision){
		
	}

	
	void OnTriggerEnter(Collider other)
	{

	}
}
