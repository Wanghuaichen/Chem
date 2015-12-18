using UnityEngine;
using System.Collections;

public class ObjectDragger : MonoBehaviour {

		
	public Transform attachmentPoint;
	
	public float 	springStiffness = 100.0f,
					springDamper = 20.0f;
	
	private SpringJoint dragSpring;
	private GameObject dragObject;
	
	Plane plane = new Plane(new Vector3(0.0f, 0.0f, -1.0f), 0.0f);
	
	// Use this for initialization
	void Start () {
		
		dragObject = new GameObject("Dragger Object");
		Rigidbody dragRigidBody = dragObject.AddComponent<Rigidbody>();
		dragRigidBody.isKinematic = true;
		
		dragObject.transform.position = this.attachmentPoint.position;
		
		dragSpring = this.gameObject.AddComponent<SpringJoint>();
		dragSpring.spring = springStiffness;
		dragSpring.damper = springDamper;
		dragSpring.anchor = this.transform.InverseTransformPoint(this.attachmentPoint.position);
		dragSpring.connectedBody = dragObject.GetComponent<Rigidbody>();
			
	}
	
	public void Drag()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		float t;
		if(plane.Raycast(ray, out t))
		{
			this.dragObject.transform.position =  ray.GetPoint(t);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Detach()
	{
		Debug.Log("Destroying objectDragger of " + this.name);
		Destroy(this.dragObject);
		Destroy(this.dragSpring);
	}
}

