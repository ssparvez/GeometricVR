using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FlipWebApps.BeautifulTransitions.Scripts.Transitions.TransitionSteps;

public class SurfaceAreaTestScript : MonoBehaviour 
{
	public GameObject surfaceAreaTest;
	public GameObject shapeMenu;
	public Text surfaceAreaTestText;
	public GameObject[] optionTexts = new GameObject[4];
	public Button[] optionButtons = new Button[4];
	public GameObject[] shapes = new GameObject[9];
	public AudioSource correctSound;
	public AudioSource incorrectSound;

	public Button backButton;
	private Color defaultColor;
	private int currentShapeIndex;
	private string currentShapeName;
	private string[] surfaceAreas = {"6a<sup>2</sup>", "4πr<sup>2</sup>", "2πrh+2πr<sup>2</sup>",
									"N/A", "A+(3/2)bh", "a(a+sqrt(a<sup>2</sup>+4h<sup>2</sup>))",
									"2(lw+wh+hl)", "πr(r+sqrt(h<sup>2</sup>+r<sup>2</sup>))", "6ah+3sqrt(3)a<sup>2</sup>"};
	private string[,] incorrectSAs = {{"a<sup>3</sup>", "2(lw+wh+hl)", "6a"},
											 {"3/4πr<sup>3</sup>","4πr<sup>3</sup>", "4/3πr<sup>3</sup>"}, 
											 {"πr<sup>2</sup>h", "πr<sup>3</sup>h", "πr<sup>2</sup>h"}, 
								{"N/A", "", ""}, {"(1/3)AH", "", ""}, {"a<sup>2</sup>(h/3)", "", ""}, 
								{"lwh", "", ""}, {"πr<sup>2</sup>(h/3)", "", ""}, {"((3sqrt(3))/2)a<sup>2</sup>h", "", ""}};
	// Use this for initialization
	void Start () 
	{
		defaultColor = optionButtons[1].GetComponent<Image>().color;
		currentShapeName = GetShapeName(surfaceAreaTestText.text);
		SetShapeIndex(currentShapeName);
		SetOptionsText();
		foreach(Button button in optionButtons)
		{
			button.onClick.AddListener(delegate{ValidateChoice(button);});
		}
		backButton.onClick.AddListener(BackToShapeMenu);
	}

	void OnEnable()
	{
		currentShapeName = GetShapeName(surfaceAreaTestText.text);
		SetShapeIndex(currentShapeName);
		SetOptionsText();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetShapeIndex(string shapeName)
	{
		for(int i = 0; i < shapes.Length; i++)
		{
			if(shapes[i].name == shapeName)
			{
				currentShapeIndex = i;
				break;
			}
		}
	}

	void SetOptionsText()
	{
		List<string> formulas = new List<string>();
		formulas.Add(surfaceAreas[currentShapeIndex]);
		formulas.Add(incorrectSAs[currentShapeIndex, 0]);
		formulas.Add(incorrectSAs[currentShapeIndex, 1]);
		formulas.Add(incorrectSAs[currentShapeIndex, 2]);
		// shuffle formulas
		formulas.Shuffle();
		// set button texts
		for(int i = 0; i < 4; i++)
		{
			optionTexts[i].GetComponentInChildren<TextMeshProUGUI>().text = formulas[i];
		}
	}

	void ValidateChoice(Button optionButton)
	{
		string optionName = optionButton.GetComponentInChildren<TextMeshProUGUI>().text;
		if(optionName == surfaceAreas[currentShapeIndex])
		{
			// success
			Debug.Log("Success");
			correctSound.Play();
			optionButton.GetComponent<Image>().color = new Color32(66, 244, 107, 255);
			// transition out
			shapeMenu.SetActive(true);
			var centerPosition = surfaceAreaTest.transform.position;
			var leftPosition = new Vector3(centerPosition.x-10,centerPosition.y, centerPosition.z);
			var topPosition = new Vector3(centerPosition.x,centerPosition.y+10, centerPosition.z);

			var transitionOut = new Move(surfaceAreaTest,centerPosition , leftPosition, 0, 1);
			var transitionIn = new Move(shapeMenu,topPosition , centerPosition, 0, 1);
			//transition next shape in
			transitionOut.AddOnCompleteAction(BackToShapeMenu);
			transitionOut.Start();
			transitionIn.Start();
		}
		else
		{
			// failure
			Debug.Log("Failure");
			incorrectSound.Play();
			optionButton.GetComponent<Image>().color = new Color32(244, 65, 65, 255);
		}
	}

	void ResetColors()
	{
		foreach(Button optionButton in optionButtons)
		{
			optionButton.GetComponent<Image>().color = defaultColor;
		}
	}

	string GetShapeName(string shapeName)
	{
		shapeName = shapeName.Replace("SA of ", "");
		shapeName = shapeName.Replace(":", "");
		shapeName = shapeName.Replace(" ", "");
		return shapeName;
	}
	void BackToShapeMenu()
	{
		ResetColors();
		surfaceAreaTest.SetActive(false);
		shapeMenu.SetActive(true);
	}
}