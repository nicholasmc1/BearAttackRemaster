using UnityEngine;
using System.Collections;

public class faceCam : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Look at including x and z leaning
		
		if((Camera.main.transform.position - transform.position).magnitude < 30)
		{
			foreach( Transform child in transform)
			{
				if(child.GetComponent<Renderer>())
					child.GetComponent<Renderer>().enabled = true;
			}
		    /*transform.LookAt(Camera.main.transform.position);
		 
		    // Euler angles are easier to deal with. You could use Quaternions here also
		    // C# requires you to set the entire rotation variable. You can't set the individual x and z (UnityScript can), so you make a temp Vec3 and set it back
		    Vector3 eulerAngles = transform.rotation.eulerAngles;
		    eulerAngles.x = 0;
		    eulerAngles.z = 0;
		 
		    // Set the altered rotation back
		    transform.rotation = Quaternion.Euler(eulerAngles);*/
		}
		
		else
		{
			foreach( Transform child in transform)
			{
				if(child.GetComponent<Renderer>())
					child.GetComponent<Renderer>().enabled = false;
			}
		}
	}
}
