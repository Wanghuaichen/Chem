using UnityEngine;
using System.Collections;

public class FollowMouseOnPlane : MonoBehaviour {
	
	public Transform origin;
	
	private LineRenderer line;
	// Use this for initialization
	void Start () {
		line = (LineRenderer)gameObject.AddComponent(typeof(LineRenderer));
        line.material = new Material (Shader.Find("Particles/Additive"));
		line.SetVertexCount(2);
	}
	
	// Update is called once per frame
	void Update () {
		Camera camera = Camera.mainCamera;
		Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
	
		Plane plane = new Plane(origin.forward, origin.position);
		
		
		float t = 0.0f;
		if(!plane.Raycast(mouseRay, out t))
			return;
		
		Vector3 mousePoint = mouseRay.GetPoint(t);
		
		line.SetPosition(0, new Vector3());
		line.SetPosition(1, mousePoint);
		this.transform.position -= (this.transform.position - mousePoint) * (float)Time.deltaTime;
	}
}