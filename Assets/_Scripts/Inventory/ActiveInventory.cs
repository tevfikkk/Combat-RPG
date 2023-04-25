using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndexNum = 0;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        // It is done with ReadValue as the number pressed is a float.
        // Then we cast it to an int to get the index number of the slot since floating point numbers occur an error.
        playerControls.Inventory.Keyboard.performed += ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());

        ToggleActiveHighlight(0);
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    public void EquipStartingWeapong() => ToggleActiveHighlight(0);

    /// <summary>
    /// Toggles the active inventory slot based on the number pressed on the keyboard.
    /// -1 is subtracted from the number pressed to get the index number of the slot.
    /// </summary>
    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue - 1);
    }

    /// <summary>
    /// Toggles the highlight of the active inventory slot.
    /// </summary>
    private void ToggleActiveHighlight(int indexNum)
    {
        activeSlotIndexNum = indexNum;

        // Deactivates all the highlights of the inventory slots.
        foreach (Transform inventorySlot in transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        // Activates the highlight of the inventory slot.
        transform.GetChild(indexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    /// <summary>
    /// Changes the active weapon.
    /// </summary>
    private void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        Transform childTransform = transform.GetChild(activeSlotIndexNum); // Gets the child transform of the active slot.
        InventorySlot inventorySlot = childTransform.GetComponentInChildren<InventorySlot>(); // Gets the inventory slot component of the child transform.

        WeaponInfo weaponInfo = inventorySlot.GetWeaponInfo(); // Gets the weapon info of the inventory slot.
        GameObject weaponToSpawn = weaponInfo?.weaponPrefab; // Gets the weapon prefab of the weapon info.

        if (weaponInfo == null)
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }

        GameObject newWeapon = Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity); // Spawns the weapon.
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0); // Resets the rotation of the weapon.
        newWeapon.transform.parent = ActiveWeapon.Instance.transform; // Sets the parent of the weapon to the ActiveWeapon instance.

        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>()); // Sets the new weapon as the active weapon.
    }
}
