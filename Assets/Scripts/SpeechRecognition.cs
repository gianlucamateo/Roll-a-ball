using UnityEngine;
using System.Collections;

public class SpeechRecognition : MonoBehaviour {

	private int count = 0;
	public PlayerController james;
	public TextMesh text;
	public InteractScript interact;
	// Use this for initialization
	void Start () {
		
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null)
		{
			if (www.text.Length>4) {
				text.text = www.text;
				print (www.text);
			}
			//Debug.Log("WWW Ok!: " + www.text);
			if (www.text.Contains ("James")) {
				print ("James");
				james.Navigate ();
			}
			if (www.text.ToLower ().Contains ("switch")) {
				interact.Switch();
			}
			if (www.text.ToLower ().Contains ("dim")) {
				print ("Dim");
				interact.Dim();
			}
			if (www.text.ToLower ().Contains ("okay")) {
				print ("okay");
				interact.EndDim();
			}

		} else {
			//Debug.Log("WWW Error: "+ www.error);
		}    
	}
	
	// Update is called once per frame
	void Update () {
		if (count++ % 10 == 0) {
			string url = "http://localhost:2960/api/speech";
			WWW www = new WWW (url);
			StartCoroutine (WaitForRequest (www));
		}
	}
}
