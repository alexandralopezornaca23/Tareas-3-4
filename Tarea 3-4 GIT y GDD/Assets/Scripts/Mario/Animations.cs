using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Animator animator;
    Collisions collisions;
    Mover mover;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        //collisions = GetComponent<Collisions>();
        //mover = GetComponent<Mover>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    animator.SetBool("Grounded", collisions.Grounded());
    //    animator.SetFloat("Velocity_x", Mathf.Abs(mover.rb2D.velocity.x));
    //    animator.SetBool("Jumping", mover.isJumping);
    //    animator.SetBool("Skid", mover.isSkidding);
    //}

    public void Grounded(bool isGrounded)
    {
        animator.SetBool("Grounded", isGrounded);
    }

    public void Velocity(float velocityX)
    {
        animator.SetFloat("Velocity_x", Mathf.Abs(velocityX));
    }

    public void Jumping(bool isJumping)
    {
        animator.SetBool("Jumping", isJumping);
    }

    public void Skid(bool isSkinding)
    {
        animator.SetBool("Skid", isSkinding);
    }

    public void Dead() 
    {
        animator.SetTrigger("Dead");
    }

    public void NewState(int state)
    {
        animator.SetInteger("State", state);
    }

    public void PowerUp()
    {
        animator.SetTrigger("PowerUp");
    }

    public void Hit()
    {
        animator.SetTrigger("Hit");
    }
}
