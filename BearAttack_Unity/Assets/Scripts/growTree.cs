using UnityEngine;
using System.Collections;

public class growTree : MonoBehaviour 
{

    public makeLevel gen;

    void Awake()
    {
        gen = GameObject.Find("LevelGenerator").GetComponent<makeLevel>();
        
    }

	void OnTriggerStay(Collider col)
	{
		if(col.transform.tag == "Shit")
		{
			Instantiate(gen.randomForest(), new Vector3(transform.position.x, 0, transform.position.z), Quaternion.identity, gen.levelParent);
			Destroy(col.gameObject);
			Destroy(transform.parent.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other){
		
		if(other.transform.tag == "Player")
		{
			GameObject.Find("Player").GetComponent<moveChar>().slowdown = true;
		}
	}
}