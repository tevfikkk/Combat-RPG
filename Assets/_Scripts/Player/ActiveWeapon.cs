using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }

    private PlayerControls playerControls;
    private bool attackButtonDown, isAttacking = false; // isAttacking is used to prevent spamming attack


    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        Attack();
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    public void ToggleIsAttacking(bool value)
    {
        isAttacking = value;
    }

    /// <summary>
    /// Start attacking when the button is pressed down and the player is alive
    /// This is an event from PlayerControls script
    /// </summary>
    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    /// <summary>
    /// Stop attacking when the button is released or the player is dead
    /// This is an event from PlayerControls script
    /// </summary>
    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    /// <summary>
    /// Attack method
    /// </summary>
    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            isAttacking = true;

            // Invoke the attack method from the current active weapon as IWeapon
            (CurrentActiveWeapon as IWeapon).Attack();
        }
    }
}
