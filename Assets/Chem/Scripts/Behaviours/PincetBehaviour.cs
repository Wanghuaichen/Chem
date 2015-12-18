using UnityEngine;
using System.Collections;

public class PincetBehaviour : MonoBehaviour {
	
	public enum PincetState
	{
		Idle,
		Used,
		Dropped,
		Picked,
		Returned
	};
	
	public SampleBehaviour sample;
	
	public PincetState state;
	private PincetState oldState;
	
	private DirectionAligner verticalizer;
	private PositionAligner positioner;
	
	ObjectDragger dragObject;
	public PincetTipBehaviour pincetTip;
	
	// Use this for initialization
	void Start () {
		verticalizer = GetComponent<DirectionAligner>();
		
		dragObject = GetComponent<ObjectDragger>();
		dragObject.attachmentPoint = this.pincetTip.transform;
		
		positioner = GetComponent<PositionAligner>();
		positioner.targetPosition = this.transform.position;
	}
	
	public void SetPickedSample(SampleBehaviour sample)
	{
		this.sample = sample;
		this.state = PincetState.Picked;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.state != this.oldState)
		{
			Debug.Log(this.name + " state changed: " + this.state);
			
			if(this.state.Equals(PincetState.Idle))
			{
				
			}
			else if(state.Equals(PincetState.Used))
			{
				
			}
			else if(state.Equals(PincetState.Picked))
			{
				
			}
			else if(state == PincetState.Dropped)
			{
				this.sample.sampleTip.DetachFromPincet();
			}
			else if(state == PincetState.Returned)
			{
				Debug.Log ("Doing the returned bit");
				
				this.verticalizer.localFrom = Vector3.up;
				this.verticalizer.to = Vector3.forward;
				
				this.positioner.enabled = true;
			}
			else
			{
				this.state = PincetState.Used;
			}
			
			this.oldState = this.state;
		}
		
		if(this.state.Equals(PincetState.Idle))
		{
			if(Input.GetMouseButton(0) && MainBehaviour.Instance.state == MainState.Pincet)
			{
				RaycastHit rayInfo;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				if(this.GetComponent<Collider>().Raycast(ray, out rayInfo, 2000.0f))
				{
					this.state = PincetState.Used;
				}
			}
			
			this.verticalizer.localFrom = Vector3.up;
			this.verticalizer.to = Vector3.forward;
		}
		
		if(this.state == PincetState.Returned)
		{

		}
		
		if(state != PincetState.Idle && state != PincetState.Returned)
		{
			this.dragObject.Drag();
		}
		
		if(this.state == PincetState.Used)
		{
			this.verticalizer.localFrom = Vector3.up;
			this.verticalizer.to = Vector3.down;
			
		}
	}
	
	public bool isReturned()
	{
		return this.state == PincetState.Returned;
	}
}
