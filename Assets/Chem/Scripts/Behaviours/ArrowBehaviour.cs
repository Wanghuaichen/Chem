using UnityEngine;
using System.Collections;

public class ArrowBehaviour : MonoBehaviour {
	
	public Transform positionContainer;
	
	private PositionAligner positioner;
	
	// Use this for initialization
	void Start () {
		positioner = this.gameObject.AddComponent<PositionAligner>();
	}
	
	// Update is called once per frame
	void Update () {
		MainState mainState = MainBehaviour.Instance.state;
		
		if(mainState == MainState.Filling)
		{
			if(MainBehaviour.Instance.container.state == ContainerBehaviour.ContainerState.Idle)
				this.positioner.targetPosition = this.positionContainer.FindChild("AboveContainer").position;
			else
				this.positioner.targetPosition = this.positionContainer.FindChild("AboveHolder").position;
		}
		else if(mainState == MainState.Pincet)
		{
			if(MainBehaviour.Instance.pincet.state == PincetBehaviour.PincetState.Idle)
				this.positioner.targetPosition = this.positionContainer.FindChild("AbovePincet").position;
			else
			if(MainBehaviour.Instance.pincet.state == PincetBehaviour.PincetState.Used)
				this.positioner.targetPosition = this.positionContainer.FindChild("AboveSamples").position;
			else
			if(MainBehaviour.Instance.pincet.state == PincetBehaviour.PincetState.Picked)
				this.positioner.targetPosition = this.positionContainer.FindChild("AboveHolder").position;
		}
		else if(mainState == MainState.PostDissolution)
		{
			this.positioner.targetPosition = this.positionContainer.FindChild("AboveSamples").position;
		}
		else if(mainState == MainState.Finishing)
		{
			this.positioner.targetPosition = this.positionContainer.FindChild("AbovePincet").position;
		}
	}
}
