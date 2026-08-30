using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject drawingCanvas;
    [SerializeField]
    private GameObject drawButton;

    [SerializeField] 
    private GameObject colourSelection;

    [SerializeField] private Image playerHealthBar;
    [SerializeField] private Image enemyHealthBar;
    
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

    public void WaitForEnemy()
    {
        drawingCanvas.SetActive(false);
        drawButton.SetActive(false);
        colourSelection.SetActive(false);
    }

    public void UpdatePlayerHealthBar(int health)
    {
        playerHealthBar.fillAmount = (float)health / 100;
    }

    public void UpdateEnemyHealthBar(int health)
    {
        enemyHealthBar.fillAmount = (float)health / 100;
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
