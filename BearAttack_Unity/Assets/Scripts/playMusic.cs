using UnityEngine;
using System.Collections;

public class playMusic : MonoBehaviour {

	// Use this for initialization
	void Awake ()
	{
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*if(GameObject.Find("Player").GetComponent<bearHealth>().health > 0)
		{
			if(Camera.main)
				transform.parent = Camera.main.transform;
		}
		
		else
		{
			transform.parent = null;
		}*/
		
		
		
		if(Application.loadedLevelName != "game-new")
		{
			Destroy(gameObject);
		}
	}
}
