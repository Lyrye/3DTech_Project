using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using PDollarGestureRecognizer;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AddNewGesture : MonoBehaviour
{
    public Transform gestureOnScreenPrefab;
    
    
    private List<Gesture> trainingSet = new List<Gesture>();
    private List<Point> points = new List<Point>();
    private int strokeId = -1;
    private Vector3 virtualKeyPosition = Vector2.zero;
    private Rect drawArea;
    private RuntimePlatform platform;
    private int vertexCount = 0;
    private List<LineRenderer> gestureLinesRenderer = new List<LineRenderer>();
    private LineRenderer currentGestureLineRenderer;
    private bool recognized;
    
    public InputField newGestureName;
    public Button addGesture;
    public Button clear;
    public Button back;

    public GameObject ErrorInName; 
    void Start()
    {
        platform = Application.platform;   
        drawArea = new Rect(0, 0, Screen.width - Screen.width / 3, Screen.height);
        ErrorInName.SetActive(false);
        addGesture.onClick.AddListener(AddGesture);
        clear.onClick.AddListener(Clear);
        back.onClick.AddListener(GoToManageGesture);
        LoadGesture();
    }

  

    // Update is called once per frame
    void Update()
    {
        Recognize(); 
    }

    void OnGUI()
    {
        GUI.Box(drawArea, "Draw Area");
    }

    private void AddGesture()
    {
        
        if (newGestureName.text == "") ErrorInName.SetActive(true); 
        else
        {
            Debug.Log(Application.persistentDataPath);
            ErrorInName.SetActive(false);
            string fileName = String.Format("{0}/{1}-{2}.xml", Application.persistentDataPath, newGestureName.text, DateTime.Now.ToFileTime());
            trainingSet.Add(new Gesture(points.ToArray(), newGestureName.text));
            #if !UNITY_WEBPLAYER
                     GestureIO.WriteGesture(points.ToArray(), newGestureName.text, fileName);
            #endif
            newGestureName.text = "";
            Util.PDollarUtil.CleanDrawingArea(recognized,strokeId,points,gestureLinesRenderer);
        }
       
    }
    private void Recognize()
    {
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer) {
            if (Input.touchCount > 0) {
                virtualKeyPosition = new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y);
            }
        } else {
            if (Input.GetMouseButton(0)) {
                virtualKeyPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
            }
        }

        if (drawArea.Contains(virtualKeyPosition)) {

            if (Input.GetMouseButtonDown(0)) {

                if (recognized) {

                    recognized = false;
                    strokeId = -1;

                    points.Clear();

                    foreach (LineRenderer lineRenderer in gestureLinesRenderer) {

                        lineRenderer.SetVertexCount(0);
                        Destroy(lineRenderer.gameObject);
                    }

                    gestureLinesRenderer.Clear();
                }

                ++strokeId;
				
                Transform tmpGesture = Instantiate(gestureOnScreenPrefab, transform.position, transform.rotation) as Transform;
                currentGestureLineRenderer = tmpGesture.GetComponent<LineRenderer>();
				
                gestureLinesRenderer.Add(currentGestureLineRenderer);
				
                vertexCount = 0;
            }
			
            if (Input.GetMouseButton(0)) {
                points.Add(new Point(virtualKeyPosition.x, -virtualKeyPosition.y, strokeId));

                currentGestureLineRenderer.SetVertexCount(++vertexCount);
                currentGestureLineRenderer.SetPosition(vertexCount - 1, Camera.main.ScreenToWorldPoint(new Vector3(virtualKeyPosition.x, virtualKeyPosition.y, 10)));
            }
        }
    }
    private void LoadGesture()
    {
        //Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        //Load user custom gestures
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (string filePath in filePaths)
            trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
    }

    private void Clear()
    {
        Util.PDollarUtil.CleanDrawingArea(recognized,strokeId,points,gestureLinesRenderer);
    }

    private void GoToManageGesture()
    {
        SceneManager.LoadScene("ManageGesture"); 
    }
}
