using UnityEngine;
using System.Collections;

public class alignGui : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		float margin = 32f;
		GetComponent<GUITexture>().pixelInset = new Rect(-Screen.width/2 + margin, -Screen.height/2 + margin, 258.8f, 78.4f);
		GUI.depth = -2;
	}
}
