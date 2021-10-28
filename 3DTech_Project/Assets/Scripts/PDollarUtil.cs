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
    }
}