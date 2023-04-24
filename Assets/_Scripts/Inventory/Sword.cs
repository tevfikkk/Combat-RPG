using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform weaponCollier;
    [SerializeField] private float swordAttackCD = 0.5f; // attack cool down seconds

    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAnim;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    /// <summary>
    /// Attack method
    /// </summary>
    public void Attack()
    {

        // isAttacking = true;

        // fire our sword animation
        myAnimator.SetTrigger("Attack");

        // activate weapon collider to detect enemy
        weaponCollier.gameObject.SetActive(true);

        // instantiate slash animation
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent; // set slash animation parent to player

        // Cool down for attack
        StartCoroutine(AttackCDRoutine());

    }

    /// <summary>
    /// Attack cool down
    /// </summary>
    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
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

        if (playerController.FacingLeft)
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

        if (playerController.FacingLeft)
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
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        // angle with arc tangent of mouse position
        // this is to get the angle of the mouse position at z axis
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollier.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollier.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
