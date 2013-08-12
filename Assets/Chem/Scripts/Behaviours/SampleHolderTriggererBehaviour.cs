using UnityEngine;
using System.Collections;

public class SampleHolderTriggererBehaviour : MonoBehaviour {
	
	public SampleBehaviour sample;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.name == sample.name)
		{
			if(sample.isReturning())
				sample.state = SampleBehaviour.SampleState.Returned;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
