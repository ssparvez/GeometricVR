using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FlipWebApps.BeautifulTransitions.Scripts.Transitions.TransitionSteps;

public class VolumeTestScript : MonoBehaviour 
{
	public GameObject volumeTest;
	public GameObject surfaceAreaTest;
	public GameObject shapeMenu;
	public Text volumeTestText;
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
	private string[] volumes = {"a<sup>3</sup>", "4/3πr<sup>3</sup>", "πr<sup>2</sup>h", 
								"N/A", "(1/3)AH", "a<sup>2</sup>(h/3)", 
								"lwh", "πr<sup>2</sup>(h/3)", "((3sqrt(3))/2)a<sup>2</sup>h"};
	private string[,] incorrectVolumes = {{"a<sup>2</sup>", "3a", "6a"},
											 {"3/4πr<sup>3</sup>","4/3r<sup>3</sup>", "4/3πr<sup>2</sup>"}, 
											 {"πr<sup>2</sup>h", "πr<sup>3</sup>h", "πr<sup>2</sup>h"}, 
								{"N/A", "", ""}, 
								{"(1/3)AH", "", ""}, 
								{"a<sup>2</sup>(h/3)", "", ""}, 
								{"lwh", "", ""}, {"πr<sup>2</sup>(h/3)", "", ""}, {"((3sqrt(3))/2)a<sup>2</sup>h", "", ""}};
	// Use this for initialization
	void Start () 
	{
		defaultColor = optionButtons[1].GetComponent<Image>().color;
		currentShapeName = GetShapeName(volumeTestText.text);
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
		currentShapeName = GetShapeName(volumeTestText.text);
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
			if(Regex.Replace(shapes[i].name, "(\\B[A-Z])", " $1") == shapeName)
			{
				currentShapeIndex = i;
				break;
			}
		}
	}

	void SetOptionsText()
	{
		List<string> formulas = new List<string>();
		formulas.Add(volumes[currentShapeIndex]);
		formulas.Add(incorrectVolumes[currentShapeIndex, 0]);
		formulas.Add(incorrectVolumes[currentShapeIndex, 1]);
		formulas.Add(incorrectVolumes[currentShapeIndex, 2]);
		// shuffle formulas
		formulas.Shuffle();
		// set button texts
		for(int i = 0; i < 4; i++)
		{
			optionTexts[i].GetComponentInChildren<TextMeshProUGUI>().SetText(formulas[i]);
		}
	}

	void ValidateChoice(Button optionButton)
	{
		string optionName = optionButton.GetComponentInChildren<TextMeshProUGUI>().text;
		if(optionName == volumes[currentShapeIndex])
		{
			// success
			Debug.Log("Success");
			correctSound.Play();
			optionButton.GetComponent<Image>().color = new Color32(66, 244, 107, 255);
			// transition out
			surfaceAreaTest.SetActive(true);
			var centerPosition = volumeTest.transform.position;
			var leftPosition = new Vector3(centerPosition.x-10,centerPosition.y, centerPosition.z);
			var rightPosition = new Vector3(centerPosition.x+10,centerPosition.y, centerPosition.z);

			var transitionOut = new Move(volumeTest,centerPosition , leftPosition, 0, 1);
			var transitionIn = new Move(surfaceAreaTest,rightPosition , centerPosition, 0, 1);
			//transition next shape in
			transitionOut.AddOnCompleteAction(OpenSurfaceAreaTest);
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

	void OpenSurfaceAreaTest()
	{
		ResetColors();
		surfaceAreaTestText.text = "SA of " + currentShapeName + ":";
		Debug.Log(currentShapeName);
		volumeTest.SetActive(false);
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
		shapeName = shapeName.Replace("Volume of ", "");
		shapeName = shapeName.Replace(":", "");
		shapeName = shapeName.Replace(" ", "");
		Debug.Log("Current shape name: " + shapeName);
		return shapeName;
	}
	void BackToShapeMenu()
	{
		ResetColors();
		volumeTest.SetActive(false);
		shapeMenu.SetActive(true);
	}
}

static class MyExtensions
{
	//  shuffles arraylist
	public static void Shuffle<T>(this IList<T> list) 
	{
        int n = list.Count;
        System.Random rnd = new System.Random();
        while (n > 1) {
            int k = (rnd.Next(0, n) % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
	}
}