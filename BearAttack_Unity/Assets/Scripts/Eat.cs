using UnityEngine;
using System.Collections;

public class Eat : MonoBehaviour {
	
	public GameObject hermitGib;
	public GameObject loggerGib;
	public GameObject hunterGib;
	
	public GameObject bloodParticle;
	
	private bool slowMo = false;

	public AudioSource slowMoSound;
	
	void Update()
	{
		if(slowMo == true)
		{
			Time.timeScale = Mathf.Lerp(Time.timeScale, .3f, Time.deltaTime * 10);
		}
		
		else
		{
			Time.timeScale = Mathf.Lerp(Time.timeScale, 1, Time.deltaTime * 10);
		}
		
		if(Time.timeScale < .31f)
		{
			slowMo = false;
		}
	}
	
	void OnCollisionEnter(Collision col)
	{
		if(GetComponent<bearHealth>().health > 0)
		{
			if(col.gameObject.tag == "Food")
			{
				Destroy(Instantiate(bloodParticle, col.transform.position, col.transform.rotation), 2f);
				
				GameObject.Find("Bear_Object").GetComponent<animate>().playAnim("attacking", .1f,.4f);
				
				GetComponent<bearHealth>().health += 50;
				GetComponent<bearHealth>().shit += 20;
				
				
				if(col.gameObject.name == "Hermit(Clone)")
				{
					if(GameObject.Find("EnemySpawner").GetComponent<spawnEnemy>().hermitSpawnRate < 21)
					{
						GameObject.Find("EnemySpawner").GetComponent<spawnEnemy>().hermitSpawnRate += 1;
					}
					
					if(GameObject.Find("EnemySpawner").GetComponent<spawnEnemy>().loggerSpawnRate > 5)
					{
						GameObject.Find("EnemySpawner").GetComponent<spawnEnemy>().loggerSpawnRate -= 1;
					}
					
					GetComponent<AudioSource>().Play();
					
					GameObject temp = Instantiate(hermitGib, transform.position, transform.rotation) as GameObject;
					
					foreach(Transform child in temp.transform)
					{
						child.GetComponent<Rigidbody>().AddForce(col.rigidbody.velocity * 100);
					}
					
					Destroy(col.gameObject);
				}
				
				else if(col.gameObject.name == "Logger(Clone)")
				{
					if(GameObject.Find("EnemySpawner").GetComponent<spawnEnemy>().loggerSpawnRate < 21)
					{
						GameObject.Find("EnemySpawner").GetComponent<spawnEnemy>().loggerSpawnRate += 1;
					}
					
					if(GameObject.Find("EnemySpawner").GetComponent<spawnEnemy>().hunterSpawnRate > 3)
					{
						GameObject.Find("EnemySpawner").GetComponent<spawnEnemy>().hunterSpawnRate -= 1;
					}
					
					GetComponent<AudioSource>().Play();
					
					GameObject temp = Instantiate(loggerGib, transform.position, transform.rotation) as GameObject;
					
					foreach(Transform child in temp.transform)
					{
						child.GetComponent<Rigidbody>().AddForce(col.rigidbody.velocity * 100);
					}
					
					Destroy(col.gameObject);
				}
				
				else if(col.gameObject.name == "Hunter(Clone)")
				{
					slowMo = true;
					
					print ("eat hunter");
					
					if(GameObject.Find("EnemySpawner").GetComponent<spawnEnemy>().hunterSpawnRate < 21)
					{
						GameObject.Find("EnemySpawner").GetComponent<spawnEnemy>().hunterSpawnRate += 1;
					}
					
					if(GameObject.Find("EnemySpawner").GetComponent<spawnEnemy>().hermitSpawnRate > 3)
					{
						GameObject.Find("EnemySpawner").GetComponent<spawnEnemy>().hermitSpawnRate -= 1;	
					}
					
					slowMoSound.Play();
					GetComponent<AudioSource>().Play();
					
					GameObject temp = Instantiate(hunterGib, transform.position, transform.rotation) as GameObject;
					
					foreach(Transform child in temp.transform)
					{
						child.GetComponent<Rigidbody>().AddForce(col.rigidbody.velocity * 100);
					}
					
					Destroy(col.gameObject);
				}
			}
		}
	}
}
