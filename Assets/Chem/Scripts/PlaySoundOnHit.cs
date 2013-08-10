using UnityEngine;
using System.Collections;

public class PlaySoundOnHit : MonoBehaviour {
	
	AudioSource audioSource;
	
	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.name.Contains("Container") || collision.collider.tag == "Dynamic")
		{
			Debug.Log(collision.relativeVelocity.magnitude);
			if(collision.relativeVelocity.magnitude > 4.0f)
				if(audioSource)
					audioSource.Play();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
