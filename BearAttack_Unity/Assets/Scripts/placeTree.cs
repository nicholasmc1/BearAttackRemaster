using UnityEngine;
using System.Collections;

public class placeTree : MonoBehaviour {
	
	Color newCol;
	
	// Use this for initialization
	void Start ()
	{
		transform.Translate(new Vector3(Random.Range(-4f,4f), 0, Random.Range(-4f,4f)));
		
		transform.localScale = new Vector3(Random.Range(.5f,1f), Random.Range(1f,2f), Random.Range(.5f,1f));
		
		transform.eulerAngles = new Vector3(0, Random.Range(0,360), 0);
		
		int choice = Random.Range(1,8);
		
		if(choice == 1)
		{
			newCol = new Color(45f/255f,101f/255f,26f/255f);
		}
		
		else if(choice == 2)
		{
			newCol = new Color(31f/255f,58f/255f,42f/255f);
		}
		
		else if(choice == 3)
		{
			newCol = new Color(23f/255f,42f/255f,10f/255f);
		}
		
		else if(choice == 4)
		{
			newCol = new Color(31f/255f,43f/255f,31f/255f);
		}
		
		else if(choice == 5)
		{
			newCol = new Color(48f/255f,47f/255f,15f/255f);
		}
		
		else if(choice == 6)
		{
			newCol = new Color(9f/255f,26f/255f,1f/255f);
		}
		
		else if(choice == 7)
		{
			newCol = new Color(36f/255f,66f/255f,16f/255f);
		}
		
		transform.FindChild("Pine_tree_1_MESH").GetComponent<Renderer>().material.color = newCol;
		
		
		//transform.Translate(new Vector3(Random.Range(-4f,4f), 0, Random.Range(-4f,4f)));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
