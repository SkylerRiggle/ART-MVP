using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private List<Equipment> loadout = new List<Equipment>();

    private Equipment currentEquipment;
}
