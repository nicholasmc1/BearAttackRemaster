using UnityEngine;
using System.Collections;

public class Footsteps : MonoBehaviour {

	private Rigidbody _myMotor;
	public AudioClip footstep;
	public AudioClip footstepDeforested;

	public float footstepGap;	
	public bool footstepsEnabled = true;

	//private bool canPlayRoll = true;

	public float runningGap;
	public float walkingGap;

	public bool slowed;

	// Use this for initialization
	IEnumerator Start () {

		_myMotor = GameObject.Find("Player").GetComponent<Rigidbody>();

		while(true)
		{
			if(footstepsEnabled && _myMotor.velocity.magnitude > 1f)
			{
				if(!slowed)
					GetComponent<AudioSource>().PlayOneShot(footstep);
				else
					GetComponent<AudioSource>().PlayOneShot(footstepDeforested);
				yield return new WaitForSeconds(footstepGap);
			}
			else
				yield return 0;
		}

	}

	public void SlowdownFootstep()
	{
		slowed = true;
		if(footstepGap <= walkingGap)
			footstepGap += 0.05f;
	}

	public void ResetFootstep()
	{
		footstepGap = runningGap;
		slowed = false;
	}
	
}
