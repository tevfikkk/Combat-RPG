using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;

    public void Attack()
    {
        print("Bow attack!");
    }

    public WeaponInfo GetWeaponInfo() => weaponInfo;
}
