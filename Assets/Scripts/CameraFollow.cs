using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;  // The object the camera follows
    public float followSpeed = 2f;  // Speed of following
    public float deathZoneY = 10f;  // The Y position where zooming starts
    public float zoomSpeed = 2f;  // Speed of zooming out
    public float maxZoomOut = 3f;  // Maximum zoom-out size
    public float minZoom = 1f;  // Minimum zoom level

    private Camera cam;
    private bool shouldZoomOut = false;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        float halfHeight = cam.orthographicSize;  // Half of camera height
        float cameraBottomY = transform.position.y - halfHeight;  // Camera's bottom position

        // Camera follows the target until the bottom of the camera reaches the death zone
        if (cameraBottomY < deathZoneY)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
        else
        {
            shouldZoomOut = true;
        }

        // Start zooming out when the camera bottom hits the death zone
        if (shouldZoomOut)
        {
            float zoomAmount = Mathf.Lerp(cam.orthographicSize, maxZoomOut, zoomSpeed * Time.deltaTime);
            cam.orthographicSize = Mathf.Clamp(zoomAmount, minZoom, maxZoomOut);
        }
    }
}
