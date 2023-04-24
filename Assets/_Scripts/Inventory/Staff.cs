using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    public void Attack()
    {
        print("Staff attack!");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
