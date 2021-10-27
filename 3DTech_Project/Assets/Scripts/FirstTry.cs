using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PDollarGestureRecognizer;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Random = System.Random;

public class FirstTry : MonoBehaviour
{

	public FirstTry instance; 
	public Transform gestureOnScreenPrefab;
	private List<Gesture> trainingSet = new List<Gesture>();
	private List<Point> points = new List<Point>();
	private int _strokeId = -1;
	private Vector3 _virtualKeyPosition = Vector2.zero;
	private Rect _drawArea;
	private RuntimePlatform _platform;
	private int _vertexCount = 0;
	public List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
	private LineRenderer _currentGestureLineRenderer;
	
	//GUI
	private string _message;
	private bool _recognized;
	private string newGestureName = "";

	private bool _isRecognize = false;
	
	
	private Gesture _currentGesture;
	public Text gestureText;

	private float score =0;
	public Text scoreText;

	
	
	public static FirstTry Instance { get; private set; }
	
	void Awake()
	{
		if (instance != null && instance != this)
			Destroy(gameObject);    // Suppression d'une instance précédente (sécurité...sécurité...)
 
		instance = this;
	}
	void Start ()
	{
		_platform = Application.platform;
		_drawArea = new Rect(Screen.width/7, Screen.height/2 - (Screen.height/4)/2, Screen.width/4, Screen.height/3);

		LoadPreMadeGesture();
		LoadCustomGesture();
		GetRandomPattern();
	}
	
	void Update ()
	{

		Recognition();
		if (_isRecognize == true)
		{
			_isRecognize = false;
		}
	}

	private void Recognition()
	{
		if (_platform == RuntimePlatform.Android || _platform == RuntimePlatform.IPhonePlayer)
		{
			if (Input.touchCount > 0)
			{
				_virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
			}
		}
		else
		{
			if (Input.GetMouseButton(0))
			{
				_virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
			}
		}

		if (_drawArea.Contains(_virtualKeyPosition))
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (_recognized)
				{
					_recognized = false;
					_strokeId = -1;

					points.Clear();

					foreach (LineRenderer lineRenderer in gestureLinesRenderer)
					{
						lineRenderer.SetVertexCount(0);
						Destroy(lineRenderer.gameObject);
					}

					gestureLinesRenderer.Clear();
				}

				++_strokeId;

				Transform tmpGesture =
					Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
				_currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();

				gestureLinesRenderer.Add(_currentGestureLineRenderer);

				_vertexCount = 0;
			}

			if (Input.GetMouseButton(0))
			{
				points.Add(new Point(_virtualKeyPosition.x, -_virtualKeyPosition.y, _strokeId));

				_currentGestureLineRenderer.SetVertexCount(++_vertexCount);
				_currentGestureLineRenderer.SetPosition(_vertexCount - 1,
					Camera.main.ScreenToWorldPoint(new Vector3(_virtualKeyPosition.x, _virtualKeyPosition.y, 10)));
			}
		}
	}

	private void OnGUI()
	{
		GUI.Box(_drawArea, "Draw Area");
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

	public void  StartRecognize()
	{
		Debug.Log("Do reco");
		_isRecognize = false;
		_recognized = true;

		Gesture candidate = new Gesture(points.ToArray());
		Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
		if (gestureResult.GestureClass == _currentGesture.Name && gestureResult.Score >0.7)
		{
			_isRecognize = true;
			score += gestureResult.Score * 100;
			GetRandomPattern();
		}	
		_message = gestureResult.GestureClass + " " + gestureResult.Score;
		scoreText.text = score.ToString(); 
	}

	private void GetRandomPattern()
	{
		float random = UnityEngine.Random.Range(0,trainingSet.Count);
		_currentGesture = trainingSet[(int)random];
		gestureText.text = _currentGesture.Name;

	}
	
}
