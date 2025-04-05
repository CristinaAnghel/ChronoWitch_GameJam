using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    public float movSpeed = 5f;

    float speedX, speedY;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        //speedX = Input.GetAxis("Horizontal") * movSpeed;
        //speedY = Input.GetAxis("Vertical") * movSpeed;
        //rb.linearVelocity = new Vector2(speedX, speedY);

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    // FixedUpdate is called every fixed framerate frame, if the MonoBehaviour is enabled
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movSpeed * Time.fixedDeltaTime);
    }

}
