using UnityEngine;

[RequireComponent(typeof(ObjectView))]
public class TouchInputController : MonoBehaviour
{
    public float rotationSpeed = 0.2f;
    public float scaleSpeed = 0.01f;

    [SerializeField]private ObjectTransformModel model;
    private ObjectView view;

    private float initialPinchDistance;
    [SerializeField] private Vector3 initialScale;

    void Start()
    {
        view = GetComponent<ObjectView>();
        model = new ObjectTransformModel(transform.localScale);
    }

    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float deltaRotation = -touch.deltaPosition.x * rotationSpeed;
                model.Rotate(deltaRotation);
                view.UpdateRotation(model.RotationY);
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            if (t0.phase == TouchPhase.Began || t1.phase == TouchPhase.Began)
            {
                initialPinchDistance = Vector2.Distance(t0.position, t1.position);
                initialScale = model.Scale;
            }
            else if (t0.phase == TouchPhase.Moved || t1.phase == TouchPhase.Moved)
            {
                float currentDistance = Vector2.Distance(t0.position, t1.position);
                float scaleFactor = (currentDistance - initialPinchDistance) * scaleSpeed;
                model.ScaleObject(scaleFactor);
                view.UpdateScale(model.Scale);
            }
        }
    }
}

