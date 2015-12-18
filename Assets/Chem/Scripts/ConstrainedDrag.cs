using UnityEngine;
using System.Collections;

public class ConstrainedDrag : MonoBehaviour {

	public float spring = 50.0f;
	public float damper = 5.0f;
	public float drag = 10.0f;
	public float angularDrag = 5.0f;
	public float distance = 0.2f;
	public bool attachToCenterOfMass = false;
	public Transform constraintPlaneObject ;
	
	private SpringJoint springJoint ;
	private GameObject go;
	private LineRenderer line;
	
	// Use this for initialization
	void Start () {
		line = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		// Make sure the user pressed the mouse down
		if (!Input.GetMouseButtonDown (0))
			return;
	
		Camera mainCamera = FindCamera();
			
		// We need to actually hit an object
		RaycastHit hit;
		
		if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100 ))
			return;
		// We need to hit a rigidbody that is not kinematic
		if (!hit.rigidbody || hit.rigidbody.isKinematic)
			return;
		
		if (!springJoint)
		{
			go = new GameObject("Rigidbody dragger");
			Rigidbody body = (Rigidbody)go.AddComponent <Rigidbody>();
			springJoint = (SpringJoint)go.AddComponent <SpringJoint>();
			body.isKinematic = true;
		}
		
		//Plane plane = new Plane(Vector3.back, constraintPlaneObject.transform.position);
		//Ray ray = mainCamera.ScreenPointToRay (Input.mousePosition);
		//float constraintPlaneDistance = 0.0f;
		//plane.Raycast(ray, out constraintPlaneDistance);
		
		springJoint.transform.position = hit.point;
		
		if (attachToCenterOfMass)
		{
			springJoint.anchor = Vector3.zero;
		}
		else
		{
			var anchor = transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
			anchor = springJoint.transform.InverseTransformPoint(anchor);
			springJoint.anchor = anchor;
		}
		
		springJoint.spring = spring;
		springJoint.damper = damper;
		springJoint.maxDistance = distance;
		springJoint.connectedBody = hit.rigidbody;
		
		if(!springJoint.connectedBody.tag.Contains("Sample"))
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
			line.SetPosition(0, springJoint.transform.TransformPoint(springJoint.anchor));
			line.SetPosition(1, springJoint.transform.position);

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
		if (GetComponent<Camera>())
			return GetComponent<Camera>();
		else
			return Camera.main;
	}
}
