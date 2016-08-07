using UnityEngine;
using System.Collections;

public class placeStump : MonoBehaviour {
	
	Color newCol;
	
	// Use this for initialization
	void Start ()
	{
		transform.Translate(new Vector3(Random.Range(-4f,4f), 0, Random.Range(-4f,4f)));
		
		transform.localScale = new Vector3(Random.Range(.5f,1.5f), Random.Range(1f,2f), Random.Range(.5f,1.5f));
		
		transform.eulerAngles = new Vector3(0, Random.Range(0,360), 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
