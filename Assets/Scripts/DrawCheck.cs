using System.Collections.Generic;
using System.IO;
using UnityEngine;
using PDollarGestureRecognizer;
using UnityEngine.InputSystem;

public class DrawCheck : MonoBehaviour
{
    private Gesture[] trainingSet = null;
    private CanvasDrawer canvasDrawer;

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
        int row = 32;
        int stroke = -1;
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
                        case "Stroke":
                            stroke++;
                            break;
                        default:
                            int col = 0;
                            row--;
                    
                            foreach (string x in fields)
                            {
                                if (x == "0xff000000")
                                {
                                    points.Add(new Point(col, row, stroke));
                                }
                                col++;
                            }

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
        canvasDrawer = GetComponent<CanvasDrawer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            canvasDrawer.ToggleBrushColour();
        }
        
        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            canvasDrawer.ClearCanvas();
        }

        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Debug.Log("finish");
        }
    }
}
