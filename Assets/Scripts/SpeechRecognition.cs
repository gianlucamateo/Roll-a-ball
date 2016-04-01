using UnityEngine;
using System.Collections;

public class SpeechRecognition : MonoBehaviour {

	private int count = 0;
	public PlayerController james;
	// Use this for initialization
	void Start () {
		
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null)
		{
			Debug.Log("WWW Ok!: " + www.text);
			if (www.text.Contains ("James")) {
				james.Navigate ();
			}
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}
	
	// Update is called once per frame
	void Update () {
		if (count++ % 20 == 0) {
			string url = "http://localhost:2960/api/speech";
			WWW www = new WWW (url);
			StartCoroutine (WaitForRequest (www));
		}
	}
}
