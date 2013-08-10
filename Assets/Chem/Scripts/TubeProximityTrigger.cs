using UnityEngine;
using System.Collections;

public class TubeProximityTrigger : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider triggerer)
	{
		if(triggerer.name.Contains( "Container" ) )
		{
			GameObject cont = triggerer.gameObject.transform.parent.gameObject;

			cont.GetComponent<ContBender>().Bending = true;
		}
	}
	
	void OnTriggerStay(Collider triggerer)
	{
		if(triggerer.name.Contains( "Container" ) )
		{
			GameObject cont = triggerer.gameObject.transform.parent.gameObject;
			
			cont.GetComponent<ContBender>().Bending = true;
		}
	}
	
	void OnTriggerExit(Collider triggerer)
	{
		if(triggerer.name.Contains( "Container" ) )
		{
			GameObject cont = triggerer.gameObject.transform.parent.gameObject;
			
			cont.GetComponent<ContBender>().Bending = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
