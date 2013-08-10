using UnityEngine;
using System.Collections;

public class ConstrainedDragFreezeRotation : MonoBehaviour {

	public float spring = 50.0f;
	public float damper = 5.0f;
	public float drag = 10.0f;
	public float angularDrag = 5.0f;
	public float distance = 0.2f;
	public bool attachToCenterOfMass = false;
	public Transform constraintPlaneObject ;
	
	private SpringJoint springJoint ;
	private GameObject go;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		// Make sure the user pressed the mouse down
		if (!Input.GetMouseButtonDown (0))
			return;
	
		Camera mainCamera = FindCamera();
			
		// We need to actually hit an object
		RaycastHit hit;
		
		if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100))
			return;
		// We need to hit a rigidbody that is not kinematic
		if (!hit.rigidbody || hit.rigidbody.isKinematic)
			return;
		
		if (!springJoint)
		{
			go = new GameObject("Rigidbody dragger");
			Rigidbody body = (Rigidbody)go.AddComponent ("Rigidbody");
			springJoint = (SpringJoint)go.AddComponent ("SpringJoint");
			body.isKinematic = true;
		}
		
		Plane plane = new Plane(Vector3.back, constraintPlaneObject.transform.position);
		Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
		float constraintPlaneDistance = 0.0f;
		plane.Raycast(ray, out constraintPlaneDistance);
		
		springJoint.transform.position = ray.GetPoint(constraintPlaneDistance);
		
		if (attachToCenterOfMass)
		{
			var anchor = transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
			anchor = springJoint.transform.InverseTransformPoint(anchor);
			springJoint.anchor = anchor;
		}
		else
		{
			springJoint.anchor = Vector3.zero;
		}
		
		springJoint.spring = spring;
		springJoint.damper = damper;
		springJoint.maxDistance = distance;
		springJoint.connectedBody = hit.rigidbody;
		springJoint.connectedBody.freezeRotation = true;
		
		StartCoroutine ("DragObject", hit.distance);
	}

	IEnumerator DragObject (float distance)
	{
		var oldDrag = springJoint.connectedBody.drag;
		var oldAngularDrag = springJoint.connectedBody.angularDrag;
		springJoint.connectedBody.drag = drag;
		springJoint.connectedBody.angularDrag = angularDrag;
		var mainCamera = FindCamera();
		while (Input.GetMouseButton (0))
		{
			Plane plane = new Plane(Vector3.back, constraintPlaneObject.transform.position);
			Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
			float constraintPlaneDistance = 0.0f;
			plane.Raycast(ray, out constraintPlaneDistance);
			
			springJoint.transform.position = ray.GetPoint(constraintPlaneDistance);
			yield return null;
		}
		if (springJoint.connectedBody)
		{
			springJoint.connectedBody.drag = oldDrag;
			springJoint.connectedBody.angularDrag = oldAngularDrag;
			springJoint.connectedBody.freezeRotation = false;
			//springJoint.connectedBody = null;
			Destroy(go);
		}
	}
	
	Camera FindCamera ()
	{
		if (camera)
			return camera;
		else
			return Camera.mainCamera;
	}
}
