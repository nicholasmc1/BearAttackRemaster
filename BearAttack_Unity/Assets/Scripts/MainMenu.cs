using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public GUISkin gSkin;

	public AudioSource stinger;

	
	void Awake()
	{
		PlayerPrefs.SetString("Quality", "high");
		AudioListener.volume = .5f;
		Time.timeScale = 1;
	}

	void OnGUI()
	{

		GUI.skin = gSkin;
		
		if(GameObject.Find("CreditsButton").GetComponent<credits>().displayCredits == false)
		{
		
			if(GUI.Button(new Rect(Screen.width/2-(Screen.width/3), Screen.height/10, Screen.width/1.5f, Screen.width/3), ""))
			{
				int rand = Random.Range(1,1000);

				if(rand == 4)
					Application.LoadLevel("spookymode");
				else
					Application.LoadLevel("game-new");
			}
			
		}
	}
}
