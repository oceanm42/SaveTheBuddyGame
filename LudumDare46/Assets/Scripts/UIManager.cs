using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField]
    private Slider health;
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public static bool GameIsPaused = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        health.maxValue = gameManager.playerHealth;
        health.value = gameManager.playerHealth;
        Resume();
    }

    private void Update()
    {
        health.value = gameManager.playerHealth;
        score.text = gameManager.playerScore.ToString();

        if(gameManager.gameHasEnded == false)
        {
            PauseMenu();
        }
    }

    private void PauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        pauseMenuUI.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
