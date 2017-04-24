using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreditsMenuScript : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject creditsMenu;
    public Button backButton;
	// Use this for initialization
	void Start () 
	{
        backButton.onClick.AddListener(GoBackToMainMenu);
	}
	
	// Update is called once per frame
	void Update () {}

	void GoBackToMainMenu()
	{
        creditsMenu.SetActive(false);
		mainMenu.SetActive(true);
	}
}
