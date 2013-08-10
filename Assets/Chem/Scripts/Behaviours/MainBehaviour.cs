using UnityEngine;
using System.Collections;

public enum MainState
{
	StartAnimation,
	PincetStage,
	Dissolution,
	PostDissolution
}

public class MainBehaviour : MonoBehaviour {
	
	    // s_Instance is used to cache the instance found in the scene so we don't have to look it up every time.
    private static MainBehaviour s_Instance = null;
 
    // This defines a static instance property that attempts to find the manager object in the scene and
    // returns it to the caller.
    public static MainBehaviour Instance {
        get {
            if (s_Instance == null) {
                // This is where the magic happens.
                //  FindObjectOfType(...) returns the first MainBehaviour object in the scene.
                s_Instance =  FindObjectOfType(typeof (MainBehaviour)) as MainBehaviour;
            }
 
            // If it is still null, create a new instance
            if (s_Instance == null) {
                GameObject obj = new GameObject("MainBehaviour");
                s_Instance = obj.AddComponent(typeof (MainBehaviour)) as MainBehaviour;
                Debug.Log ("Could not locate an MainBehaviour object. \n MainBehaviour was Generated Automaticly.");
            }
 
            return s_Instance;
        }
    }
	
	public MainState state = MainState.StartAnimation;
	public PincetBehaviour pincet;
	public TubeBehaviour firstTube, secondTube;
	
	void Update()
	{
		if(this.state == MainState.StartAnimation)
		{
			this.animation.Play("CameraFlyIn", AnimationPlayMode.Stop);
		}
		else if(this.state == MainState.Dissolution)
		{
			this.animation.Play("TubeCloseUp", AnimationPlayMode.Stop);
		}
		
		if(this.firstTube.isDissolving() && this.secondTube.isDissolving() && this.state != MainState.PostDissolution)
		{
			this.state = MainState.Dissolution;
			this.pincet.state = PincetBehaviour.PincetState.Idle;
			this.firstTube.sample.state = SampleBehaviour.SampleState.Dissolved;
			this.secondTube.sample.state = SampleBehaviour.SampleState.Dissolved;
		}
	}
	
	public void setCloseUpFinished()
	{
		this.state = MainState.PostDissolution;
		this.pincet.state = PincetBehaviour.PincetState.Used;
	}
	
	public void setInitialFinished()
	{
		this.state = MainState.PincetStage;
		pincet.state = PincetBehaviour.PincetState.Idle;
	}
}
