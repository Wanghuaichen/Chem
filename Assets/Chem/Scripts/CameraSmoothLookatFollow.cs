using UnityEngine;
using System.Collections;


public class CameraSmoothLookatFollow : MonoBehaviour {
	


	
	public Transform target;
	public bool smooth = true;
	public double damping = 6.0;

	
	// Use this for initialization
	void Start () {
		// Make the rigid body not change rotation
   		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (target) {
		if (smooth)
		{
			// Look at and dampen the rotation
			var rotation = Quaternion.LookRotation(target.position - transform.position);
			
			Vector3 targetPosConstrained = new Vector3(target.position.x / 2.0f, transform.position.y, transform.position.z);
			transform.position = Vector3.Lerp(transform.position, targetPosConstrained , (float)(Time.deltaTime * damping));
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, (float)(Time.deltaTime * damping));
		}
		else
		{
			// Just lookat
		    transform.LookAt(target);
		}
	}
	}
}
