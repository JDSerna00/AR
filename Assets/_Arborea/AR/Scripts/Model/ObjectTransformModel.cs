using UnityEngine;

public class ObjectTransformModel:MonoBehaviour
{
    public float RotationY { get; private set; }
    public Vector3 Scale { get; private set; }

    public ObjectTransformModel(Vector3 initialScale)
    {
        Scale = initialScale;
    }

    public void Rotate(float delta) => RotationY += delta;

    public void ScaleObject(float scaleFactor)
    {
        Scale += Vector3.one * scaleFactor;
        Scale = Vector3.Max(Vector3.one * 0.5f, Vector3.Min(Vector3.one * 2f, Scale));
    }

    public void Reset()
    {
        RotationY = 0f;
        Scale = Vector3.one;
    }
}