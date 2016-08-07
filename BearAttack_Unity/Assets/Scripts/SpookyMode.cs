using UnityEngine;
using System.Collections;

public class SpookyMode : MonoBehaviour {

	private float time;

	bool killed = false;

	// Use this for initialization
	void Start () {
		//Hermit.spookyModeOn = true;

		Screen.lockCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;

		if(time >= 120f && !killed)
		{
			Time.timeScale = 0;
			killed = true;
			GameObject.Find("Player").GetComponent<score>().time = -4444.4444f;
			//Camera.main.GetComponent<ThirdPersonCam>().enabled = false;
		}

		if(killed)
			time += 0.01f;

		if(time >= 121.5f)
		{
			Time.timeScale = 1;
			Application.LoadLevel("game-new");
		}
	}
}
