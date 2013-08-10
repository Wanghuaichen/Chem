using UnityEngine;
using System.Collections;

public class AttachObjectsAtPoint : MonoBehaviour {

	public float spring = 50.0f;
	public float damper = 5.0f;
	public float drag = 10.0f;
	public float angularDrag = 5.0f;
	public float distance = 2.0f;
	
	public Transform connectedBodyTransform;
	public Transform thisObjectConnectionTransform;
	
	private SpringJoint springJoint ;
	
	// Use this for initialization
	void Start () {
	}
	
	public void Attach(Transform connectedBodyTransform)
	{
			
		this.connectedBodyTransform = connectedBodyTransform;
		
		if (!springJoint)
		{
			springJoint = (SpringJoint)this.gameObject.AddComponent ("SpringJoint");
		}
		
		//springJoint.transform.position = this.thisObjectConnectionTransform.position;
		
		///springJoint.anchor = Vector3.zero;

		springJoint.anchor = springJoint.transform.InverseTransformPoint(this.thisObjectConnectionTransform.transform.position);

		springJoint.spring = spring;
		springJoint.damper = damper;
		springJoint.maxDistance = distance;
		springJoint.connectedBody = this.connectedBodyTransform.rigidbody;
	}
	
	public void Detach()
	{
		Component.Destroy(this.springJoint);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Destroy()
	{
	}
}
