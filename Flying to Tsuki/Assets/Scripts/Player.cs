using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float PlayerSpeed;
    public Joystick Joystick;
    private Rigidbody2D rb;
    private Vector2 playerDirection;
    private Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        float verticalMove = Joystick.Vertical;
        float horizontalMove = Joystick.Horizontal;
        playerDirection = new Vector2(horizontalMove, verticalMove).normalized;

        animator.SetFloat("Horizontal", horizontalMove);
        animator.SetFloat("Vertical", verticalMove);
        animator.SetFloat("Speed", playerDirection.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(playerDirection.x * PlayerSpeed, playerDirection.y * PlayerSpeed);
    }
}
