using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform block;
    [SerializeField] private Transform blockHolder;

    private Transform currentBlock = null;
    private Rigidbody2D currentRB;

    private Vector2 blockStartPosition = new Vector2(0f, 4f);

    private float blockSpeed = 8f;
    private float blockSpeedIncrement = 0.5f;
    private int blockDirection = 1;
    private float xLimit = 5;
    private float timeBetweenSpawn = 1f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnNewBlock();
    }
    private IEnumerator DelayedSpawner()
    {
        yield return new WaitForSeconds(timeBetweenSpawn);
        SpawnNewBlock();
    }
    // Update is called once per frame
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
                currentBlock = null;
                currentRB.simulated = true;
                StartCoroutine(DelayedSpawner());
            }
        }
    }
    private void SpawnNewBlock()
    {
        currentBlock = Instantiate(block, blockHolder);
        currentBlock.position = blockStartPosition;
        currentBlock.GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        currentRB = currentBlock.GetComponent<Rigidbody2D>();
        blockSpeed += blockSpeedIncrement; 
    }

    public void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
