using UnityEngine;
using System.Collections;

public class gib : MonoBehaviour {
	
	private float explosionForce = 200;	
	
	// Use this for initialization
	void Start ()
	{
		foreach(Transform child in transform)
		{
			Vector3 direction = (child.transform.position - transform.position) + GameObject.Find("Player").GetComponent<Rigidbody>().velocity;
			child.GetComponent<Rigidbody>().AddForce(direction.normalized * explosionForce);
			child.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(0,explosionForce), Random.Range(0,explosionForce), Random.Range(0,explosionForce)));
		}
	}
}
