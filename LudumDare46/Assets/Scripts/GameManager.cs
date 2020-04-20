using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private UIManager uiManager;
    public int playerHealth;
    [HideInInspector]
    public bool gameHasEnded = false;
    [HideInInspector]
    public int playerScore;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if(gameHasEnded == false)
        {
            playerScore += Mathf.RoundToInt(100 * Time.deltaTime);
        }
        if(gameHasEnded == false && playerHealth <= 0)
        {
            gameHasEnded = true;
            EndGame();
        }
    }

    private void EndGame()
    {
        gameHasEnded = true;
        uiManager.gameOverUI.SetActive(true);
    }
}
