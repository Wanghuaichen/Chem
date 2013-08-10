using UnityEngine;
using System.Collections;

public class PincetBehaviour : MonoBehaviour {
	
	public enum PincetState
	{
		Idle,
		Used,
		Dropped,
		Picked
	};
	
	private AttachObjectsAtPoint attachScript;
	
	public PincetState state;
	private PincetState oldState;
	
	Transform draggedTransform;
	
	public Transform attachmentPoint;
	
	Transform initial;
	Quaternion 	toOrientation,
				bent;
	
	
	private SpringJoint dragSpring;
	private GameObject dragObject;
	
	// Use this for initialization
	void Start () {
		initial = this.transform;
		this.bent = Quaternion.AngleAxis(90.0f, new Vector3(1.0f, 0.0f, 0.0f)) * this.initial.rotation;
		
		dragObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
		Rigidbody dragRigidBody = dragObject.AddComponent<Rigidbody>();
		dragRigidBody.isKinematic = true;
		
		dragObject.transform.position = this.transform.position;
		
		dragSpring = this.gameObject.AddComponent<SpringJoint>();
		dragSpring.spring = 100.0f;
		dragSpring.damper = 10.0f;
		dragSpring.anchor = Vector3.zero;
		dragSpring.connectedBody = dragObject.rigidbody;
	}
	
	public void SetPickedSample(Transform sampleAttacher)
	{
			this.attachScript = sampleAttacher.GetComponent<AttachObjectsAtPoint>();
			this.state = PincetState.Picked;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.state != this.oldState)
		{
			
			this.oldState = this.state;
			
			if(this.state.Equals(PincetState.Idle))
			{
				this.toOrientation = this.initial.rotation;
				
			}
			else if(state.Equals(PincetState.Used))
			{
				this.toOrientation = bent;
				
			}
			else if(state.Equals(PincetState.Picked)){
					this.toOrientation = bent;
					attachScript.Attach(this.transform);
			}
			else
			{
				this.toOrientation = this.transform.rotation;
				
				if(attachScript)
					attachScript.Detach();
				
				this.state = PincetState.Used;
			}
		}
		
		if(this.state.Equals(PincetState.Idle))
		{
			if(Input.GetMouseButton(0))
			{
				RaycastHit rayInfo;
				Ray ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
				
				Debug.Log(ray.direction);
				
				if(this.collider.Raycast(ray, out rayInfo, 2000.0f))
				{
					this.state = PincetState.Used;
				}
			}
		}
		
		if(!state.Equals(PincetState.Idle))
		{
			Plane plane = new Plane(new Vector3(0.0f, 0.0f, -1.0f), 0.0f);
			Ray ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
			
			float t ;
			
			if(plane.Raycast(ray, out t))
			{
				Vector3 point = ray.GetPoint(t);
				this.dragObject.transform.position =  point;
			}
		}
		
		if(this.toOrientation != this.transform.rotation)
			this.transform.rotation = Quaternion.Slerp(this.transform.rotation, toOrientation, Time.deltaTime);
	}
}
