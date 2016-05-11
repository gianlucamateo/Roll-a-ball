using UnityEngine;
using System.Collections;

public class LightSwitchController : SwitchScript {

	public LightController light;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public void Switch(){
		light.Switch ();
	}

	public override void Dim (float zeroToOne)
	{
		light.Dim (zeroToOne);
	}
}
