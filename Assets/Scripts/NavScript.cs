using UnityEngine;
using System.Collections;

public class NavScript : MonoBehaviour {



	void Start () {
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		agent.destination = new Vector3(5,0,0); 
	}
}
