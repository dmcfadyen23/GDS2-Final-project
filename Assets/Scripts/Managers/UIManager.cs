using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject drawingCanvas;
    [SerializeField]
    private GameObject drawButton;

    [SerializeField] 
    private GameObject colourSelection;
    
    public void GoToDrawing()
    {
        drawingCanvas.SetActive(true);
        drawButton.SetActive(false);
        colourSelection.SetActive(false);
    }

    public void GoToMain()
    {
        drawingCanvas.SetActive(false);
        drawButton.SetActive(true);
        colourSelection.SetActive(false);
    }

    public void GoToColour()
    {
        drawingCanvas.SetActive(false);
        drawButton.SetActive(false);
        colourSelection.SetActive(true);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
