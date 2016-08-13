using UnityEngine;
using System.Collections;

public class Gibber : MonoBehaviour 
{
    public AudioClip KillSound;
    private AudioSource _killSource;
    public AudioClip slowMoSound;
    private AudioSource _slowMoSource;
    public GameObject Gibs;
    public bool isHunter;
    void Awake()
    {
       _killSource =  gameObject.AddComponent<AudioSource>();
       _killSource.clip = KillSound;
       _killSource.spatialBlend = 1;
       if (isHunter)
       {
           _slowMoSource = gameObject.AddComponent<AudioSource>();
           _slowMoSource.clip = slowMoSound;
           _slowMoSource.spatialBlend = 1;
       }
    }

    public void SmashEm(Vector3 _velocity)
    {

        _killSource.Play();
        if (isHunter)
            _slowMoSource.Play();
        GameObject temp = Instantiate(Gibs, transform.position, transform.rotation) as GameObject;

        foreach (Transform child in temp.transform)
            child.GetComponent<Rigidbody>().AddForce(_velocity * 100);

        Destroy(gameObject);

    }
}
