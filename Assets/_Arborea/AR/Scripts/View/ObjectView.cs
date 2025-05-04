using UnityEngine;

public class ObjectView : MonoBehaviour
{
    public void UpdateRotation(float rotationY)
    {
        transform.localEulerAngles = new Vector3(0, rotationY, 0);
    }

    public void UpdateScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
}

