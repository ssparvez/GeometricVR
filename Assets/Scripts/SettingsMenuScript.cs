using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour 
{
	public GameObject mainMenu;
	public GameObject settingsMenu;
	public Slider volumeSlider;
	public Button backButton;
	// Use this for initialization
	void Start () 
	{
		volumeSlider.value = AudioListener.volume; // initialize vol slider position
		volumeSlider.onValueChanged.AddListener(delegate {ChangeVolume(); });
		backButton.onClick.AddListener(BackToMainMenu);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	// changes all system volume
	void ChangeVolume()
	{
		AudioListener.volume = volumeSlider.value;
	}

	void BackToMainMenu()
	{
		settingsMenu.SetActive(false);
		mainMenu.SetActive(true);
	}
}
