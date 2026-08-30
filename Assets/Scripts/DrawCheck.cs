using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using PDollarGestureRecognizer;
using UnityEngine.InputSystem;

public class DrawCheck : MonoBehaviour
{
    private Gesture[] trainingSet = null;
    private CanvasDrawer canvasDrawer;
    public readonly List<Point> CandidatePoints = new List<Point>();
    private UIManager uiManager;

    public void FinishDrawing()
    {
        if (CandidatePoints.Count > 0)
        {
            Debug.Log("finish");
            Gesture candidate = new Gesture(CandidatePoints.ToArray());
            string gestureShape = PointCloudRecognizer.Classify(candidate, trainingSet);
            Debug.Log("shape is " + gestureShape);
            uiManager.GoToMain();
        }
        else
        {
            Debug.Log("No drawing found");
        }
    }

    public void WriteDrawing()
    {
        if (CandidatePoints.Count > 0)
        {
            Debug.Log("writing drawing to database");
            string gestureName = "CircleWithCross";
            int currentStroke = 0;
            Gesture candidate = new Gesture(CandidatePoints.ToArray(), gestureName);
            string filepath = Path.Combine(Application.dataPath, "ShapeData", gestureName + "2.txt");
            Debug.Log(filepath);
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine("Gesture, " + gestureName);
                for (int i = 0; i < candidate.Points.Length; i++)
                {
                    if (currentStroke == candidate.Points[i].StrokeID)
                    {
                        writer.WriteLine(candidate.Points[i].X.ToString() + ", " + candidate.Points[i].Y.ToString() + ", " + candidate.Points[i].StrokeID);
                    }
                    else
                    {
                        currentStroke++;
                    }
                }
            }
            uiManager.GoToMain();
        }
    }

    public void ClearDrawing()
    {
        canvasDrawer.ClearCanvas();
        CandidatePoints.Clear();
        Debug.Log(CandidatePoints.Count());
    }
    
    private Gesture[] LoadTrainingSet()
    {
        List<Gesture> gestures = new List<Gesture>();
        string[] gestureFolders = Directory.GetDirectories(Application.dataPath);
        foreach (string folder in gestureFolders)
        {
            string[] gestureFiles = Directory.GetFiles(folder, "*.txt");
            foreach (string file in gestureFiles)
                gestures.Add(ReadGesture(file));
        }
        return gestures.ToArray();
    }

    private Gesture ReadGesture(string filename)
    {
        List<Point> points = new List<Point>();
        string gestureName = "";
        if (File.Exists(filename))
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] fields = line.Split(", ");
                    switch (fields[0])
                    {
                        case "Gesture":
                            gestureName = fields[1];
                            break;
                        default:
                            float x = float.Parse(fields[0]);
                            float y = float.Parse(fields[1]);
                            int strokeID = int.Parse(fields[2]);
                            points.Add(new Point(x, y, strokeID));
                            break;
                    }
                    
                    
                }
            }
            
        }
        return new Gesture(points.ToArray(), gestureName);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trainingSet = LoadTrainingSet();
        Debug.Log(trainingSet.Length);
        // string str = "";
        // foreach (var point in trainingSet[0].Points)
        // {
        //     str += point.X.ToString() + ", " + point.Y.ToString() + ", ";
        // }
        // Debug.Log(str);
        canvasDrawer = GetComponent<CanvasDrawer>();
        uiManager = FindAnyObjectByType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // if (Keyboard.current.spaceKey.wasPressedThisFrame)
        // {
        //     canvasDrawer.ToggleBrushColour();
        // }
        //
        // if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        // {
        //     canvasDrawer.ClearCanvas();
        //     CandidatePoints.Clear();
        //     Debug.Log(CandidatePoints.Count());
        // }
        //
        // if (Keyboard.current.enterKey.wasPressedThisFrame && CandidatePoints.Count > 0)
        // {
        //     Debug.Log("finish");
        //     Gesture candidate = new Gesture(CandidatePoints.ToArray());
        //     string gestureShape = PointCloudRecognizer.Classify(candidate, trainingSet);
        //     Debug.Log("shape is " + gestureShape);
        // }
    }
}
