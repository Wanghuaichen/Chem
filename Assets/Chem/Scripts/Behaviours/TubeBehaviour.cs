using UnityEngine;
using System.Collections;

public class TubeBehaviour : MonoBehaviour {
	
	
	public enum TubeState
	{
		Idle,
		Filling,
		Filled,
		Dissolving
	}

	public TubeState state = TubeState.Idle;
	private TubeState oldState;
	
	public float level = 0;
	public float fillTime = 6.0f;
	
	public GameObject prefabBubbles;
	public GameObject bubbles;
	
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
				bubbles = Instantiate(prefabBubbles) as GameObject;
				bubbles.transform.position = this.transform.position;
				bubbles.transform.parent = this.transform;
			}
			else if(this.state == TubeState.Idle)
			{
				if(bubbles)
					Destroy(bubbles);
			}
		}
	}
	
	public bool isDissolving()
	{
		return this.state == TubeState.Dissolving;
	}
	
	public bool isFilled()
	{
		return this.state == TubeState.Filled;
	}
	
	public void Fill()
	{
		this.state = TubeState.Filling;
		
		level += Time.deltaTime / fillTime;
		
		if(level >= 1.0f)
		{
			this.state = TubeState.Filled;
		}
	}
	
	public void Dissolve(SampleBehaviour sample)
	{
		this.sample = sample;
		sample.state = SampleBehaviour.SampleState.Dissolving;
		state = TubeState.Dissolving;
	}
}
