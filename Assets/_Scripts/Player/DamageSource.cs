using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private int damageAmount; // The amount of damage the player does to enemies.

    private void Start()
    {
        MonoBehaviour currentWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        damageAmount = (currentWeapon as IWeapon).GetWeaponInfo().weaponDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        enemyHealth?.TakeDamage(damageAmount);
    }
}
