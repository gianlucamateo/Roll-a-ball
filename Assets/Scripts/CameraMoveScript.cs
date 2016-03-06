using UnityEngine;
using System.Collections;

public class CameraMoveScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float moveHorizontal = Input.GetAxis ("rightX");
		float moveVertical = Input.GetAxis ("rightY");
		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);	
		transform.position += movement/10;
	}
}
