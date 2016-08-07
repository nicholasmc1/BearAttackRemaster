using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {
	private int _phase = 0;
	private TextMesh _tm;

	private bearHealth _bh;
	// BearBhea this for initialization
	void Start () {
		_tm = GetComponent<TextMesh>();
		_tm.text = "Move\nWASD";

		_bh = GameObject.Find("Player").GetComponent<bearHealth>();
		iTween.FadeTo(gameObject, 0, 0.1f);
		iTween.FadeTo(gameObject, 1, 1f);

		if(PlayerPrefs.HasKey("Tutorial") && PlayerPrefs.GetInt("Tutorial")==1)
			Destroy(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		

		//phases

		switch(_phase)
		{
			case -1:
				//wait

				break;

			case 0:
				//WASD
				if(Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("s") || Input.GetKeyDown("d"))
				{
					
					StartCoroutine(ChangeText("Camera Control\nMouse", _phase+1));
					_phase = -1;
				}

				break;

			case 1:
				//camera
				if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
				{
					//StartCoroutine(ChangeText("dick", _phase+1));
					iTween.FadeTo(gameObject, 0, 1f);
					_phase = 2;
				}

				break;

			case 2:
				//wait for poo
				//get bearhealth
				if(_bh.shit >=100)
				{
					_phase=-1;
					StartCoroutine(PooPrompt());
				}

				break;


			case 3:
				//poo
				if(Input.GetKeyDown("space"))
				{	
					StartCoroutine(EndTutorial());
				}
				break;


		}
	}


	IEnumerator ChangeText(string text, int ph)
	{
		iTween.FadeTo(gameObject, 0, 1f);

		yield return new WaitForSeconds(1.2f);

		
		_tm.text = text;
		iTween.FadeTo(gameObject, 1, 1f);

		yield return new WaitForSeconds(1f);

		_phase = ph;
	}


	IEnumerator PooPrompt()
	{
		_tm.text = "Poop\nSPACE";

		iTween.FadeTo(gameObject, 1, 1f);

		yield return new WaitForSeconds(1f);

		_phase = 3;
	}

	IEnumerator EndTutorial()
	{
		iTween.FadeTo(gameObject, 0, 1f);
		yield return new WaitForSeconds(1.2f);
		PlayerPrefs.SetInt("Tutorial", 1);
		Destroy(gameObject);
	}
}
