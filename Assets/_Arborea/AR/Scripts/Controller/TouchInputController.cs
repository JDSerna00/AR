using UnityEngine;

public class TouchInputController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.2f;
    [SerializeField] private float scaleSpeed = 0.01f;

    private ObjectTransformModel _model;
    private ObjectView _view;
    private float _initialPinchDistance;

    public void Initialize(ObjectTransformModel model, ObjectView view)
    {
        _model = model;
        _view = view;
    }

    private void Update()
    {
        if (Input.touchCount == 1) HandleRotation();
        else if (Input.touchCount == 2) HandleScaling();
    }

    private void HandleRotation()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Moved)
        {
            _model.Rotate(-touch.deltaPosition.x * rotationSpeed);
            _view.UpdateRotation(_model.RotationY);
        }
    }

    private void HandleScaling()
    {
        Touch t1 = Input.GetTouch(0);
        Touch t2 = Input.GetTouch(1);

        if (t1.phase == TouchPhase.Began || t2.phase == TouchPhase.Began)
        {
            _initialPinchDistance = Vector2.Distance(t1.position, t2.position);
        }
        else if (t1.phase == TouchPhase.Moved || t2.phase == TouchPhase.Moved)
        {
            float currentDistance = Vector2.Distance(t1.position, t2.position);
            float scaleFactor = (currentDistance - _initialPinchDistance) * scaleSpeed;

            _model.ScaleObject(scaleFactor);
            _view.UpdateScale(_model.Scale);

            _initialPinchDistance = currentDistance;
        }
    }

    public void ResetModel()
    {
        _model.Reset();
        _view.UpdateRotation(_model.RotationY);
        _view.UpdateScale(_model.Scale);
    }
}