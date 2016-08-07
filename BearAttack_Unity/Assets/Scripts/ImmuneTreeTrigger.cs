using UnityEngine;
using System.Collections;

public class ImmuneTreeTrigger : MonoBehaviour {
	GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position;
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Tree")
		{
			other.gameObject.tag = "ImmuneTree";
		}
	}

	void OnTriggerExit(Collider other){
		if(other.gameObject.tag == "ImmuneTree")
		{
			other.gameObject.tag = "Tree";
		}
	}
}
