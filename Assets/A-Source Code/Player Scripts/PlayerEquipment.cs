using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    private PlayerManager playerManager;

    [SerializeField] private List<Equipment> equipment = new List<Equipment>();
    private Equipment currentEquipment;
    private int selectionIndex = 0;

    private void Awake() => playerManager = GetComponent<PlayerManager>();

    private void Start() => SelectWeapon();

    private void Update()
    {
        if (playerManager.fireInput)
        {
            currentEquipment?.Fire();
        } else if (playerManager.aimInput)
        {
            currentEquipment?.Aim();
        } else if (playerManager.reloadInput)
        {
            currentEquipment?.Reload();
        }

        HandleWeaponChange(Input.mouseScrollDelta.y);
    }

    private void HandleWeaponChange(float scrollInput)
    {
        if (scrollInput < 0.1f && scrollInput > -0.1f) { return; }
        selectionIndex += (int)Mathf.Sign(scrollInput);
        SelectWeapon();
    }

    private void SelectWeapon()
    {
        if (equipment.Count <= 0) { return; }
        selectionIndex = (equipment.Count + selectionIndex) % equipment.Count;

        currentEquipment = equipment[selectionIndex];
        CursorManager.instance.SetCursor(currentEquipment.cursor);
    }
}
