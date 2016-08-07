using UnityEngine;
using System.Collections;

public class APISpawner : MonoBehaviour
{
	public GameObject API;
	
	// Use this for initialization
	void Awake ()
	{
		if(GameObject.FindGameObjectsWithTag("API").Length < 1)
		{
			Instantiate(API, Vector3.zero, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
