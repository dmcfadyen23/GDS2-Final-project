using System.Collections;
using TMPro;
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

    public TextMeshProUGUI battleLogText;

    [SerializeField] private Slider playerHealthBar;
    [SerializeField] public Slider[] enemyHealthBars = new Slider[3];
    
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

    public void UpdatePlayerHealthBar(float health)
    {
        playerHealthBar.value = health;
    }

    public void UpdateEnemyHealthBar(float health, int enemyIndex)
    {
        if (health <= 0)
        {
            enemyHealthBars[enemyIndex].gameObject.SetActive(false);
        }
        enemyHealthBars[enemyIndex].value = health;
        
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
