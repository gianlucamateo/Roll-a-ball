using UnityEngine;
using System.Collections;

public class MovieController : MonoBehaviour {

	public MovieTexture movie;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.M)) {

			Renderer r = GetComponent<Renderer>();
			movie = (MovieTexture)r.material.mainTexture;

			if (movie.isPlaying) {
				movie.Pause();
			}
			else {
				movie.Play();
			}
		}
	}
}
