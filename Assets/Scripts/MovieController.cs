using UnityEngine;
using System.Collections;

public class MovieController : SwitchScript {

	public MovieTexture movie;
	// Use this for initialization
	void Start () {
		Renderer r = GetComponent<Renderer>();
		r.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.M)) {

			this.Switch ();
		}
	}
	override public void Switch() {
		
		Renderer r = GetComponent<Renderer>();
		movie = (MovieTexture)r.material.mainTexture;

		if (movie.isPlaying) {
			movie.Pause();
		}
		else {
			movie.Play();
		}
		r.enabled = movie.isPlaying;
	}
	public override void Dim (float zeroToOne)
	{
		return;
	}
}
