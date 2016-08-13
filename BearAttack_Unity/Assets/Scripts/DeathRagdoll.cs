using UnityEngine;
using System.Collections;

public class DeathRagdoll : MonoBehaviour 
{
    public GameObject ragdoll;
	public GameObject bloodParticle;
    public GameObject bearObj;
	public void ActivateRagdoll(Vector3 force)
	{
        ragdoll.SetActive(true);
        bearObj.GetComponent<Renderer>().enabled = false;

		ragdoll.transform.parent = bearObj.transform;
		Destroy(Instantiate(bloodParticle, transform.position, transform.rotation), 2f);
        ragdoll.transform.FindChild("RagdollSpine").GetComponent<Rigidbody>().AddForce(force);
	}
}
