using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraPickingLookAt : MonoBehaviour {
	
	private Color oldColor;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if(!Input.GetMouseButtonDown(0))
			return;
		// We need to actually hit an object
		RaycastHit	 hit = new RaycastHit();
		
//		Component[] components = GetComponents<Collider>();
		
		Camera mainCamera = Camera.mainCamera;
		
//		foreach(Component comp in components)
		{
			if (!Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition), out hit, 100))
				return;
			// We need to hit a rigidbody that is not kinematic
			if (!hit.rigidbody || hit.rigidbody.isKinematic)
				return;
			
			CameraSmoothLookatFollow sla = GetComponent<CameraSmoothLookatFollow>();
			
			if(sla)
				sla.target = hit.rigidbody.transform;
		}
	}
	
	void OnMouseEnter()
	{
		
	}
}
