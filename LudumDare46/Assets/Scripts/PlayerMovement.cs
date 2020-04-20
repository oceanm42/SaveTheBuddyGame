using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameManager gameManager;

    [SerializeField]
    private float force;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainScene")
        {
            if (gameManager.gameHasEnded == false && UIManager.GameIsPaused == false)
            {
                Movement();
            }
        }
        else
        {
            Movement();
        }
    }

    private void Movement()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(mousePos * force);
        }
    }
}
