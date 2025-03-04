using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform[] blocks;
    [SerializeField] private Transform blockHolder;
    [SerializeField] private TMP_Text scoreText;

    private Transform currentBlock = null;
    private Rigidbody2D currentRB;

    private float blockSpeed = 1.5f;
    private int blockDirection = 1;
    private float xLimit = 2;
    private float timeBetweenSpawn = 1f;
    private int score = 0;

    void Start()
    {
        SpawnNewBlock();
    }

    private IEnumerator DelayedSpawner()
    {
        yield return new WaitForSeconds(timeBetweenSpawn);
        SpawnNewBlock();
    }

    void Update()
    {
        if (currentBlock)
        {
            float moveAmount = Time.deltaTime * blockSpeed * blockDirection;
            currentBlock.position += new Vector3(moveAmount, 0, 0);

            if (Mathf.Abs(currentBlock.position.x) > xLimit)
            {
                currentBlock.position = new Vector3(blockDirection * xLimit, currentBlock.position.y, 0);
                blockDirection = -blockDirection;
            }

            if (Input.GetMouseButtonDown(0))
            {
                currentBlock.SetParent(null);
                currentBlock = null;
                currentRB.simulated = true;
                StartCoroutine(DelayedSpawner());
            }
        }
    }

    public void IncreaseScore()
    {
        score += 1;
        scoreText.text = "Score: "+ score.ToString();
    }
    private void SpawnNewBlock()
    {
        if (blocks.Length == 0) return;

        int randomIndex = Random.Range(0, blocks.Length);
        currentBlock = Instantiate(blocks[randomIndex]);
        currentBlock.position = blockHolder.position;
        currentRB = currentBlock.GetComponent<Rigidbody2D>();
        currentBlock.GetComponent<BlockFallIndicator>().gameManager = this;
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
