using UnityEngine;
using System.Collections;

public class SampleBehaviour : MonoBehaviour {

	public enum SampleState
	{
		Idle,
		Dissolving,
		Dissolved
	}
	
	private SampleState oldState;
	public SampleState state = SampleState.Idle;
	
	public SampleTriggererBehaviour sampleTip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(this.state != oldState)
		{
			this.oldState = this.state;
			
			if(this.state == SampleState.Dissolving)
			{
				
			}
		}
	}
	
	public bool isPickable()
	{
		return MainBehaviour.Instance.state != MainState.Dissolution && this.state != SampleState.Dissolving;
	}
}
