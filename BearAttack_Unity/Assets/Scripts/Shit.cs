using UnityEngine;
using System.Collections;

public class Shit : MonoBehaviour {

	public GameObject shitPrefab;
	public AudioSource shitSound;
    public Transform ass;
    private bool canShit = true;
    private bearHealth health;
    private animate anims;
	// Update is called once per frame
    void Start()
    {
        health = GetComponent<bearHealth>();
        anims = GetComponentInChildren<animate>();
    }
	void Update ()
	{
        if (canShit && Input.GetKey(KeyCode.Space))
        {
            canShit = false;
            StartCoroutine(DoAShit());
        }
	}

	IEnumerator DoAShit()
	{
        health.shit = 0;
        anims.playAnim("poop", .05f, 2f);

		yield return new WaitForSeconds(0.15f);

		shitSound.Play();
        GameObject temp = Instantiate(shitPrefab, ass.position, Quaternion.identity) as GameObject;
        temp.GetComponent<Rigidbody>().AddForce(ass.forward * 400);
        canShit = true;
	}
}
