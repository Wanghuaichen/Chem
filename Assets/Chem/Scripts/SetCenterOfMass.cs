using UnityEngine;
using System.Collections;

public class SetCenterOfMass : MonoBehaviour {
	
	public Transform CenterOfMass;
	
	private LineRenderer line;
	
	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		this.rigidbody.centerOfMass = CenterOfMass.localPosition;
		line.SetPosition(1, CenterOfMass.position);
	}
}
