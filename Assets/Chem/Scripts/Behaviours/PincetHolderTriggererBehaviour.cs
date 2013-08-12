using UnityEngine;
using System.Collections;

public class PincetHolderTriggererBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.name == MainBehaviour.Instance.pincet.name)
		{
			if(MainBehaviour.Instance.state == MainState.Finishing)
			{
				MainBehaviour.Instance.pincet.state = PincetBehaviour.PincetState.Returned;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
