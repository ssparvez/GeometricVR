using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject shapeMenu;
	public GameObject settingsMenu;
	public GameObject creditsMenu;
	public Button[] buttons;
	// Use this for initialization
	void Start () 
	{
		Cursor.visible = false;
		buttons = this.GetComponentsInChildren<Button>(true);
		foreach(Button button in buttons)
		{
			switch(button.name)
			{
				case "SettingsButton":
					button.onClick.AddListener(OpenSettingsMenu);
					break;
				case "CreditsButton":
					button.onClick.AddListener(OpenCreditsMenu);
					break;
				case "VRModeButton":
					button.onClick.AddListener(ToggleVRMode);
					break;
				default:
					button.onClick.AddListener(delegate{OpenShapeMenu(button.name);});
					break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {}

	void OpenSettingsMenu()
	{
		mainMenu.SetActive(false);
		settingsMenu.SetActive(true);
	}

	void OpenCreditsMenu()
	{
		mainMenu.SetActive(false);
		creditsMenu.SetActive(true);
	}

	void OpenShapeMenu(string shapeName)
	{
		mainMenu.SetActive(false);
		shapeMenu.SetActive(true);
		Text shapeMenuText = GameObject.Find("ShapeMenuText").GetComponent<Text>();
		//format shape name
		shapeName = shapeName.Replace("Button", "");
		shapeMenuText.text = Regex.Replace(shapeName, "(\\B[A-Z])", " $1");
	}

	void ToggleVRMode() 
	{
		// switch between regular and vr view
		GvrViewer viewer = GameObject.Find("GvrViewerMain").GetComponent<GvrViewer>();
		viewer.VRModeEnabled = !viewer.VRModeEnabled;
	}
}
