using UnityEngine;
using System.Collections;

public class ForestedAreaTrigger : MonoBehaviour
{
    private moveChar player;
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<moveChar>();
    }

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.layer == 13)
			player.EndSlowdown();
	}
}
