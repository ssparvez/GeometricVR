using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasterEggScript : MonoBehaviour {
	public GameObject torus;
	public Button eggButton;
	public GameObject torusText;
	private int frames = 0;
	// Use this for initialization
	void Start () 
	{
		eggButton.onClick.AddListener(ToggleTorus);
	}
	// Update is called once per frame
	void Update () {
		frames++;
		if(frames % 2 == 0)
		{
			torus.transform.Rotate(1,1,1);
			frames = 0;
		}
	}
	void ToggleTorus()
	{
		torus.SetActive(!torus.activeSelf);
		torusText.SetActive(!torusText.activeSelf);
	}
}
