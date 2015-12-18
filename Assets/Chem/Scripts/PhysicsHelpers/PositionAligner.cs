using UnityEngine;
using System.Collections;

public class PositionAligner : MonoBehaviour {

	public Vector3 targetPosition;
	public float speed = 1.0f;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Rigidbody>().isKinematic = true;
		this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, speed * Time.deltaTime);
	}
}
