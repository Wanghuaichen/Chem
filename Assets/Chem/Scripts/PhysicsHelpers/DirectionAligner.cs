using UnityEngine;
using System.Collections;

public class DirectionAligner : MonoBehaviour {
	
	public Vector3 localFrom;
	public Vector3 to;
	public float maxDamping = 20.0f;
	public float speed = 1.0f;
	
	public bool hack = false;
	
	private Quaternion orientation;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 globalFrom = this.transform.TransformDirection(localFrom);
		
		this.orientation = Quaternion.FromToRotation(localFrom, to);
		
		if(hack)
		{
			float dot = Vector3.Dot(globalFrom.normalized, to.normalized);
			float angularDrag = Mathf.Max(dot * maxDamping, 0.1f);
			this.rigidbody.angularDrag = angularDrag;
		}
		
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, this.orientation, Time.deltaTime * speed);
		
	}
}
