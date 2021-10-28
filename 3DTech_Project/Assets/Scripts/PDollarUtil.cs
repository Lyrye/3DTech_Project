using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PDollarGestureRecognizer;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


namespace Util
{
    public class PDollarUtil  : MonoBehaviour
    {
        public static void CleanDrawingArea(bool recognized,int strokeId,List<Point> points, List<LineRenderer> gestureLinesRenderer)
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

        public static List<Gesture> LoadPreMadeGesture()
        {
	        List<Gesture> trainingSet = new List<Gesture>();
	        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
	        foreach (TextAsset gestureXml in gesturesXml)
		        trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

	        return trainingSet;
        }
        
        public static List<Gesture> LoadCustomGesture()
        {
	        List<Gesture> trainingSet = new List<Gesture>();
	        string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
	        foreach (string filePath in filePaths)
		        trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
	        return trainingSet;
        }
        
        
    }
}