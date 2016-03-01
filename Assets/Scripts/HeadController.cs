using UnityEngine;
using System.Collections;

public class HeadController : MonoBehaviour {

	public GameObject body;
	public Vector3 baseOffset, variableOffset;

	// Use this for initialization
	void Start () {
		variableOffset = Vector3.zero;	
	}
	
	// Update is called once per frame
	void Update () {

		if (variableOffset.magnitude > 1)
			variableOffset.Normalize ();
		Vector3 totaloffset = baseOffset + variableOffset/3f;
		totaloffset.Normalize ();
		totaloffset *= 0.5f;
		transform.position = body.transform.position + totaloffset;	

	}
}
