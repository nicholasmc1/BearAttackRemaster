using UnityEngine;
using System.Collections;

public class score : MonoBehaviour {

	public GUIStyle scoreStyle;
	public GUIStyle gameOverStyle;
	

	public GUISkin pauseSkin;
	private bool paused = false;
		
	
	public bool submitted = false;
	
	public float time = 0.0f;

	void Start()
	{
		Time.timeScale = 1;
	}
	 
	void Update ()
	{
		if(GetComponent<bearHealth>().health > 0)
		{
			time += Time.deltaTime;
		}
		

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(paused)
			{
				paused = false;
				Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
			}
			else
			{
				paused = true;
				Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
			}
		}		
	}
	
	void OnGUI()
	{
		if(GetComponent<bearHealth>().health > 0)
		{
			GUI.Label(new Rect(Screen.width/4, 32, Screen.width/2, Screen.height/8), time.ToString("0.00"), scoreStyle);
		}
		
		// If the bear is dead, display game over
		else
		{	
			GUI.Label(new Rect(Screen.width/4, Screen.height/4, Screen.width/2, Screen.height/2), "GAME OVER\n" + "You survived for " + time.ToString("0.00") + " seconds!\n \n Press Space to restart", gameOverStyle);
			
			if(submitted == false)
			{
				Application.ExternalCall("kongregate.stats.submit","highScore",(int)(time*100));
				submitted = true;
			}
			
			if(Input.GetKeyDown (KeyCode.Space))
			{
				Application.LoadLevel(Application.loadedLevel);
			}
				
		}
		
		if(paused)
		{
			GUI.skin = pauseSkin;
			GUI.Box(new Rect(Screen.width/2-130, Screen.height/2-175, 260, 350), "PAUSED\n\nVolume:\n\nWASD: Movement \nSPACE: Poop \nMouse: Control Camera \nESC: Pause");
			AudioListener.volume = GUI.HorizontalSlider(new Rect(Screen.width/2-95, Screen.height/2-71, 189, 10), AudioListener.volume, 0f, 1f);

			if(GUI.Button(new Rect(Screen.width/2-105, Screen.height/2+85, 210, 70), "Return to Main Menu"))
			{
				Application.LoadLevel(0);
			}
		}		
	}
}
