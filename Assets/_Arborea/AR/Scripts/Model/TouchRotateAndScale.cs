using UnityEngine;

public class TouchRotateAndScale : MonoBehaviour
{
    public float rotationSpeed = 0.2f;
    public float scaleSpeed = 0.01f;
    public float minScale = 0.3f;
    public float maxScale = 3.0f;

    private Vector2 lastTouchPos;
    private float initialPinchDistance;
    private Vector3 initialScale;

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float rotationAmount = touch.deltaPosition.x * rotationSpeed;
                transform.Rotate(0, -rotationAmount, 0, Space.World);
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            // Check pinch movement
            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                initialPinchDistance = Vector2.Distance(touch0.position, touch1.position);
                initialScale = transform.localScale;
            }
            else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
            {
                float currentPinchDistance = Vector2.Distance(touch0.position, touch1.position);
                float scaleFactor = (currentPinchDistance - initialPinchDistance) * scaleSpeed;
                Vector3 newScale = initialScale + Vector3.one * scaleFactor;

                // Clamp scale
                newScale = Vector3.Max(Vector3.one * minScale, Vector3.Min(Vector3.one * maxScale, newScale));
                transform.localScale = newScale;
            }
        }
    }
}
