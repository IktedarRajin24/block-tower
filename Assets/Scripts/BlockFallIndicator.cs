using UnityEngine;

public class BlockFallIndicator : MonoBehaviour
{
    [SerializeField] public GameManager gameManager;
    [SerializeField] public AudioSource dropAudio;
    [SerializeField] public ParticleSystem splashParticle;
    public LineRenderer lineRenderer;
    private bool hasFallen = false;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        dropAudio = GetComponent<AudioSource>();
        lineRenderer = GetComponentInChildren<LineRenderer>();
        splashParticle = GetComponent<ParticleSystem>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasFallen && (collision.gameObject.tag == "Base" || collision.gameObject.tag == "Block"))
        {
            gameManager.IncreaseScore();
            hasFallen = true;
            dropAudio.Play();
            lineRenderer.enabled = false;
        }
        if(collision.gameObject.tag == "Water")
        {
            splashParticle.Play();
        }
    }
}
