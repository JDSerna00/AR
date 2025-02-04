using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    [SerializeField] string url;
    private void OnMouseDown()
    {
        Application.OpenURL(url);
    }
}
