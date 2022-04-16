using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    private PlayerManager playerManager;

    [SerializeField] private List<Firearm> weapons = new List<Firearm>();
    private Firearm currentWeapon;
    private int weaponIndex;

    [SerializeField] private float moveDilation = 0.5f;

    private void Awake() => playerManager = GetComponent<PlayerManager>();

    private void Start() => HandleWeaponSwap(0);

    private void Update()
    {
        if (playerManager.fireInput)
        {
            currentWeapon?.Fire();
        } else if (playerManager.reloadInput)
        {
            currentWeapon?.Reload();
        }

        currentWeapon?.SetAim(playerManager.aimInput, 1 + (playerManager.moveInput.magnitude * moveDilation));

        if (playerManager.scrollInput.y != 0)
        {
            HandleWeaponSwap(playerManager.scrollInput.y);
        }
    }

    private void HandleWeaponSwap(float swapDelta)
    {
        weaponIndex += Mathf.RoundToInt(swapDelta);
        weaponIndex = weaponIndex % weapons.Count;

        currentWeapon?.gameObject.SetActive(false);
        currentWeapon = weapons[weaponIndex];
        currentWeapon?.gameObject.SetActive(true);
        CursorManager.instance.SetCursor(currentWeapon?.cursor);
    }
}