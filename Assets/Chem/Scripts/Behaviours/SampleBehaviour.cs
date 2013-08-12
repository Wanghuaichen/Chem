using UnityEngine;
using System.Collections;

public class SampleBehaviour : MonoBehaviour {

	public enum SampleState
	{
		Idle,
		Picked,
		Dissolving,
		Dissolved,
		Returning,
		Returned
	}
	
	private DirectionAligner aligner;
	private PositionAligner positioner;
	
	private SampleState oldState;
	public SampleState state = SampleState.Idle;
	
	public SampleTriggererBehaviour sampleTip;

	// Use this for initialization
	void Start () {
		aligner = GetComponent<DirectionAligner>();
		positioner = GetComponent<PositionAligner>();
		positioner.targetPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.state != oldState)
		{
			Debug.Log(this.name + " state: " + this.state);
			
			if(this.state == SampleState.Dissolving)
			{
				this.aligner.enabled = false;
			}
			else if(this.state == SampleState.Picked || this.state == SampleState.Returning)
			{
				this.aligner.localFrom = Vector3.up;
				this.aligner.to = Vector3.up;
				this.aligner.enabled = true;
			}
			else if(this.state == SampleState.Returned)
			{
				this.aligner.localFrom = Vector3.up;
				this.aligner.to = Vector3.back;
				this.aligner.enabled = true;
				
				this.positioner.enabled = true;
				
				MainBehaviour.Instance.pincet.state = PincetBehaviour.PincetState.Dropped;
			}
			
			this.oldState = this.state;
		}
		
	}
	
	public bool isReturned()
	{
		return this.state == SampleState.Returned;
	}
	
	public bool isReturning()
	{
		return this.state == SampleState.Returning;
	}
	
	public bool isPickable()
	{
		return this.state!= SampleState.Picked && this.state != SampleState.Dissolving && this.state != SampleState.Returned;
	}
}
