using UnityEngine;
using System.Collections;

public class TextColorSetter : MonoBehaviour {
	
	public Color selectedColor = Color.white;
	
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.color = selectedColor;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
