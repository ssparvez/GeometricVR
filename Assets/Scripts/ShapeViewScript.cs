using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FlipWebApps.BeautifulTransitions.Scripts.Transitions.TransitionSteps;

public class ShapeViewScript : MonoBehaviour 
{
	public GameObject shapeMenu;
	public GameObject shapeView;
	public Text shapeViewText;
	public Button backButton;
	public Button nextButton;
	public Button prevButton;
	public GameObject[] shapes = new GameObject[9];
	public GameObject volumeFormula;
	public GameObject surfaceAreaFormula;
	private string[] volumes = {"a<sup>3</sup>", "4/3πr<sup>3</sup>", "πr<sup>2</sup>h", 
								"VERY LONG", "(1/3)AH", "a<sup>2</sup>(h/3)", 
								"lwh", "πr<sup>2</sup>(h/3)", "((3sqrt(3))/2)a<sup>2</sup>h"};
	private string[] surfaceAreas = {"6a<sup>2</sup>", "4πr<sup>2</sup>", "2πrh+2πr<sup>2</sup>",
									"DUNNO", "A+(3/2)bh", "LITTLE LONG",
									"2(lw + wh + hl)", "πr(r+sqrt(h<sup>2</sup>+r<sup>2</sup>))", "6ah+3sqrt(3)a<sup>2</sup>"};
    private int currentShapeIndex; // current shape being viewed
	private int frames = 0;

	void Awake()
	{
		
	}
	// Use this for initialization
	void Start() 
	{
        string currentShapeName = shapeViewText.text.Replace(" ", "");;
		Debug.Log(currentShapeName);
		ActivateShape(currentShapeName);
		// set formula text
		SetFormulas();

		backButton.onClick.AddListener(BackToShapeMenu);
		nextButton.onClick.AddListener(NextShape);
		prevButton.onClick.AddListener(PrevShape);
	}

	void OnEnable()
	{
		string currentShapeName = GameObject.Find("ShapeMenuText").GetComponent<Text>().text.Replace(" ", "");;
		ActivateShape(currentShapeName);
		SetFormulas();
	}
	
	// Update is called once per frame
	void Update() 
	{
		frames++;
		if(frames % 2 == 0)
		{
			shapes[currentShapeIndex].transform.Rotate(1,1,1);
			frames = 0;
		}
	}

	void BackToShapeMenu()
	{
		shapes[currentShapeIndex].SetActive(false);
		//currentShape.SetActive(false);
        shapeMenu.SetActive(true);
		Text shapeMenuText = GameObject.Find("ShapeMenuText").GetComponent<Text>();
		shapeMenuText.text = shapeViewText.text;
		shapeView.SetActive(false);
	}
	void ActivateShape(string currentShapeName)
	{
		for(int i = 0; i < shapes.Length; i++)
		{
			if(shapes[i].name == currentShapeName)
			{
				shapes[i].SetActive(true);
				currentShapeIndex = i;
				break;
			}
		}
	}
	
	void NextShape() // switches to next shape
	{
		int nextShapeIndex = (currentShapeIndex == shapes.Length-1) ? 0 : currentShapeIndex+1;
		shapes[nextShapeIndex].SetActive(true);
		// Create a new transition to move the gameobject with a delay of 1 and duration of 3
		var centerPosition = shapes[currentShapeIndex].transform.position;
		var leftPosition = new Vector3(centerPosition.x-10,centerPosition.y, centerPosition.z);
		var rightPosition = new Vector3(centerPosition.x+10,centerPosition.y, centerPosition.z);

		var transitionOut = new Move(shapes[currentShapeIndex],centerPosition , leftPosition, 0, 1);
		var transitionIn = new Move(shapes[nextShapeIndex],rightPosition , centerPosition, 0, 1);
		//transition next shape in
		transitionOut.AddOnCompleteAction(delegate{UpdateIndex(nextShapeIndex);});
		transitionOut.Start();
		transitionIn.Start();
	}

	void UpdateIndex(int newShapeIndex)
	{
		shapes[currentShapeIndex].SetActive(false);
		currentShapeIndex = newShapeIndex;
		// set formula text
		// volumeFormula.GetComponent<TextMeshPro>().text += volumes[currentShapeIndex];
		// surfaceAreaFormula.GetComponent<TextMeshPro>().text += surfaceArea[currentShapeIndex];
		shapeViewText.text = Regex.Replace(shapes[currentShapeIndex].name, "(\\B[A-Z])", " $1");
		SetFormulas();
	}
	void PrevShape() // switches to prev shape
	{
		int prevShapeIndex = (currentShapeIndex == 0) ? shapes.Length-1 : currentShapeIndex-1;
		shapes[prevShapeIndex].SetActive(true);

		var centerPosition = shapes[currentShapeIndex].transform.position;
		var rightPosition = new Vector3(centerPosition.x+10,centerPosition.y, centerPosition.z);
		var leftPosition = new Vector3(centerPosition.x-10,centerPosition.y, centerPosition.z);

		var transitionOut = new Move(shapes[currentShapeIndex],centerPosition , rightPosition, 0, 1);
		var transitionIn = new Move(shapes[prevShapeIndex],leftPosition , centerPosition, 0, 1);

		transitionOut.AddOnCompleteAction(delegate{UpdateIndex(prevShapeIndex);});
		transitionOut.Start();
		transitionIn.Start();
		
	}

	void SetFormulas()
	{
		volumeFormula.GetComponent<TextMeshProUGUI>().text = "V = " + volumes[currentShapeIndex];
		surfaceAreaFormula.GetComponent<TextMeshProUGUI>().text = "SA = " + surfaceAreas[currentShapeIndex];
	}
}