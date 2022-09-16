using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationController : MonoBehaviour
{
    Animator animator;
    int velocityHash;
    float speed;
    void Start()
    {
        animator = GetComponent<Animator>();
        velocityHash = Animator.StringToHash("Blend");
    }
    private void Update()
    {
        animator.SetFloat(velocityHash, speed);
    }
    public void SetAnimationSpeed(float speedVal)
    {
       speed = speedVal;
    }
}
