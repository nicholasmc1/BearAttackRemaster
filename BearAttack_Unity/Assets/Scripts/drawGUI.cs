using UnityEngine;
using System.Collections;

public class drawGUI : MonoBehaviour {
	
	public Texture overlay;
	public Texture shitOutline;
	public Texture shitOverlay;
	
	public float scale;
	public float shitScale;
	
	public float x;
	public float y;
	
	public float x2;
	public float y2;
	
	
	void OnGUI()
	{
		if (GameObject.Find("Player").GetComponent<bearHealth>().shit >= 100f)
		{
			GameObject.Find("Player").GetComponent<bearHealth>().shit = 100;
			
			GUI.DrawTexture(new Rect(x, Screen.height-y, 729f*shitScale, 620f*shitScale), shitOverlay);
		}
		
		GUI.DrawTexture(new Rect(x2, Screen.height-y2, 647f*scale, 250.9f*scale), overlay);
		
		GUI.DrawTexture(new Rect(x, Screen.height-y, 729f*shitScale, 620f*shitScale), shitOutline);
	}
}
