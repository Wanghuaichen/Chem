using UnityEngine;
using System.Collections;

public class ContBender : MonoBehaviour {
	
	private bool bending = false;
	
	public float BendAngle = 45.0f;
	
	private Quaternion initialOrient;
	private Quaternion bendedOrient;
	
	private AudioSource sound;
	
	// Use this for initialization
	void Start () {
		initialOrient = this.transform.rotation;
		bendedOrient = Quaternion.AngleAxis(BendAngle, Vector3.forward) * initialOrient;
		sound = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
		if(this.Bending)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, bendedOrient, (float)(Time.deltaTime));
		}
		else
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, initialOrient, (float)(Time.deltaTime));
		}
	}
	
	public bool Bending 
	{
		get { return this.bending;  }
		set { 
			if(this.bending == value)
				return;
			
			this.bending = value;

			if(bending == true)
			{
				if(!sound.isPlaying)
					sound.Play();
			}
			else
			{
				sound.Stop();
			}
		}
	}
}
