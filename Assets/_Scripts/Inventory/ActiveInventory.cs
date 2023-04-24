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
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

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
    }
}
