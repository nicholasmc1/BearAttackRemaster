using UnityEngine;
using System.Collections;

public class setHeight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = new Vector3(transform.position.x, GameObject.Find("Spine2").transform.position.y, transform.position.z);
	}
}
