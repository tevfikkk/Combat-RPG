using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject magicLaserPrefab;
    [SerializeField] private Transform magicLaserSpawnPoint;

    private Animator myAnim;
    readonly int FIRE_HASH = Animator.StringToHash("Fire");

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
        myAnim.SetTrigger(FIRE_HASH);
    }

    public void SpawnStaffProjectileAnimEvent()
    {
        GameObject newMagic = Instantiate(magicLaserPrefab, magicLaserSpawnPoint.position, Quaternion.identity);
        newMagic.GetComponent<MagicLaser>().UpdateLaserRange(weaponInfo.weaponRange);
    }

    public WeaponInfo GetWeaponInfo() => weaponInfo;

    /// <summary>
    /// Sword follows the mouse position
    /// and rotates based on the mouse position
    /// </summary>
    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        // angle with arc tangent of mouse position
        // this is to get the angle of the mouse position at z axis
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
