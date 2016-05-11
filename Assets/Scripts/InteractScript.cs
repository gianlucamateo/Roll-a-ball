using UnityEngine;
using System.Collections;

public class InteractScript : MonoBehaviour {

	public Camera head;
	public Vector3 direction;
	public GameObject thumbTip, indexFingerTip;
	public GameObject hitObject;
	public float fingerDist;
	public GameObject[] interactables;
	public GameObject navTarget;
	public TextMesh text;
	public int sleepCounter=0;
	public int counter = 0;
	public volatile bool switchCommand = false;
	public volatile bool dimCommand = false;
	public volatile bool dimming = false;
	public LightSwitchController dimmingLight;
	public PlayerController Player;
	private int timeout = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if (sleepCounter-- > 0) {
			return;
		}
		if (counter++ % timeout != 0) {
			return;
		}

		Vector3 fwd = transform.position - head.transform.position;
		direction = fwd;
		fingerDist = (thumbTip.transform.position - indexFingerTip.transform.position).magnitude;
		if (dimming) {
			text.text = "dimming "+fingerDist;
			dimmingLight.Dim (fingerDist * 10);
		}
		RaycastHit hit;			
		if (Physics.Raycast (transform.position, fwd,out hit)) {
			hitObject = hit.transform.gameObject;
			foreach (GameObject s in interactables) {
				if (hitObject == s && switchCommand) {
					SwitchScript sScript = s.GetComponent<SwitchScript> ();

					Vector3 pos = hitObject.transform.position;
					pos.y = 0;
					navTarget.transform.position = pos + sScript.offset;

					//sScript.Switch ();
					Player.Navigate (sScript.Switch);
					switchCommand = false;
					sleepCounter = 50;
					print ("switched");
				}
				if (hitObject == s && dimCommand) {
					LightSwitchController lController = s.GetComponent<LightSwitchController> ();
					SwitchScript sScript = s.GetComponent<SwitchScript> ();
					if (lController == null) {
						text.text = "dim failed";
						return;
					}
					Vector3 pos = hitObject.transform.position;
					pos.y = 0;
					navTarget.transform.position = pos + sScript.offset;
					Player.Navigate (this.startDimming);

					dimCommand = false;
					dimmingLight = lController;
				}
			}				
		}
			
	}
	public void startDimming(){
		dimming = true;
	}
	public void Switch(){
		switchCommand = true;
	}

	public void Dim(){
		dimCommand = true;
	}

	public void EndDim(){
		dimCommand = false;
		dimming = false;
	}
}
