using System.Collections;
using UnityEngine;

public class AreaDetector : MonoBehaviour
{
    private bool isInCollider = false;
    [SerializeField] private Transform blockHolder;
    [SerializeField] private Transform ground;
    public float speed = 2f;
    public float maxBlockHeight = 6.0f; // Maximum Y position for blockHolder

    private float previousHeight = 0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        float colliderTopY = other.bounds.max.y; // Get collider's top Y in world space
        float currentAreaY = transform.position.y;

        // Check if the collider's top is higher than the current area position
        if (colliderTopY > currentAreaY)
        {
            isInCollider = true;
            float moveDistance = colliderTopY - currentAreaY;
            StartCoroutine(UpdatePositionAfterDelay(moveDistance));
            previousHeight = colliderTopY; // Update previous height
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isInCollider = false;
    }

    private IEnumerator UpdatePositionAfterDelay(float moveDistance)
    {
        yield return new WaitForSeconds(1.5f); // Delay before moving

        if (isInCollider)
        {
            float elapsedTime = 0f;
            float duration = 1.5f;

            Vector3 startBlockPos = blockHolder.position;
            Vector3 targetBlockPos = startBlockPos + new Vector3(0, 1.0f, 0);

            // Ensure blockHolder doesn't go above maxBlockHeight
            if (targetBlockPos.y > maxBlockHeight)
            {
                targetBlockPos.y = maxBlockHeight;
            }

            Vector3 startObjectPos = transform.position;
            Vector3 targetObjectPos = startObjectPos + new Vector3(0, moveDistance, 0);

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                blockHolder.position = Vector3.Lerp(startBlockPos, targetBlockPos, t);
                transform.position = Vector3.Lerp(startObjectPos, targetObjectPos, t);

                elapsedTime += Time.deltaTime * speed;
                yield return null;
            }

            blockHolder.position = targetBlockPos;
            transform.position = targetObjectPos;
        }
    }
}
