using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private TrapSpawner trapSpawner;
    private GameManager gameManager;
    private AudioManager audioManager;
    [SerializeField]
    private GameObject hitEffect;

    private void Start()
    {
        trapSpawner = FindObjectOfType<TrapSpawner>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameManager.gameHasEnded == false && UIManager.GameIsPaused == false)
        {
            if (collision.gameObject.tag == "Crusher")
            {
                gameManager.playerHealth -= trapSpawner.crusherDamage;
                HitEffect();
            }
            else if (collision.gameObject.tag == "Spikeball")
            {
                gameManager.playerHealth -= trapSpawner.spikeballDamage;
                HitEffect();
            }
            else if (collision.gameObject.tag == "Spike")
            {
                gameManager.playerHealth -= trapSpawner.spikeDamage;
                HitEffect();
            }
        }
    }

    private void HitEffect()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        audioManager.Play("Hit");
        StartCoroutine(DestroyEffect(effect, 5f));
    }

    private IEnumerator DestroyEffect(GameObject effect, float destroyTime)
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(effect);
    }
}