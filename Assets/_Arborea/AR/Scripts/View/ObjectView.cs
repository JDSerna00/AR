using UnityEngine;

public class ObjectView : MonoBehaviour
{
    public void UpdateRotation(float rotationY)
    {
        transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    public void UpdateScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
}

