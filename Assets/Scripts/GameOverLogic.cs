using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLogic : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] public AudioSource splashAudio;
    [SerializeField] private float gameOverDelay = 2.0f;

    private void Start()
    {
        splashAudio = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        splashAudio.Play();
        StartCoroutine(DelayedGameOver());
    }

    private IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(gameOverDelay);
        gameManager.GameOver();
    }
}