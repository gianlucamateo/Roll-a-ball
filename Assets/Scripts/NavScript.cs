using UnityEngine;
using System.Collections;

public class NavScript : MonoBehaviour {

	public GameObject target;
	public Vector3 tarVec;

	void Start () {
		updateRoute ();
	}
	public void updateRoute(){
		NavMeshAgent agent = GetComponent<NavMeshAgent> ();
		agent.destination = target.transform.position;
		tarVec = target.transform.position;
	}
}
