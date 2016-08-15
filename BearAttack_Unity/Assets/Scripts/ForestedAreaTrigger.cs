using UnityEngine;
using System.Collections;

public class ForestedAreaTrigger : MonoBehaviour
{
    private moveChar player;
    public GameObject grassParticle;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<moveChar>();
        if(grassParticle != null)
            Instantiate(grassParticle, transform, false);
    }

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.layer == 13)
			player.EndSlowdown();
	}
}
