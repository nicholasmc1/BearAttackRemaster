using UnityEngine;
using System.Collections;

public class spawnHunter : MonoBehaviour {
	
	public GameObject hunter;
	public float spawnSpeed;
	public int spawnLimit;
	
	// Use this for initialization
	void Start ()
	{
		StartCoroutine(spawn());
	}
	
	IEnumerator spawn()
    {
        while(true)
		{	
			Instantiate(hunter, transform.position, Quaternion.LookRotation(GameObject.Find("Player").transform.position));
			yield return new WaitForSeconds(spawnSpeed * (float)GameObject.Find("EnemySpawner").GetComponent<spawnEnemy>().hunterSpawnRate *.1f);
        }
    }
}
