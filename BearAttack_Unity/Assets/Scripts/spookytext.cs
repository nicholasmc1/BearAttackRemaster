using UnityEngine;
using System.Collections;

public class spookytext : MonoBehaviour {

	private string _textToDraw;
	public string _text;
	public GUISkin gskin;

	private Rect _rect;

	private int _count;

	// Use this for initialization
	void Start () {
		NewRect();

		_textToDraw = "";

		StartCoroutine(AddText());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void NewRect()
	{
		_rect = new Rect(0,0, 500, 500);
		_count = 0;
	}

	void OnGUI() {


		GUI.skin = gskin;

		GUI.Box(_rect, _textToDraw);
	}

	IEnumerator AddText()
	{
		foreach(char letter in _text.ToCharArray())
		{
			_textToDraw += letter;

			yield return new WaitForSeconds(0.05f);
		}
	}
}
