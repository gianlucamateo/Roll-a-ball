using UnityEngine;
using System.Collections;

public class NavScript : MonoBehaviour {

	public GameObject target;

	void Start () {
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = target.transform.position; 
	}
}
