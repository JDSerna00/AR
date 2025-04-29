using UnityEngine;

public class ObjectTransformModel:MonoBehaviour
{
    public float RotationY { get; private set; }
    public Vector3 Scale { get; private set; }

    private float minScale = 0.3f;
    private float maxScale = 3.0f;

    public ObjectTransformModel(Vector3 initialScale)
    {
        Scale = initialScale;
        RotationY = 0f;
    }

    public void Rotate(float delta)
    {
        RotationY += delta;
    }

    public void ScaleObject(float scaleFactor)
    {
        Vector3 newScale = Scale + Vector3.one * scaleFactor;
        newScale = Vector3.Max(Vector3.one * minScale, Vector3.Min(Vector3.one * maxScale, newScale));
        Scale = newScale;
    }
}
