using UnityEngine;
using System.Collections;

public class Eat : MonoBehaviour 
{

	public GameObject bloodParticle;
	
	private bool slowMo = false;

	public AudioSource slowMoSound;
    private animate anims;
    private spawnEnemy enemyManager;
    private bearHealth _myHealth;
    void Start()
    {
        anims = GetComponentInChildren<animate>();
        enemyManager = GameObject.Find("EnemySpawner").GetComponent<spawnEnemy>();
        _myHealth = GetComponent<bearHealth>();
    }

	void Update()
	{
		if(slowMo == true)
			Time.timeScale = Mathf.Lerp(Time.timeScale, .3f, Time.deltaTime * 10);
		else
			Time.timeScale = Mathf.Lerp(Time.timeScale, 1, Time.deltaTime * 10);
		
		if(Time.timeScale < .31f)
			slowMo = false;

        if (Input.GetKeyDown(KeyCode.Alpha0))
            anims.playAnim("attack", 0, .4f);
            
	}
	
	void OnCollisionEnter(Collision col)
	{
		if(GetComponent<bearHealth>().health > 0)
		{
			if(col.gameObject.tag == "Food")
			{
				Destroy(Instantiate(bloodParticle, col.transform.position, col.transform.rotation), 2f);

                anims.playAnim("attack", .1f, .4f);

                _myHealth.health += 50;
                _myHealth.shit += 20;

                col.gameObject.GetComponent<Gibber>().SmashEm(GetComponent<Rigidbody>().velocity);
				
				if(col.gameObject.name == "Hermit(Clone)")
				{
                    if (enemyManager.hermitSpawnRate < 21)
                        enemyManager.hermitSpawnRate += 1;

                    if (enemyManager.loggerSpawnRate > 5)
                        enemyManager.loggerSpawnRate -= 1;


				}
				
				else if(col.gameObject.name == "Logger(Clone)")
				{
                    if (enemyManager.loggerSpawnRate < 21)
                        enemyManager.loggerSpawnRate += 1;

                    if (enemyManager.hunterSpawnRate > 3)
                        enemyManager.hunterSpawnRate -= 1;

					
				}
				
				else if(col.gameObject.name == "Hunter(Clone)")
				{
					slowMo = true;
					

                    if (enemyManager.hunterSpawnRate < 21)
                        enemyManager.hunterSpawnRate += 1;

                    if (enemyManager.hermitSpawnRate > 3)
                        enemyManager.hermitSpawnRate -= 1;


				}
			}
		}
	}
}
