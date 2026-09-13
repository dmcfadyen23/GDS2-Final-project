using UnityEngine;

public class EndTurn : MonoBehaviour
{

    public void FinishTurn(int i)
    {
        UIManager uiManager = FindAnyObjectByType<UIManager>();
        uiManager.GoToMain();
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
