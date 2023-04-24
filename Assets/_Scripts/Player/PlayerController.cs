using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    // This is a property. It's a shortcut for a getter and setter.
    public bool FacingLeft
    {
        get => facingLeft;
    }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 3.3f; // dash speed multiplier
    [SerializeField] private TrailRenderer trailRenderer; // trail renderer for dash effect
    [SerializeField] private Transform weaponCollider; // weapon collider for attack

    // Event comes from Input System through Player Controls script
    private PlayerControls playerControls;

    // We'll be using this Vector2 to store our values incoming from our player controls input.
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private float startingMoveSpeed;

    private bool facingLeft = false;
    private bool isDashing = false;

    protected override void Awake()
    {
        base.Awake();

        // Create new instance of Player Controls
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Subscribe to the Attack event
        playerControls.Combat.Dash.performed += _ => Dash();

        startingMoveSpeed = moveSpeed; // store the starting move speed
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

    public Transform GetWeaponCollider() => weaponCollider;

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
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed; // increase the speed to dash speed
            trailRenderer.emitting = true; // enable the trail renderer
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed = startingMoveSpeed; // decrease the speed to normal
        trailRenderer.emitting = false; // disable the trail renderer
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
