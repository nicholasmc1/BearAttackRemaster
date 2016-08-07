using UnityEngine;
using System.Collections;

public class musicSpawner : MonoBehaviour
{
	public GameObject music;
	
	// Use this for initialization
	void Awake ()
	{
		if(GameObject.FindGameObjectsWithTag("Music").Length < 1)
		{
			Instantiate(music, Vector3.zero, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
