using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3; // The amount of health the enemy starts with.
    [SerializeField] private GameObject deathVFXPrefab; // The death VFX prefab.
    [SerializeField] private float knockbackThrust = 10f; // The knockback thrust.

    private int currentHealth; // The current health the enemy has.
    private Knockback knockback; // The knockback component of the enemy.
    private Flash flash; // The flash component of the enemy.

    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

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
        knockback.GetKnockedBack(PlayerController.Instance.transform, knockbackThrust); // 10f is the knockback thrust. Magic numbers are bad, it will be changed later.
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }

    /// <summary>
    /// Wait for the flash to finish and then invoke the DetectDeath method.
    /// </summary>
    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreDefaultMatTime()); // Wait for the flash to finish.
        DetectDeath(); // invoke the DetectDeath method.
    }

    /// <summary>
    /// Checks if the enemy's health is 0 or less.
    /// </summary>
    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity); // Instantiate the death VFX prefab.
            Destroy(gameObject);
        }
    }
}
