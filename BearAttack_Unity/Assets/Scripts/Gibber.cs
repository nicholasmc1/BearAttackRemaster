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
    private GameObject soundContainer;
    void Awake()
    {
       soundContainer = new GameObject();
       _killSource = soundContainer.AddComponent<AudioSource>();
       _killSource.clip = KillSound;
       if (isHunter)
       {
           _slowMoSource = soundContainer.AddComponent<AudioSource>();
           _slowMoSource.clip = slowMoSound;
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
        soundContainer.transform.parent = null;
        Destroy(soundContainer, 5);
        Destroy(gameObject);

    }
}
