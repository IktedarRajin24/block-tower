using System.Collections;
using UnityEngine;

public class AreaDetector : MonoBehaviour
{
    private bool isInCollider = false;
    [SerializeField] private Transform blockHolder;
    public float targetHeight = 0.8f;
    public float speed = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        isInCollider = true;
        StartCoroutine(UpdatePositionAfterDelay());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isInCollider = false;
    }

    private IEnumerator UpdatePositionAfterDelay()
    {
        yield return new WaitForSeconds(1.5f); // Delay before movement starts

        if (isInCollider)
        {
            float elapsedTime = 0f;
            float duration = 1.5f; // Adjust duration for smooth movement

            Vector3 startBlockPos = blockHolder.position;
            Vector3 targetBlockPos = startBlockPos + new Vector3(0, targetHeight, 0);

            Vector3 startObjectPos = transform.position;
            Vector3 targetObjectPos = startObjectPos + new Vector3(0, 0.2f, 0);

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                blockHolder.position = Vector3.Lerp(startBlockPos, targetBlockPos, t);
                transform.position = Vector3.Lerp(startObjectPos, targetObjectPos, t);

                elapsedTime += Time.deltaTime * speed;
                yield return null;
            }

            // Ensure exact final positions
            blockHolder.position = targetBlockPos;
            transform.position = targetObjectPos;
        }
    }
}
