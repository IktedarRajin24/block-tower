using UnityEngine;

public class BlockFallIndicator : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;
    [SerializeField] public AudioSource dropAudio;

    private bool hasFallen = false;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        dropAudio = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasFallen && (collision.gameObject.tag == "Base" || collision.gameObject.tag == "Block"))
        {
            gameManager.IncreaseScore();
            hasFallen = true;
            dropAudio.Play();
        }
    }
}
