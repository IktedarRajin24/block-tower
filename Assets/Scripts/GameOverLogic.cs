using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLogic : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] public AudioSource splashAudio;
    [SerializeField] private float gameOverDelay = 2.0f;
    [SerializeField] private Transform waterTop;
    [SerializeField] private float moveSpeed = 1f;  
    [SerializeField] private float moveRange = 0.5f;

    private Vector3 initialPosition;

    private void Start()
    {
        splashAudio = GetComponent<AudioSource>();
        if (waterTop != null)
        {
            initialPosition = waterTop.position;
        }
    }
    private void Update()
    {
        if (waterTop != null)
        {
            float offset = Mathf.Sin(Time.time * moveSpeed) * moveRange;
            waterTop.position = new Vector3(initialPosition.x + offset, initialPosition.y, initialPosition.z);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        splashAudio.Play();
        ParticleSystem particle = collision.GetComponent<ParticleSystem>();
        Debug.Log(particle);
        particle.Play();
        StartCoroutine(DelayedGameOver());
    }

    private IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(gameOverDelay);
        gameManager.GameOver();
    }
}