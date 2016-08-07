using UnityEngine;
using System.Collections;

public class bearHealth : MonoBehaviour
{
	// The style of the health and shit bars
	public GUIStyle normal;
	
	// The style of shit bar when ready
	public GUIStyle ready;
	
	// The bears current health
	public float health;
	
	// How much shit the bear currently has
	public float shit;
	
	private bool died = false;
	public AudioSource deathSound;
	public AudioSource deathShotSound;
	
	public float marginH;
	public float marginV;
	public float marginHTwo;
	public float marginVTwo;
	
	
	public float healthScale;
	public float healthScale2;
	public float shitScale;
	
	void FixedUpdate()
	{
		if(died)
			return;

		// Decreases the health slowly over time
		health -= .1f;
		
		// Health decreases faster if bear is moving
		health -= GetComponent<Rigidbody>().velocity.magnitude * .01f;
		
		if(health > 300)	
		{
			shit += health - 300;
			health = 300;
		}
		
		if(health <= 0 && died == false)
		{
			died = true;

			if(health <= -1)
				deathShotSound.Play();
			else if(health <= 0)
				deathSound.Play();
		}
	}
	
	void OnGUI()
	{		
		// If the bear is alive, display GUI
		if(health > 0)
		{
			
			Rect healthRect = new Rect(marginH, Screen.height-marginV, health * healthScale, healthScale2);
			GUI.Box(healthRect, "", normal);
			
			if(shit > 0 && shit <= 100f)
			{
				Rect shitRect = new Rect(Screen.width-marginHTwo, Screen.height-marginVTwo, 193, shit * shitScale);
				GUI.Box(shitRect, "", ready);
			}
		}
	}
}
