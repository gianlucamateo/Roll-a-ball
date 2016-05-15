using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour {

	public float thrust;
	public GameObject head;
	public Rigidbody rb;
	public NavScript nav;
	public Vector3[] navPoints;
	public Vector3 controlOverride = Vector3.zero;
	public bool stopping = false,slowing = false;
	public bool navigating = false, newPoints = false;
	public int navIndex = 0;
	public Vector3 vOffset, movement;
	public int stopCount = 0;
	public float distance;
	public float forward;
	public float currentSpeed,targetSpeed;
	public NavMeshPathStatus stat;
	private int navCount = 0;
	public bool BB8enabled;

	public delegate void delegateFunc();

	delegateFunc callOnNavFinished;

	public void Navigate(delegateFunc func = null){
		if (!BB8enabled) {
			if(func != null)
				func ();
			return;
		}
		this.head.GetComponent<HeadController>().playBeep ();
		this.refreshRoute ();
		stopping = true;
		nav.GetComponent<NavMeshAgent> ().ResetPath ();
		nav.updateRoute ();
		stat = nav.GetComponent<NavMeshAgent> ().pathStatus;
		navigating = true;
		callOnNavFinished = func;
		navIndex = 0;
		navCount = 0;
	}

	private void refreshRoute()
	{
		newPoints = true;
	}

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}
    //before physics
    void FixedUpdate()
    {
		if (navCount++ < 20) {
			this.refreshRoute();
		}
		if (Input.GetKey (KeyCode.Space)) {
			UnityEngine.VR.InputTracking.Recenter ();
		}
		if (Input.GetKey (KeyCode.N)) {
			this.Navigate ();
		}
		if (Input.GetKey (KeyCode.P)) {
			stopping = true;
		}
		if (stopping) {
			stop (true);
		}
		if (slowing) {
			stop (false);
		}
		if (navigating && newPoints) {
			this.navPoints = nav.GetComponent<NavMeshAgent>().path.corners;
			newPoints = false;
		}
		if (navigating && navPoints.Length>0 && !(stopping || slowing)) {
			distance = (navPoints [navIndex] - transform.position).magnitude;
			targetSpeed = distance;
			currentSpeed = rb.velocity.magnitude;

			controlOverride = navPoints [navIndex] - transform.position;

			if (distance < 0.5) {
				slowing = true;

				if (navPoints.Length > navIndex + 1) {
					navIndex++;
				} else {
					navigating = false;
					stopping = true;
					if (callOnNavFinished != null) {
						callOnNavFinished ();
					}
				}
			}
		}		
		if (controlOverride.magnitude > 1) {
			controlOverride.Normalize ();
		}
		float moveHorizontal = controlOverride.x == 0 ? Input.GetAxis ("Horizontal") : controlOverride.x;
		float moveVertical = controlOverride.z == 0 ? Input.GetAxis ("Vertical") : controlOverride.z;
		movement = new Vector3 (moveHorizontal, 0, moveVertical);
		if (movement.magnitude > 0) {
			rb.angularDrag = 0.05f;
		}
		vOffset = head.GetComponent<HeadController> ().variableOffset;
		vOffset += movement / 10;
		vOffset = movement;
		vOffset.x = Mathf.Round (100f * vOffset.x) / 100f;
		vOffset.y = Mathf.Round (100f * vOffset.y) / 100f;
		vOffset.z = Mathf.Round (100f * vOffset.z) / 100f;

		if (movement.magnitude == 0) {
			vOffset *= 0.9f;
			if (vOffset.magnitude < 0.01f) {
				vOffset = Vector3.zero;
			}
		} else {
			rb.AddForce (vOffset * thrust);
		}
		head.GetComponent<HeadController> ().variableOffset = rb.velocity/4f;
		
    }

	void stop(bool fullstop)
	{		
		Vector3 velocity = rb.velocity;
		controlOverride = -velocity;
		//rb.velocity *= 0.9f;
		if (fullstop) {
			if (rb.velocity.magnitude < 0.1) {
				stopCount++;
				if (stopCount > 50) {
					vOffset = Vector3.zero;
					rb.velocity = Vector3.zero;
					movement = Vector3.zero;
					rb.angularVelocity = Vector3.zero;
					controlOverride = Vector3.zero;
					head.GetComponent<HeadController> ().variableOffset = Vector3.zero;
					stopCount = 0;
					stopping = false;
					rb.angularDrag = float.MaxValue;

				}

			}
		} else {
			if (Mathf.Abs(Vector3.Dot(rb.velocity,Vector3.Cross(navPoints[navIndex]-transform.position,Vector3.up))) < 0.2f&&vOffset.magnitude<0.3f) { //.2 .3
				slowing = false;
			}
		}
			
	}



}
