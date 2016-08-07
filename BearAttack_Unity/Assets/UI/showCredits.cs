using UnityEngine;
using System.Collections;

public class showCredits : MonoBehaviour {
	
	public GUIStyle style;
	
	public float x;
	public float y;
	
	public float x2;
	public float y2;
	
	private bool lowQuality = false;
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width/2 - x, Screen.height - y, x2, y2), "", style))
		{
			if(lowQuality == false)
			{
				PlayerPrefs.SetString("Quality", "high");
				lowQuality = true;
			}
			
			else
			{
				PlayerPrefs.SetString("Quality", "low");
				lowQuality = false;
			}
				
		}
		
		if(GameObject.Find("CreditsButton").GetComponent<credits>().displayCredits == true)
		{
			GetComponent<GUIText>().text =	"Roman Maksymyschyn\tProgramming\n" +
							"Winston Tang\tProgramming\n" +
							"Nicholas McDonnell\tArt\n" +
							"Maximilian Atkins\tArt";
		}
		
		else
		{
			GetComponent<GUIText>().text = "";
		}
	}
}
