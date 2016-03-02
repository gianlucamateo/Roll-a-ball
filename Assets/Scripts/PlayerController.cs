using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float thrust;
	public GameObject head;
	public Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}
    //before physics
    void FixedUpdate()
    {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);
		Vector3 vOffset = head.GetComponent<HeadController> ().variableOffset;
		vOffset += movement / 10;
		rb.AddForce (vOffset * thrust);
		if (movement.magnitude == 0)
			vOffset *= 0.9f;
		head.GetComponent<HeadController> ().variableOffset = vOffset;
    }



}
