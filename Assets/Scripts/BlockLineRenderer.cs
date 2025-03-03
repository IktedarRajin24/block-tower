using UnityEngine;

public class BlockToViewportLine : MonoBehaviour
{
    public Camera mainCamera;
    public float lineWidthMultiplier = 1f;
    public float opacity = 0.5f; // Opacity value (0.0 to 1.0)
    private LineRenderer lineRenderer;

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Main camera not found. Assign a camera in the Inspector.");
                return;
            }
        }

        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        lineRenderer.positionCount = 2;

        // Set initial opacity
        UpdateLineColor();
    }

    void Update()
    {
        if (mainCamera == null || lineRenderer == null) return;

        // Get the bottom center of the block
        Vector3 blockBottom = GetComponentInParent<SpriteRenderer>().bounds.min;
        blockBottom.x = transform.position.x;
        blockBottom.z = transform.position.z;

        // Get the viewport bottom center (Y-axis)
        Vector3 viewportBottom = new Vector3(0.5f, 0f, mainCamera.nearClipPlane + 1f); // Change 1f to 0f
        Vector3 worldBottom = mainCamera.ViewportToWorldPoint(viewportBottom);

        // Set the line renderer positions
        lineRenderer.SetPosition(0, blockBottom);
        lineRenderer.SetPosition(1, new Vector3(transform.position.x, worldBottom.y, transform.position.z)); // use world bottom

        // Dynamically set line width based on block's scale
        float blockWidth = GetComponentInParent<SpriteRenderer>().bounds.size.x;
        float dynamicLineWidth = blockWidth * lineWidthMultiplier;
        lineRenderer.startWidth = dynamicLineWidth;
        lineRenderer.endWidth = dynamicLineWidth;

        // Update line color (for opacity)
        UpdateLineColor();
    }

    void UpdateLineColor()
    {
        Debug.Log(lineRenderer.material.color);
        Color color = lineRenderer.material.color;
        color.a = opacity;
        lineRenderer.material.color = color;
    }
}