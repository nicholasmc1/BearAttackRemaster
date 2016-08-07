using UnityEngine;
using System.Collections;

public class destroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//int range =(int)Mathf.Sqrt(GameObject.FindGameObjectsWithTag("Forest").Length) * 7;
		
		if(Mathf.Abs(transform.position.x) < -75 || Mathf.Abs(transform.position.x) > 65)
		{
			Destroy(gameObject);
		}
		
		if(Mathf.Abs(transform.position.z) < -75 || Mathf.Abs(transform.position.z) > 65)
		{
			Destroy(gameObject);
		}
	}
}
