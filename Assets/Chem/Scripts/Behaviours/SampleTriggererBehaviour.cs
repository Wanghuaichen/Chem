using UnityEngine;
using System.Collections;

public class SampleTriggererBehaviour : MonoBehaviour {
	
	public SampleBehaviour sample;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		Debug.Log(collider.gameObject.tag);
		if(collider.gameObject.tag == "PincetTip")
		{
			if(!sample.isPickable())
			{
				Debug.Log("Cannot be picked. Currently dissolving: " + sample.name);
				return;
			}
			
			Debug.Log("Picked sample: " + sample.name);
			MainBehaviour.Instance.pincet.SetPickedSample(this.sample.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
