using UnityEngine;
using System.Collections;

public class makeLevel : MonoBehaviour 
{
	public GameObject[] forestPrefabs;

	public GameObject deforestGroundPrefab;
	public GameObject forestEdgePrefab;

    public Transform levelParent;
	// Use this for initialization
	void Start ()
	{
        levelParent = new GameObject("LevelChunks").transform;
        GameObject temp;
		// loop through all possible tile positions
		for(int x = -60; x < 60; x += 10)
			for(int z = -60; z < 60; z += 10)
			{
				// Make sure tree doesn't spawn where player is
                //currently spawned with the level
				/*if(x == 0 && z == 0)
				{
					Instantiate(groundPrefab, new Vector3(x, 0, z), Quaternion.identity);
				}*/
				
				// make deforested perimeter at edge of world
				if(x <= -60 || z <= -60 || x >= 50 || z >= 50)
					temp = (GameObject)Instantiate(deforestGroundPrefab, new Vector3(x, 0, z), Quaternion.identity);
				
				// make deforested perimeter at edge of world
				else if(x <= -50 || z <= -50 || x >= 40 || z >= 40)
					temp = (GameObject)Instantiate(forestEdgePrefab, new Vector3(x, 0, z), Quaternion.identity);
				
				// for everywhere else, spawn a tree
				else
					temp = (GameObject)Instantiate(randomForest(), new Vector3(x, 0, z), Quaternion.identity);

                temp.transform.parent = levelParent;
			}
	}

	
	public GameObject randomForest()
	{
        int choice = Random.Range(0, forestPrefabs.Length);


		return forestPrefabs[choice];
	}
}
