using UnityEngine;
using System.Collections;

public class BlindController : SwitchScript {
	public GameObject Blinds;
	public bool up = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Switch() {
		print ("playing animation");
		if (up) {
			Blinds.GetComponent<Animator> ().Play ("BlindLower");
			up = false;
		} else {
			Blinds.GetComponent<Animator> ().Play ("BlindRise");
			up = true;
		}
	}
	public override void Dim (float zeroToOne)
	{
		return;
	}
}
