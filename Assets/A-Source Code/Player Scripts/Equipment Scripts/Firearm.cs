using UnityEngine;

public abstract class Firearm : Equipment
{
    [SerializeField] private int clipSize;
    private int totalAmmo;
    private int clipAmmo;
}
