using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3; // The amount of health the enemy starts with.

    private int currentHealth; // The current health the enemy has.

    private void Start()
    {
        currentHealth = startingHealth; // Sets the current health to the starting health at the start of the game.
    }

    /// <summary>
    /// Reduces the enemy's health by the amount of damage taken.
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        print(currentHealth);
        DetectDeath();
    }

    /// <summary>
    /// Checks if the enemy's health is 0 or less.
    /// </summary>
    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
        }
    }
}
