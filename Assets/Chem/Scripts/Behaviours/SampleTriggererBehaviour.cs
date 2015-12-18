using UnityEngine;
using System.Collections;

public class SampleTriggererBehaviour : MonoBehaviour {
	
	public SampleBehaviour sample;
	public float conSpringStiff = 150.0f;
	public float conSpringDamp = 20.0f;
	public float sampleSpringDrag = 20.0f;
	
	private float sampleOldDrag;
	
	private SpringJoint sampleSpring;
	
	// Use this for initialization
	void Start () {
		sampleSpring = sample.gameObject.AddComponent<SpringJoint>();
		sampleSpring.anchor = sample.transform.InverseTransformPoint(this.transform.position);
		sampleSpring.maxDistance = 0.01f;
		
		sampleOldDrag = sample.GetComponent<Rigidbody>().drag;
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "PincetTip")
		{
			if(!sample.isPickable() && MainBehaviour.Instance.pincet.state != PincetBehaviour.PincetState.Picked)
			{
				Debug.Log("Sample " + sample.name + " not pickable.");
				return;
			}
			
			Debug.Log("Picked sample: " + sample.gameObject.name);
			MainBehaviour.Instance.pincet.SetPickedSample(this.sample.GetComponent<SampleBehaviour>());
			
			if(MainBehaviour.Instance.state == MainState.PostDissolution)
			{
				sample.state = SampleBehaviour.SampleState.Returning;
			}
			else if(MainBehaviour.Instance.state == MainState.Pincet)
			{
				sample.state = SampleBehaviour.SampleState.Picked;
			}
			
			this.AttachToPincet(MainBehaviour.Instance.pincet);
		}
	}
	
	void AttachToPincet(PincetBehaviour pincet)
	{
		this.sampleSpring.spring = conSpringStiff;
		sampleSpring.GetComponent<Rigidbody>().drag = this.sampleSpringDrag;

	
		if(!sampleSpring.connectedBody)
		{
			sampleSpring.connectedBody = pincet.pincetTip.transform.GetComponent<Rigidbody>();
			sample.transform.position += pincet.pincetTip.transform.position - this.transform.position;
		}
	}
	
	public void DetachFromPincet()
	{
		this.sampleSpring.connectedBody = null;
		this.sampleSpring.spring = 0.0f;
		this.sampleSpring.GetComponent<Rigidbody>().drag = this.sampleOldDrag;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
