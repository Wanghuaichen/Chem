using UnityEngine;
using System.Collections;

public class ContainerReturnTriggererBehaviour : MonoBehaviour {
	
	public ContainerBehaviour container;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.name.Contains( "Container" ) )
		{
			ContainerChildBehaviour cont = collider.GetComponent<ContainerChildBehaviour>();
			{
				if(MainBehaviour.Instance.state == MainState.Filled)
				{
					cont.container.state = ContainerBehaviour.ContainerState.Returned;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
