using UnityEngine;

public class ObjectView : MonoBehaviour
{
    public void UpdateRotation(Vector3 eulerAngles)
    {
        transform.localEulerAngles = eulerAngles;
    }

    public void UpdateScale(Vector3 scale)
    {
        transform.localScale = scale;
    }
}

