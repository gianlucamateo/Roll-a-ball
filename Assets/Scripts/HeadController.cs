using UnityEngine;
using System.Collections;

public class HeadController : MonoBehaviour {

	public float angle;
	public GameObject body;
	public Vector3 baseOffset, variableOffset, direction;
	public float x = 0;

	// Use this for initialization
	void Start () {
		variableOffset = Vector3.zero;	
	}
	
	// Update is called once per frame
	void Update () {

		direction = body.GetComponent<PlayerController> ().rb.velocity;
		float headingAngle = Vector3.Angle (Vector3.forward, direction);
		float headingSign = Vector3.Cross (Vector3.forward, direction).y;
		if (headingSign < 0)
			headingSign = -1;
		else
			headingSign = 1;
		x += 1f;
		if (variableOffset.magnitude > 1)
			variableOffset.Normalize ();
		Vector3 totaloffset = baseOffset + variableOffset/3f;
		totaloffset.Normalize ();
		angle = Vector3.Angle (Vector3.up, totaloffset);
		Vector3 rotAxis = Vector3.Cross (Vector3.up, totaloffset);
		float sign = rotAxis.z;
		if (sign > 0)
			sign = -1;
		else
			sign = 1;
		totaloffset *= 0.6f;
		transform.position = body.transform.position + totaloffset;
		transform.rotation = Quaternion.AngleAxis (angle, rotAxis) * Quaternion.Euler (0, headingAngle * headingSign, 0);//Quaternion.Euler (0, 90, 0) * Quaternion.Euler(angle*sign,0,0);

	}
}
