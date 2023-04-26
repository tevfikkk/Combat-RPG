using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockbackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private int currentHealth;
    private bool canTakeDamage = true;

    private Knockback knockback;
    private Flash flash;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        flash = GetComponent<Flash>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy)
        {
            // take damage
            TakeDamage(1, other.transform);
        }
    }

    public void TakeDamage(int damage, Transform hitTransform)
    {
        if (!canTakeDamage) { return; }

        knockback.GetKnockedBack(hitTransform, knockbackThrustAmount);
        StartCoroutine(flash.FlashRoutine());

        canTakeDamage = false;
        currentHealth -= damage;
        // print($"Player took {damage} damage. Current health: {currentHealth}");
        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
}
