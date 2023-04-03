using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool GettingKnockedBack { get; private set; } // Whether the enemy is getting knocked back or not.

    [SerializeField] private float knockBackTime = .2f; // How long the enemy will be knocked back for.

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Gets the enemy knocked back.
    /// damageSource is the transform of the object that is damaging the enemy.
    /// knockbackThrust is the amount of force that will be applied to the enemy.
    /// </summary>
    public void GetKnockedBack(Transform damageSource, float knockbackThrust)
    {
        GettingKnockedBack = true; // Set the gettingKnockedBack bool to true.
        Vector2 difference = (transform.position - damageSource.position).normalized * knockbackThrust * rb.mass; // Get the difference between the enemy and the damage source, normalize it, multiply it by the knockback thrust and the enemy's mass.
        rb.AddForce(difference, ForceMode2D.Impulse); // Add force to the enemy.
        StartCoroutine(KnockRoutine()); // Start the knockback routine.
    }

    /// <summary>
    /// The knockback routine.
    /// Waits for the knockback time and then sets the velocity to zero.
    ///  </summary>
    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime); // Wait for the knockback time.
        rb.velocity = Vector2.zero; // Set the velocity to zero.
        GettingKnockedBack = false; // Set the gettingKnockedBack bool to false.
    }
}
