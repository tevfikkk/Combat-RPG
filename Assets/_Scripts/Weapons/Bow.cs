using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    private Animator myAnim;

    // Animator hashes are faster than strings
    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    public void Attack()
    {
        myAnim.SetTrigger(FIRE_HASH);
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);
        // Update the weapon info of the projectile.
        newArrow.GetComponent<Projectile>().UpdateProjectileRange(weaponInfo.weaponRange);
    }

    public WeaponInfo GetWeaponInfo() => weaponInfo;
}
