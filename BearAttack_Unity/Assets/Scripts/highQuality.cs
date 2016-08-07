using UnityEngine;
using System.Collections;

public class highQuality : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		if(PlayerPrefs.GetString("Quality") == "high")
		{
			Destroy(gameObject);
		}
	}
}
