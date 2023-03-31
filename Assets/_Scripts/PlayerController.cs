using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    // Event comes from Input System through Player Controls script
    private PlayerControls playerControls;

    // We'll be using this Vector2 to store our values incoming from our player controls input.
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        // Create new instance of Player Controls
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // playerControls must be enabled in order to receive input
    // we don't have to disable since we are using the same instance
    private void OnEnable() => playerControls.Enable();

    // Funfact: Update is good for player input!
    private void Update()
    {
        PlayerInput();
    }

    // Funfact: FixedUpdate is good for Physics!
    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    /// <summary>
    /// Reads player input from PlayerControls script
    /// movement reads only 1 and -1 for x and y
    /// the speed is controlled by moveSpeed ın the Move() method
    /// </summary>
    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        myAnimator.SetFloat("moveX", movement.x);
        myAnimator.SetFloat("moveY", movement.y);
    }

    /// <summary>
    /// Moves the player based on the movement Vector2
    /// </summary>
    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// Adjusts the player facing direction based on the mouse position
    /// </summary>
    private void AdjustPlayerFacingDirection()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mySpriteRenderer.flipX = (mousePos - rb.position).x < 0;
    }
}
