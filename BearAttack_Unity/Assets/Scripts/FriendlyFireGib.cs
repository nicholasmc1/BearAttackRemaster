using UnityEngine;
using System.Collections;

public class FriendlyFireGib : MonoBehaviour {

	public GameObject gib;
	public GameObject bloodParticle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FriendlyFire()
	{
		Instantiate(gib, transform.position, transform.rotation);
		Instantiate(bloodParticle, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
