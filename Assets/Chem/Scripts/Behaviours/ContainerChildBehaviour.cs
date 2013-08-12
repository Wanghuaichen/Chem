using UnityEngine;
using System.Collections;

public class ContainerChildBehaviour : MonoBehaviour {
	
	public ContainerBehaviour container;
	// Use this for initialization
	void Start () {
		this.container = this.transform.parent.gameObject.GetComponent<ContainerBehaviour>();
	}
	
	
	// Update is called once per frame
	void Update () {
		if(MainBehaviour.Instance.state == MainState.Filling && this.container.state == ContainerBehaviour.ContainerState.Idle)
		{
			if(Input.GetMouseButton(0))
			{
				Ray ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition);
				RaycastHit rayHit;
				
				if(this.collider.Raycast(ray, out rayHit, 2000.0f))
				{
					this.container.state = ContainerBehaviour.ContainerState.Picked;
				}
			}
		}
	}
}
