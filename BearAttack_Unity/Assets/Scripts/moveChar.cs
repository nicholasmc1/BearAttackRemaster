using UnityEngine;
using System.Collections;

public class moveChar : MonoBehaviour
{
	// how fast the bear moves
	private float _baseMoveSpeed;
	public float moveSpeed;
	// how fast the bear turns
	public float rotationSpeed;
	
	public float maxVelocity;
	
	private Quaternion newRotation;
	
	// how fast the bear model rotates
	public float damping;


	public bool slowdown;
	
	public AudioSource feetSound;

	private Footsteps fs;

	void Start()
	{
		_baseMoveSpeed = moveSpeed;
		fs = GetComponentInChildren<Footsteps>();
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		Screen.lockCursor = true;
		
		// if bear is alive
		if(GetComponent<bearHealth>().health > 0)
		{
			//slowdown from deforested
			if(slowdown && moveSpeed >= 2.5f)
			{
				moveSpeed -= 0.5f;
				fs.SlowdownFootstep();
			}
			
			Vector3 moveVec = Camera.main.transform.forward;
			moveVec = new Vector3(moveVec.x, 0, moveVec.z);
			
			GetComponent<Rigidbody>().AddForce(moveVec * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime * 60);
			
			
			moveVec = Camera.main.transform.right;
			GetComponent<Rigidbody>().AddForce(moveVec * moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime * 60);
			
			
			Vector3 velocity = GetComponent<Rigidbody>().velocity;
		    if (velocity == Vector3.zero) return;
		 
		    float magnitude = velocity.magnitude;
		    if (magnitude > maxVelocity)
		    {
		        velocity *= (maxVelocity / magnitude);
		        GetComponent<Rigidbody>().velocity = velocity;
		    }


			// make the bear smoothly rotate to face the direction of movement
			if(Mathf.Abs(GetComponent<Rigidbody>().velocity.x) > 0 || Mathf.Abs(GetComponent<Rigidbody>().velocity.z) > 0)
				newRotation = Quaternion.LookRotation(new Vector3(GetComponent<Rigidbody>().velocity.x, 0, GetComponent<Rigidbody>().velocity.z) * 10000);
			
	    	transform.FindChild("Player Model").transform.rotation = Quaternion.Slerp(transform.FindChild("Player Model").transform.rotation, newRotation, Time.deltaTime * damping);
			
			/*if(rigidbody.velocity.magnitude > 1)
			{
				if(feetSound.isPlaying == false)
				{
					feetSound.Play();
				}
			}*/
		}
	}


	public void EndSlowdown()
	{
		slowdown = false;
		moveSpeed = _baseMoveSpeed;
		fs.ResetFootstep();
	}
}
