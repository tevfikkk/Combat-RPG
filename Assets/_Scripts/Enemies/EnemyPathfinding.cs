using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback; // its purpose is to get the gettingKnockedBack bool from the Knockback script.
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        // If the enemy is getting knocked back, then don't move.
        if (knockback.GettingKnockedBack)
        {
            return;
        }

        // Move the enemy by overriding the velocity. However, if the enemy is getting knocked back, then don't move.
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));

        if (moveDir.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    /// <summary>
    /// Moves the enemy to the target position
    /// </summary>
    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }

    public void StopMoving()
    {
        moveDir = Vector2.zero;
    }
}
