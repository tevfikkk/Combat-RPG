using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private float swordAttackCD = 0.5f; // attack cool down seconds
    [SerializeField] private WeaponInfo weaponInfo;

    private Animator myAnimator;
    private Transform weaponCollier;
    private GameObject slashAnim;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public WeaponInfo GetWeaponInfo() => weaponInfo;

    private void Start()
    {
        weaponCollier = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashAnimationSpawnPoint").transform;
    }

    /// <summary>
    /// Attack method
    /// </summary>
    public void Attack()
    {
        // fire our sword animation
        myAnimator.SetTrigger("Attack");

        // activate weapon collider to detect enemy
        weaponCollier.gameObject.SetActive(true);

        // instantiate slash animation
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent; // set slash animation parent to player
    }

    /// <summary>
    /// This is done with putting this method in the animation event
    /// </summary>
    public void DoneAttackingAnimEvent()
    {
        weaponCollier.gameObject.SetActive(false);
    }

    /// <summary>
    /// Flip the slash animation when the sword is swing up
    /// This is done with putting this method in the animation event
    /// </summary>
    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    /// <summary>
    /// Flip the slash animation when the sword is swing down
    /// This is done with putting this method in the animation event
    /// </summary>
    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

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
            weaponCollier.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollier.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
