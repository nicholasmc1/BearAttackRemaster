using UnityEngine;
using System.Collections;

public class moveChar : MonoBehaviour
{
	// how fast the bear moves
	private float _baseMoveSpeed;
	public float moveSpeed;
	
	public float maxVelocity;
	
	private Quaternion newRotation;
	
	// how fast the bear model rotates
	public float damping;


	public bool slowdown;
	
	public AudioSource feetSound;

	private Footsteps fs;
    private Rigidbody myRigid;
    private bearHealth myHealth;
    private Transform bearVisual;
	void Start()
	{
		_baseMoveSpeed = moveSpeed;
		fs = GetComponentInChildren<Footsteps>();
        myRigid = GetComponent<Rigidbody>();
        myHealth = GetComponent<bearHealth>();
        bearVisual = transform.FindChild("Player Model");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
  
		// if bear is alive
        if (myHealth.health > 0)
		{
			//slowdown from deforested
			if(slowdown && moveSpeed >= 2.5f)
			{
				moveSpeed -= 0.5f;
				fs.SlowdownFootstep();
			}
			
			Vector3 moveVec = Camera.main.transform.forward;
			moveVec = new Vector3(moveVec.x, 0, moveVec.z);
			
			myRigid.AddForce(moveVec * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime * 60);
			
			
			moveVec = Camera.main.transform.right;
            myRigid.AddForce(moveVec * moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime * 60);


            Vector3 velocity = myRigid.velocity;
		    if (velocity == Vector3.zero) return;
		 
		    float magnitude = velocity.magnitude;
		    if (magnitude > maxVelocity)
		    {
		        velocity *= (maxVelocity / magnitude);
                myRigid.velocity = velocity;
		    }


			// make the bear smoothly rotate to face the direction of movement
            if (Mathf.Abs(velocity.x) > 0 || Mathf.Abs(velocity.z) > 0)
                newRotation = Quaternion.LookRotation(new Vector3(velocity.x, 0, velocity.z) * 10000);

            bearVisual.transform.rotation = Quaternion.Slerp(bearVisual.transform.rotation, newRotation, Time.deltaTime * damping);
			
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
