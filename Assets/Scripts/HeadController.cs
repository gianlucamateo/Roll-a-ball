using UnityEngine;
using System.Collections;
using System.Timers;

public class HeadController : MonoBehaviour {

	public float angle;
	public Camera Camera;
	public GameObject body;
	public Vector3 baseOffset, variableOffset, direction;
	public float x = 0, headingSign = 0;
	public float headingAngle = 0;
	private float[] beginnings = {0,1.5f,5.3f,7.3f,9.5f,29.2f,30.6f};
	private float[] durations = {1.5f,1.5f,1.5f,2,2.2f,1.2f,1.4f};
	private volatile bool stopSound = false;


	// Use this for initialization
	void Start () {
		variableOffset = Vector3.zero;	
	}

	public void playBeep() {		
		stopSound = false;
		AudioSource source = this.GetComponent<AudioSource> ();
		int index = Random.Range(0,7);
		source.time = beginnings[index];
		source.Play ();
		Timer t = new Timer (1000 * durations[index]);
		t.Elapsed += (object sender, ElapsedEventArgs e) => {
			stopSound = true;
			t.Dispose();
		};
		t.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A)) {
			playBeep ();
		}
		if (stopSound) {			
			AudioSource source = this.GetComponent<AudioSource> ();
			source.Stop ();
		}
		PlayerController bodyController = body.GetComponent<PlayerController>();
		direction = bodyController.rb.velocity;

		float directionAngle = Vector3.Angle (Vector3.forward, direction);
		Vector3 dirToCam = -(transform.position - Camera.transform.position).normalized;
		dirToCam.y = 0;
		float playerAngle = Vector3.Angle (Vector3.forward, dirToCam); 



		x = (1 / (direction.magnitude + 1));

		Vector3 meanDirection = direction.normalized * (1 - x) + dirToCam.normalized * x;

		headingAngle = Vector3.Angle (Vector3.forward, meanDirection); 

		headingSign = Vector3.Cross (Vector3.forward, meanDirection).y;
		if (headingSign < 0)
			headingSign = -1;
		else
			headingSign = 1;
		if (variableOffset.magnitude > 1)
			variableOffset.Normalize ();
		Vector3 totaloffset = baseOffset + variableOffset;
		totaloffset.Normalize ();
		angle = Vector3.Angle (Vector3.up, totaloffset);
		Vector3 rotAxis = Vector3.Cross (Vector3.up, totaloffset);
		float sign = rotAxis.z;
		if (sign > 0)
			sign = -1;
		else
			sign = 1;
		totaloffset *= 0.3f;
		transform.position = body.transform.position + totaloffset;
		transform.rotation = Quaternion.AngleAxis (angle, rotAxis) * Quaternion.Euler (0, headingAngle * headingSign, 0);//Quaternion.Euler (0, 90, 0) * Quaternion.Euler(angle*sign,0,0);

	}
}
