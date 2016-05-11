using UnityEngine;
using System.Collections;

public abstract class SwitchScript : MonoBehaviour {

	public Vector3 offset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public abstract void Dim (float zeroToOne);

	public abstract void Switch ();
}
