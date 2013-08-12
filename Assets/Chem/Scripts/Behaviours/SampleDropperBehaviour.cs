using UnityEngine;
using System.Collections;

[RequireComponent (typeof (BoxCollider))]
public class SampleDropperBehaviour : MonoBehaviour {
	
	public TubeBehaviour tube;
	
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.tag.Equals("Sample"))
		{	
			SampleBehaviour sampleBhv = collider.gameObject.GetComponent<SampleBehaviour>();

			if(sampleBhv.state == SampleBehaviour.SampleState.Picked)
			{
			
				Debug.Log("Dropping sample into : " + this.name);
			
				MainBehaviour.Instance.pincet.state = PincetBehaviour.PincetState.Dropped;
				tube.Dissolve(sampleBhv);
			}
		}
	}
}
