using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    private Vector2 moveDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    /// <summary>
    /// Moves the enemy to the target position
    /// </summary>
    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }
}
