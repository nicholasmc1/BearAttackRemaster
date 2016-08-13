using UnityEngine;
using System.Collections;

public class spawnEnemy : MonoBehaviour {
	
	public int hermitSpawnRate;
	public int loggerSpawnRate;
	public int hunterSpawnRate;
	
	public GameObject loggerSpawner;
	public GameObject hunterSpawner;
	
	public float spawnLimit;
	
	public bool first = true;
	
	// Use this for initialization
	void Start ()
	{
		StartCoroutine(levelUp());
	}
	
	void Update()
	{
		//spawnLimit = GameObject.FindGameObjectsWithTag("Forest").Length;
		
		//print ("hermit: " + hermitSpawnRate);
		//print ("logger: " + loggerSpawnRate);
		//print ("hunter: " + hunterSpawnRate);
	}
	
	IEnumerator levelUp()
    {
        while(true)
        {
			if(first == true)
			{
				first = false;
			}
			
			else
			{
				if(GameObject.FindGameObjectsWithTag("LoggerSpawn").Length < 10)
				{
					spawnLoggerSpawner();
				}
				
				if(GameObject.FindGameObjectsWithTag("LoggerSpawn").Length > 2)
				{
					spawnHunterSpawner();
				}
			}
			
			yield return new WaitForSeconds(30f);
			
        }
    }
	
	public void spawnLoggerSpawner()
	{
		int range = 6;
				
		int side = Random.Range(0,100);
		
		if(side >= 0 && side < 25)
		{
			Instantiate(loggerSpawner, new Vector3(Random.Range(-range + 1, range - 1), 0, range - 1) * 10, Quaternion.LookRotation(transform.position + new Vector3(0,0,-10000)));
		}
		
		if(side >= 25 && side < 50)
		{
			Instantiate(loggerSpawner, new Vector3(Random.Range(-range + 1, range - 1), 0, -range) * 10, Quaternion.LookRotation(transform.position + new Vector3(0,0,10000)));
		}
		
		if(side >= 50 && side < 75)
		{
			Instantiate(loggerSpawner, new Vector3(range - 1, 0, Random.Range(-range + 1, range - 1)) * 10, Quaternion.LookRotation(transform.position + new Vector3(-10000,0,0)));
		}
		
		if(side >= 75 && side <= 100)
		{
			Instantiate(loggerSpawner, new Vector3(-range, 0, Random.Range(-range + 1, range - 1)) * 10, Quaternion.LookRotation(transform.position + new Vector3(10000,0,0)));
		}
	}
	
	public void spawnHunterSpawner()
	{
		int range = 6;
				
		int side = Random.Range(0,100);
		
		if(side >= 0 && side < 25)
		{
			Instantiate(hunterSpawner, new Vector3(Random.Range(-range + 1, range - 1), 0, range - 1) * 10, Quaternion.LookRotation(transform.position + new Vector3(0,0,-10000)));
		}
		
		if(side >= 25 && side < 50)
		{
			Instantiate(hunterSpawner, new Vector3(Random.Range(-range + 1, range - 1), 0, -range) * 10, Quaternion.LookRotation(transform.position + new Vector3(0,0,10000)));
		}
		
		if(side >= 50 && side < 75)
		{
			Instantiate(hunterSpawner, new Vector3(range - 1, 0, Random.Range(-range + 1, range - 1)) * 10, Quaternion.LookRotation(transform.position + new Vector3(-10000,0,0)));
		}
		
		if(side >= 75 && side <= 100)
		{
			Instantiate(hunterSpawner, new Vector3(-range, 0, Random.Range(-range + 1, range - 1)) * 10, Quaternion.LookRotation(transform.position + new Vector3(10000,0,0)));
		}
	}
}
