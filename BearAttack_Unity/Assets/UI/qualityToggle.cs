using UnityEngine;
using System.Collections;

public class qualityToggle : MonoBehaviour {
	
	public GUIStyle style;
	public GUIText qualityText;
	
	public float x;
	public float y;
	
	public float x2;
	public float y2;
	
	private bool lowQuality = true;
	
	void Awake()
	{
		PlayerPrefs.SetString("Quality", "high");
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width/2 - x, Screen.height - y, x2, y2), new GUIContent("", "Quality"), style))
		{	
			Debug.Log("clicked");
			if(lowQuality == false)
			{
				print ("high");
				GetComponent<GUIText>().text = "Quality: < HIGH >";
				PlayerPrefs.SetString("Quality", "high");
				lowQuality = true;
			}
			
			else
			{
				print ("low");
				GetComponent<GUIText>().text = "Quality: < LOW >";
				PlayerPrefs.SetString("Quality", "low");
				lowQuality = false;
			}
				
		}
		
		if(PlayerPrefs.GetString("Quality") == "high")
		{
			GetComponent<GUIText>().text = "Quality: < HIGH >";
		}
		
		else if(PlayerPrefs.GetString("Quality") == "low")
		{
			GetComponent<GUIText>().text = "Quality: < LOW >";
		}
		
		if(GameObject.Find("CreditsButton").GetComponent<credits>().displayCredits == true)
		{
			GetComponent<GUIText>().text = "";
		}
		
		string hover = GUI.tooltip;
		
		if(hover == "Quality")
		{
			qualityText.color = Color.white;
		}
		
		else
		{
			qualityText.color = new Color(255f,255f,255f, .7f);
		}
	}
}
