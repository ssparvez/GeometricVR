using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
	public Text shapeViewText;
	public GameObject mainMenu;
	public GameObject shapeView;
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
					button.onClick.AddListener(delegate{OpenShapeView(button.name);});
					break;
			}
		}
	}
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
	void OpenShapeView(string shapeName)
	{
		mainMenu.SetActive(false);
		//format shape name and set text of shape view canvas
		shapeName = shapeName.Replace("Button", "");
		shapeViewText.text = Regex.Replace(shapeName, "(\\B[A-Z])", " $1");
		shapeView.SetActive(true);
	}
	void ToggleVRMode() 
	{
		// switch between regular and vr view
		GvrViewer viewer = GameObject.Find("GvrViewerMain").GetComponent<GvrViewer>();
		viewer.VRModeEnabled = !viewer.VRModeEnabled;
	}
}
