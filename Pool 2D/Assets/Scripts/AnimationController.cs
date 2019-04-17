using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator Animator;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator.SetFloat("IsMoving", rb.velocity.magnitude); //rb.velocity.magnitude;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Animator.SetFloat("IsMoving", rb.velocity.magnitude);
    }
}
