using UnityEngine;
using System.Collections;

public class TubeContainerTriggerBehaviour : MonoBehaviour {
	
	public TubeBehaviour tube;
	public Transform pouringPosition;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider triggerer)
	{
		if(triggerer.name.Contains( "Container" ) )
		{
			ContainerChildBehaviour cont = triggerer.GetComponent<ContainerChildBehaviour>();
			
			if(MainBehaviour.Instance.state == MainState.Filling && !this.tube.isFilled())
			{
				cont.container.setTubeTip(this.transform.position);
				cont.container.setPouringPosition(pouringPosition.position);
				cont.container.setFilledTube(tube);
				
				cont.container.state = ContainerBehaviour.ContainerState.Pouring;
			}
		}
	}
	
	void OnTriggerStay(Collider triggerer)
	{
	}
	
	void OnTriggerExit(Collider triggerer)
	{
		if(triggerer.name.Contains( "Container" ) )
		{			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
