using UnityEngine;
using System.Collections;

public class LightController : SwitchScript {

	public GameObject[] bulbs;
	public KeyCode code;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (code)) {
			Switch ();
		}
	}

	public override void Switch(){
		Light l = (Light)this.gameObject.GetComponent<Light>();
		if (l.intensity > 0) {
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

	public override void Dim(float zeroToOne){
		Light l = (Light)this.gameObject.GetComponent<Light>();
		l.intensity = zeroToOne;
		foreach (GameObject bulb in bulbs) {
			bulb.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", new Color(0.4f*zeroToOne,0.4f*zeroToOne,0.34f*zeroToOne));
		}
		return;
	}
}
