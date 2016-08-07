using UnityEngine;
using System.Collections;

public class makeLevel : MonoBehaviour {
	
	public GameObject deforestPrefab;
	
	public GameObject forestPrefab;
	public GameObject flowerPrefab;
	public GameObject mushroomPrefab;
	public GameObject redBushPrefab;
	public GameObject blueBushPrefab;
	
	public GameObject groundPrefab;
	public GameObject deforestGroundPrefab;
	public GameObject grassPrefab;
	public GameObject forestEdgePrefab;
	
	// Use this for initialization
	void Start ()
	{
		// loop through all possible tile positions
		for(int x = -60; x < 60; x += 10)
		{
			for(int z = -60; z < 60; z += 10)
			{
				// Make sure tree doesn't spawn where player is
				if(x == 0 && z == 0)
				{
					Instantiate(groundPrefab, new Vector3(x, 0, z), Quaternion.identity);
				}
				
				// make deforested perimeter at edge of world
				else if(x <= -60 || z <= -60 || x >= 50 || z >= 50)
				{
					Instantiate(deforestGroundPrefab, new Vector3(x, 0, z), Quaternion.identity);
				}
				
				// make deforested perimeter at edge of world
				else if(x <= -50 || z <= -50 || x >= 40 || z >= 40)
				{
					Instantiate(forestEdgePrefab, new Vector3(x, 0, z), Quaternion.identity);
				}
				
				// for everywhere else, spawn a tree
				else
				{
					Instantiate(randomForest(), new Vector3(x, 0, z), Quaternion.identity);
				}
			}
		}
	}
	
	private GameObject randomGround()
	{
		return groundPrefab;
	}
	
	private GameObject randomForest()
	{
		int choice = Random.Range(0,101);
		
		if(choice >= 90)
		{
			return mushroomPrefab;
		}
		
		if(choice < 90 && choice >= 70)
		{
			return flowerPrefab;
		}
		
		if(choice < 70 && choice >= 67)
		{
			return redBushPrefab;
		}
		
		if(choice < 67 && choice >= 65)
		{
			return blueBushPrefab;
		}
		
		else
		{
			return forestPrefab;
		}
	}
}
