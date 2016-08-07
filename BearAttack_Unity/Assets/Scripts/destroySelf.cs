using UnityEngine;
using System.Collections;

public class destroySelf : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		Destroy(gameObject, 5);
		
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject, 5);
		}
	}
}
