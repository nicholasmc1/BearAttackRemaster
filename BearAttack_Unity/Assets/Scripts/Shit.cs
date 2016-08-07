using UnityEngine;
using System.Collections;

public class Shit : MonoBehaviour {

	public GameObject shitPrefab;
	public AudioSource shitSound;
	// Update is called once per frame
	void Update ()
	{
		if(transform.parent.parent.GetComponent<bearHealth>().shit >= 100)
		{
			if(Input.GetKey(KeyCode.Space))
			{
				StartCoroutine(DoAShit());
			}
		}
	}

	IEnumerator DoAShit()
	{
		transform.parent.parent.GetComponent<bearHealth>().shit = 0;

		yield return new WaitForSeconds(0.1f);

		shitSound.Play();
		GameObject.Find("Bear_Object").GetComponent<animate>().playAnim("pooping", .05f,2f);
		
		GameObject temp = Instantiate(shitPrefab, transform.position, Quaternion.identity) as GameObject;
		temp.GetComponent<Rigidbody>().AddForce(transform.forward * 400);
	}
}
