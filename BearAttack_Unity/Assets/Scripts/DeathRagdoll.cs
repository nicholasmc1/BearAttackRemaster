using UnityEngine;
using System.Collections;

public class DeathRagdoll : MonoBehaviour {
	public GameObject ragdollPrefab;
	public GameObject bloodParticle;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void ActivateRagdoll(Vector3 force)
	{

		GameObject bearObj = GameObject.Find("Bear_Object");
		GameObject ragdoll = (GameObject)Instantiate(ragdollPrefab, bearObj.transform.position, bearObj.transform.rotation);
		bearObj.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;

		ragdoll.transform.parent = bearObj.transform;
		Destroy(Instantiate(bloodParticle, transform.position, transform.rotation), 2f);
		GameObject.Find("RagdollSpine").GetComponent<Rigidbody>().AddForce(force);


		Destroy(this);
	}
}
