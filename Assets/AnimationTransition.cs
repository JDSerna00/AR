using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTransition : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        animator.SetTrigger("Clicked");
    }
}
