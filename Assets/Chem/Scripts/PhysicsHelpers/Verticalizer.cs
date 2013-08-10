using UnityEngine;
using System.Collections;

public class Verticalizer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public float MaxDrag = 20.0f;
	
	// Update is called once per frame
	void Update () {
			float angularDrag = Mathf.Max( MaxDrag * Vector3.Dot(
				this.transform.TransformDirection(Vector3.down), Vector3.down), 0.1f);
			this.rigidbody.angularDrag = angularDrag;
	}
}
