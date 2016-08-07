using UnityEngine;
using System.Collections;

public class credits : MonoBehaviour {
	
	public GUIStyle style;
	public GUIText creditsText;
	
	public float x;
	public float y;
	
	public float x2;
	public float y2;

	public bool displayCredits;
	
	void OnGUI()
	{	
		if(GUI.Button(new Rect(Screen.width/2 - x, Screen.height - y, x2, y2), new GUIContent("", "Credits"), style))
		{
			if(displayCredits == false)
			{
				displayCredits = true;
				GetComponent<GUIText>().text = "Back";
				
			}
			
			else
			{
				displayCredits = false;
				GetComponent<GUIText>().text = "Credits";
			}
		}
	
		string hover = GUI.tooltip;
		
		if(hover == "Credits")
		{
			creditsText.color = Color.white;
		}
		
		else
		{
			creditsText.color = new Color(255f,255f,255f, .7f);
		}
	}
}
