using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;

    private WeaponInfo weaponInfo;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveProjectile();
        DetectFireDistance();
    }

    /// <summary>
    /// This method is to update the weapon info.
    /// </summary>
    public void UpdateWeaponInfo(WeaponInfo weaponInfo) => this.weaponInfo = weaponInfo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // If the projectile hits the environment objects, it will not be destroyed.
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();

        // check if the projectile hits the enemy or the environment objects.
        // Trees have is trigger in their base (stamps). we want to have projectiles collide with tree's base.
        if (!other.isTrigger && (indestructible || enemyHealth))
        {
            enemyHealth?.TakeDamage(1);
            Instantiate(particleOnHitPrefabVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// This method is to detect the distance between the projectile and the weapon.
    /// </summary>
    private void DetectFireDistance()
    {
        // if the projectile exceeds the weapon range, it will be destroyed.
        if (Vector3.Distance(transform.position, startPosition) > weaponInfo.weaponRange)
            Destroy(gameObject, 0.1f);

        print($"Distance: {Vector3.Distance(transform.position, startPosition)}");
        print($"Range: {weaponInfo.weaponRange}");
    }

    /// <summary>
    /// This method is to move the projectile.
    /// </summary>
    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
