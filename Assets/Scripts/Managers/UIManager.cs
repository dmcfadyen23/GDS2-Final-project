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
    
    public GameObject targetReticle;
    public GameObject targetSelection;
    public GameObject[] enemyTargetSelection = new GameObject[3];

    [SerializeField] private Slider playerHealthBar;
    [SerializeField] public Slider[] enemyHealthBars = new Slider[3];

    
    public void GoToDrawing()
    {
        drawingCanvas.SetActive(true);
        drawButton.SetActive(false);
        colourSelection.SetActive(false);
        targetReticle.SetActive(false);
        targetSelection.SetActive(false);
    }

    public void GoToMain()
    {
        drawingCanvas.SetActive(false);
        drawButton.SetActive(true);
        colourSelection.SetActive(false);
        targetReticle.SetActive(false);
        targetSelection.SetActive(false);
    }

    public void GoToTargeting()
    {
        drawingCanvas.SetActive(false);
        drawButton.SetActive(false);
        colourSelection.SetActive(false);
        targetReticle.SetActive(true);
        targetSelection.SetActive(true);
        CombatManager combatManager = FindAnyObjectByType<CombatManager>();
        for (int i = 0; i < combatManager.GetAllEnemies().Length; i++)
        {
            if (combatManager.GetEnemy(i).activeSelf)
            {
                enemyTargetSelection[i].SetActive(true);
            }
            else
            {
                enemyTargetSelection[i].SetActive(false);
            }
        }
    }

    public void GoToColour()
    {
        drawingCanvas.SetActive(false);
        drawButton.SetActive(false);
        colourSelection.SetActive(true);
        targetReticle.SetActive(false);
        targetSelection.SetActive(false);
    }

    public void WaitForEnemy()
    {
        drawingCanvas.SetActive(false);
        drawButton.SetActive(false);
        colourSelection.SetActive(false);
        targetReticle.SetActive(false);
        targetSelection.SetActive(false);
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
