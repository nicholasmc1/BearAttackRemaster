using UnityEngine;
using System.Collections;

public class animate : MonoBehaviour {

	public Animator anim;
	private bearHealth health;
	
	private bool dieOnce = false;
	private bool animPlaying = false;
	
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
		health = transform.parent.parent.GetComponent<bearHealth>();
	}
	
	void Update()
	{
		if(health.health <= 0)
		{
			if(dieOnce == false)
			{
				
				StartCoroutine( PlayOneShot("dead", .2f, .5f) );
				dieOnce = true;
			}
		}
		
		else if(animPlaying == false)
		{

			anim.SetFloat("speed",transform.parent.parent.GetComponent<Rigidbody>().velocity.magnitude);
			
			if(transform.parent.parent.GetComponent<Rigidbody>().velocity.magnitude > 9f)
			{
				anim.speed = transform.parent.parent.GetComponent<Rigidbody>().velocity.magnitude * .09f;
			}
			
			else
			{
				anim.speed = .9f;
			}

			anim.SetFloat("Direction", Input.GetAxis("Horizontal"));
		}
	}
	
	public void playAnim(string paramName, float waitTime, float animSpeed)
	{
		StartCoroutine( PlayOneShot(paramName, waitTime, animSpeed) );
	}
	
	public IEnumerator PlayOneShot ( string paramName, float waitTime, float animSpeed)

    {
		if(anim.GetBool(paramName) != true)
		{
			anim.speed = animSpeed;
            anim.SetTrigger(paramName);
			animPlaying = true;
	
	        yield return new WaitForSeconds(waitTime);

			animPlaying = false;
		}

    }
}
