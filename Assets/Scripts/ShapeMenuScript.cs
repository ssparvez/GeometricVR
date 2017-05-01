using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShapeMenuScript : MonoBehaviour 
{
	public Text shapeMenuText;
	public Text volumeTestText;
	public GameObject shapeMenu; // current screen
	public GameObject mainMenu; // prev screen
	public GameObject shapeView; // next screen
	public GameObject volumeTest;
	public Button visualizeButton;
	public Button testButton;
	public Button backButton;
	// Use this for initialization
	void Start() 
	{
		Debug.Log("HELLLO");
		// add handlers for buttons
		visualizeButton.onClick.AddListener(OpenShapeView);
		testButton.onClick.AddListener(OpenVolumeView);
		backButton.onClick.AddListener(BackToMainMenu);
	}
	
	// Update is called once per frame
	void Update() {}

	void OpenShapeView()
	{
		shapeView.SetActive(true);
		// set shape name of shape view
		Text shapeViewText = GameObject.Find("ShapeViewText").GetComponent<Text>();
		shapeViewText.text = shapeMenuText.text;
		shapeMenu.SetActive(false);
	}

	void OpenVolumeView()
	{
		volumeTest.SetActive(true);
		volumeTestText.text = "Volume of " + shapeMenuText.text + ":";
		shapeMenu.SetActive(false);
	}

	void BackToMainMenu()
	{
		shapeMenu.SetActive(false);
		mainMenu.SetActive(true);
	}
}
