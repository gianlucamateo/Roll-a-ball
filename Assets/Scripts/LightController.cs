using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

	public GameObject[] bulbs;
	public KeyCode code;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (code)) {
			Light l = (Light)this.gameObject.GetComponent<Light>();
			if (l.intensity == 1) {
				l.intensity = 0;

				foreach (GameObject bulb in bulbs) {
					bulb.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.black);
				}

			} else {
				l.intensity = 1;

				foreach (GameObject bulb in bulbs) {
					bulb.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", new Color(0.4f,0.4f,0.34f));
				}
			}

		}
	}
}
