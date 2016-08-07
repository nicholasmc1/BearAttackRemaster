﻿using UnityEngine;
using System.Collections;

public class spawnHermit : MonoBehaviour {
	
	public GameObject hermit;
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
			if(GameObject.FindGameObjectsWithTag("Food").Length < spawnLimit)
			{
				Instantiate(hermit, transform.position, Quaternion.LookRotation(GameObject.Find("Player").transform.position));
			}
			
			yield return new WaitForSeconds(spawnSpeed * (float)GameObject.Find("EnemySpawner").GetComponent<spawnEnemy>().hermitSpawnRate *.1f);
        }
    }
}
