using UnityEngine;
using System.Collections;

public class rotateCam : MonoBehaviour {
	
	// set inital camera angle
	public float targetRotation = 45;
	
	// set rotation speed
	public float rotationSpeed;
	
	// initialise new rotation Quat
	Quaternion newRotation = Quaternion.identity;
	
	// Update is called once per frame
	void Update ()
	{
		// if Q is pressed, add 90 degrees to the desired camera rotation
		if(Input.GetKeyDown(KeyCode.E))
		{
			targetRotation += 90;
			
			// make sure rotation stays in bounds
			if(targetRotation > 360)
			{
				targetRotation = 45f;
			}
		}
		
		// if E is pressed, subtract 90 degrees to the desired camera rotation
		if(Input.GetKeyDown(KeyCode.Q))
		{
			targetRotation -= 90;
			
			// make sure rotation stays in bounds
			if(targetRotation < 0)
			{
				targetRotation = 315f;
			}
		}
		
		// initialise the initial rotation
		newRotation = transform.rotation;
		
		// set the 
		newRotation.eulerAngles = new Vector3(30, targetRotation, 0);
		
		transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed);
	}
}
