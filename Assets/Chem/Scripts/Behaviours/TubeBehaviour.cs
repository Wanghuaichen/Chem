using UnityEngine;
using System.Collections;

public class TubeBehaviour : MonoBehaviour {
	
	
	public enum TubeState
	{
		Idle,
		Dissolving
	}

	public TubeState state = TubeState.Idle;
	private TubeState oldState;
	
	public GameObject prefabBubbles;
	public SampleBehaviour sample;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(this.oldState != this.state)
		{
			this.oldState = this.state;
			
			if(this.state == TubeState.Dissolving)
			{
				var bubbles = Instantiate(prefabBubbles) as GameObject;
				bubbles.transform.position = this.transform.position;
				bubbles.transform.parent = this.transform;
			}
		}
	}
	
	public bool isDissolving()
	{
		return this.state == TubeState.Dissolving;
	}
	
	public void Dissolve(SampleBehaviour sample)
	{
		this.sample = sample;
		state = TubeState.Dissolving;
	}
}
