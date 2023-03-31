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

    private void Awake()
    {
        // Create new instance of Player Controls
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
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
        Move();
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
}
