using UnityEngine;
using System.Collections;

public class InteractScript : MonoBehaviour {

	public Camera head;
	public Vector3 direction;
	public GameObject thumbTip, indexFingerTip;
	public GameObject hitObject;
	public float fingerDist;
	public GameObject[] interactables;
	public int sleepCounter=0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (sleepCounter-- > 0) {
			return;
		}
		Vector3 fwd = transform.position - head.transform.position;
		direction = fwd;
		fingerDist = (thumbTip.transform.position - indexFingerTip.transform.position).magnitude;
		RaycastHit hit;			
		if (Physics.Raycast (transform.position, fwd,out hit)) {
			hitObject = hit.transform.gameObject;
			print (hitObject);
			foreach (GameObject s in interactables) {
				if (hitObject == s && fingerDist < 0.03f) {
					SwitchScript sScript = s.GetComponent<SwitchScript> ();
					sScript.Switch ();
					sleepCounter = 50;
					print ("switched");
				}
			}
				
		}
			
	}
}
