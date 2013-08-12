using UnityEngine;
using System.Collections;

public class ContainerBehaviour : MonoBehaviour {
	
	public enum ContainerState
	{
		Idle,
		Picked,
		Pouring,
		Filled,
		Returning,
		Returned
	}
	
	public ContainerState state = ContainerState.Idle;
	private ContainerState oldState;
	
	private AudioSource sound;
	private DirectionAligner directioner;
	private PositionAligner positioner;
	
	private Vector3 tubeTip;
	private Vector3 pouringPosition;
	private TubeBehaviour filledTube;
	
	public ObjectDragger objectDragger;
	
	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource>();
		directioner = GetComponent<DirectionAligner>();
		positioner = GetComponent<PositionAligner>();
		
		objectDragger = GetComponent<ObjectDragger>();
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.state != this.oldState)
		{
			if(this.state == ContainerState.Idle)
			{
				directioner.localFrom = Vector3.forward;
				directioner.to = Vector3.up;
			}
			else if(this.state == ContainerState.Picked)
			{
				directioner.localFrom = Vector3.forward;
				directioner.to = Vector3.up;
			}
			else if(this.state == ContainerState.Pouring)
			{
				this.positioner.targetPosition = this.pouringPosition;
				this.positioner.enabled = true;
				this.rigidbody.isKinematic = true;
			}
			else if(this.state == ContainerState.Returned)
			{
				this.objectDragger.Detach();
			}
				
			if(this.state != ContainerState.Pouring)
			{
				this.positioner.enabled = false;
				this.rigidbody.isKinematic = false;
			}
			
			this.oldState = this.state;
		}
		
		if(this.state == ContainerState.Picked)
		{
			objectDragger.Drag();
		}
		
		if(this.state == ContainerState.Pouring)
		{
			directioner.localFrom = Vector3.forward;
			directioner.to = Vector3.Normalize(tubeTip - this.transform.position);
			
			if(!this.sound.isPlaying)
				this.sound.Play();
			
			filledTube.Fill();
			
			if(filledTube.isFilled())
			{
				this.state = ContainerState.Picked;
				this.sound.Stop();
			}
		}
	}
	
	public void setPouringPosition(Vector3 pouringPosition)
	{
		this.pouringPosition = pouringPosition;
	}
	
	public void setTubeTip(Vector3 tubeTip)
	{
		this.tubeTip = tubeTip;
	}
	
	public void setFilledTube(TubeBehaviour tube)
	{
		this.filledTube = tube;
	}
	
	public bool isReturned()
	{
		return this.state == ContainerState.Returned;
	}
}
