using UnityEngine;
using System.Collections;

public class SpookySpawn : MonoBehaviour {
	
	public float hermitSpawnRate;
	public float loggerSpawnRate;
	public float hunterSpawnRate;
	
	public GameObject hermit;
	public GameObject logger;
	public GameObject hunter;
	
	public float spawnLimit;
	
	// Use this for initialization
	void Start ()
	{
		StartCoroutine(SpawnDelay());
		//StartCoroutine(spawnLogger());
		//StartCoroutine(spawnHunter());
		
		//StartCoroutine(levelUp());
	}

	IEnumerator SpawnDelay()
	{
		yield return new WaitForSeconds(10f);
		StartCoroutine(spawnHermit());
	}
	
	void Update()
	{
		//print ((int)Mathf.Sqrt(GameObject.FindGameObjectsWithTag("Forest").Length));
		spawnLimit = GameObject.FindGameObjectsWithTag("Forest").Length;
	}
	
	public void spawnSingleHermit()
	{
		int range =(int)Mathf.Sqrt(GameObject.FindGameObjectsWithTag("Forest").Length) * 3;
				
		int side = Random.Range(0,100);
		
		if(side >= 0 && side < 25)
		{
			Instantiate(hermit, new Vector3(Random.Range(-range, range), 0, range), Quaternion.LookRotation(GameObject.Find("Player").transform.position));
		}
		
		if(side >= 25 && side < 50)
		{
			Instantiate(hermit, new Vector3(Random.Range(-range, range), 0, -range), Quaternion.LookRotation(GameObject.Find("Player").transform.position));
		}
		
		if(side >= 50 && side < 75)
		{
			Instantiate(hermit, new Vector3(range, 0, Random.Range(-range, range)), Quaternion.LookRotation(GameObject.Find("Player").transform.position));
		}
		
		if(side >= 75 && side <= 100)
		{
			Instantiate(hermit, new Vector3(-range, 0, Random.Range(-range, range)), Quaternion.LookRotation(GameObject.Find("Player").transform.position));
		}
	}
	
	public void spawnSingleLogger()
	{
		int range =(int)Mathf.Sqrt(GameObject.FindGameObjectsWithTag("Forest").Length) * 5;
				
		int side = Random.Range(0,100);
				
		if(side >= 0 && side < 25)
		{
			Instantiate(logger, new Vector3(Random.Range(-range, range), 0, range), Quaternion.LookRotation(GameObject.Find("Player").transform.position));
		}
		
		if(side >= 25 && side < 50)
		{
			Instantiate(logger, new Vector3(Random.Range(-range, range), 0, -range), Quaternion.LookRotation(GameObject.Find("Player").transform.position));
		}
		
		if(side >= 50 && side < 75)
		{
			Instantiate(logger, new Vector3(range, 0, Random.Range(-range, range)), Quaternion.LookRotation(GameObject.Find("Player").transform.position));
		}
		
		if(side >= 75 && side <= 100)
		{
			Instantiate(logger, new Vector3(-range, 0, Random.Range(-range, range)), Quaternion.LookRotation(GameObject.Find("Player").transform.position));
		}
	}
	
	public void spawnSingleHunter()
	{
		int range =(int)Mathf.Sqrt(GameObject.FindGameObjectsWithTag("Forest").Length) * 4;
				
		int side = Random.Range(0,100);
				
		if(side >= 0 && side < 25)
		{
			Instantiate(hunter, new Vector3(Random.Range(-range, range), 0, range), Quaternion.LookRotation(GameObject.Find("Player").transform.position));
		}
		
		if(side >= 25 && side < 50)
		{
			Instantiate(hunter, new Vector3(Random.Range(-range, range), 0, -range), Quaternion.LookRotation(GameObject.Find("Player").transform.position));
		}
		
		if(side >= 50 && side < 75)
		{
			Instantiate(hunter, new Vector3(range, 0, Random.Range(-range, range)), Quaternion.LookRotation(GameObject.Find("Player").transform.position));
		}
		
		if(side >= 75 && side <= 100)
		{
			Instantiate(hunter, new Vector3(-range, 0, Random.Range(-range, range)), Quaternion.LookRotation(GameObject.Find("Player").transform.position));
		}
	}
	
	IEnumerator spawnHermit()
    {
		bool first = true;
		
        while(true)
        {
			if(first == true)
			{
				first = false;
			}
			
			else if(GameObject.FindGameObjectsWithTag("Food").Length < spawnLimit)
			{
				spawnSingleHermit();
			}
			
			yield return new WaitForSeconds(1/hermitSpawnRate);
			
        }
    }
	
	IEnumerator spawnLogger()
    {
		bool first = true;
		
        while(true)
        {
			if(first == true)
			{
				first = false;
			}
			
			else if(GameObject.FindGameObjectsWithTag("Food").Length < spawnLimit)
			{
				spawnSingleLogger();
			}
			
			yield return new WaitForSeconds(1/loggerSpawnRate);
        }
    }
	
	IEnumerator spawnHunter()
    {
		bool first = true;
		
        while(true)
        {
			if(first == true)
			{
				first = false;
			}
			
			
			else if(GameObject.FindGameObjectsWithTag("Food").Length < spawnLimit)
			{
				spawnSingleHunter();
			}
			
			yield return new WaitForSeconds(1/hunterSpawnRate);
        }
    }
	
	IEnumerator levelUp()
    {
        while(true)
        {
			hermitSpawnRate -= .5f;
			
			loggerSpawnRate += .2f;
			
			hunterSpawnRate += .2f;
			
			yield return new WaitForSeconds(1);
			
        }
    }
}
