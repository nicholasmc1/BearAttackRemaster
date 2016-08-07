using UnityEngine;
using System.Collections;

public class lowQuality : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		if(PlayerPrefs.GetString("Quality") == "low")
		{
			Destroy(gameObject);
		}
	}
}
