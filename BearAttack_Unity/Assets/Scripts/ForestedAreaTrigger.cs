using UnityEngine;
using System.Collections;

public class ForestedAreaTrigger : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			GameObject.Find("Player").GetComponent<moveChar>().EndSlowdown();
		}
	}
}
