using UnityEngine;
using System.Collections;

public class colSound : MonoBehaviour
{
	void OnCollisionEnter(Collision col)
	{
		if(transform.parent.GetComponent<AudioSource>().isPlaying == false)
			transform.parent.GetComponent<AudioSource>().Play();
	}
}
