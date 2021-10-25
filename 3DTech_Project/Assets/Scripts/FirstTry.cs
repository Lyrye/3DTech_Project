using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PDollarGestureRecognizer;
using UnityEngine.UI;
using Random = System.Random;

public class FirstTry : MonoBehaviour {

	public Transform gestureOnScreenPrefab;
	private List<Gesture> trainingSet = new List<Gesture>();
	private List<Point> points = new List<Point>();
	private int strokeId = -1;
	private Vector3 virtualKeyPosition = Vector2.zero;
	public Rect drawArea;
	private RuntimePlatform platform;
	private int vertexCount = 0;
	public List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
	private LineRenderer currentGestureLineRenderer;


	public Text feedback;
	//GUI
	private string message;
	private bool recognized;
	private string newGestureName = "";

	private bool isRecognize = false;
	public GameObject reward;
	
	public Button recognize;
	public Button getPattern;
	private Gesture currentGesture;
	public Text GestureText;

	void Start ()
	{
		reward.SetActive(false);
		recognize.onClick.AddListener(StartRecognize);
		getPattern.onClick.AddListener(GetRandomPattern);
		platform = Application.platform;
		drawArea = new Rect(Screen.width/7, Screen.height/2 - (Screen.height/4)/2, Screen.width/4, Screen.height/3);

		LoadPreMadeGesture();
		LoadCustomGesture();
	}
	
	void Update ()
	{
		Recognition();
		if (isRecognize == true)
		{
			reward.SetActive(true);
			isRecognize = false;
		}
	}

	private void Recognition()
	{
		if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
		{
			if (Input.touchCount > 0)
			{
				virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
			}
		}
		else
		{
			if (Input.GetMouseButton(0))
			{
				virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
			}
		}

		if (drawArea.Contains(virtualKeyPosition))
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (recognized)
				{
					recognized = false;
					strokeId = -1;

					points.Clear();

					foreach (LineRenderer lineRenderer in gestureLinesRenderer)
					{
						lineRenderer.SetVertexCount(0);
						Destroy(lineRenderer.gameObject);
					}

					gestureLinesRenderer.Clear();
				}

				++strokeId;

				Transform tmpGesture =
					Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
				currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

				gestureLinesRenderer.Add(currentGestureLineRenderer);

				vertexCount = 0;
			}

			if (Input.GetMouseButton(0))
			{
				points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

				currentGestureLineRenderer.SetVertexCount(++vertexCount);
				currentGestureLineRenderer.SetPosition(vertexCount - 1,
					Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
			}
		}
	}

	private void OnGUI()
	{
		GUI.Box(drawArea, "Draw Area");
	}

	private void LoadCustomGesture()
	{
		string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
		foreach (string filePath in filePaths)
			trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
	}
	private void LoadPreMadeGesture()
	{
		TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
		foreach (TextAsset gestureXml in gesturesXml)
			trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));
	}

	private void StartRecognize()
	{
		isRecognize = false;
		recognized = true;

		Gesture candidate = new Gesture(points.ToArray());
		Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
		if (gestureResult.GestureClass == currentGesture.Name && gestureResult.Score >0.7)
		{
			isRecognize = true;
			reward.SetActive(true);
			Debug.Log("It is the right pattern");
		}	
		message += gestureResult.GestureClass + " " + gestureResult.Score;
		feedback.text = message;
	}

	private void GetRandomPattern()
	{
		float random = UnityEngine.Random.Range(0,trainingSet.Count);
		currentGesture = trainingSet[(int)random];
		GestureText.text = currentGesture.Name;

	}
	
}
