using UnityEngine;

public class ObjectTransformModel:MonoBehaviour
{
    public Vector3 Rotation { get; private set; }
    public Vector3 Scale { get; private set; }

    public ObjectTransformModel(Vector3 initialScale)
    {
        Scale = initialScale;
        Rotation = Vector3.zero;
    }

    public void Rotate(Vector3 deltaRotation)
    {
        Rotation += deltaRotation;
        // Keep angles between 0-360 for cleanliness
        Rotation = new Vector3(
            Rotation.x % 360f,
            Rotation.y % 360f,
            Rotation.z % 360f
        );
    }

    public void ScaleObject(float scaleFactor)
    {
        float newScale = Mathf.Clamp(Scale.x + scaleFactor, 0.5f, 2f);
        Scale = new Vector3(newScale, newScale, newScale);
    }

    public void Reset()
    {
        Rotation = Vector3.zero;
        Scale = Vector3.one;
    }
}