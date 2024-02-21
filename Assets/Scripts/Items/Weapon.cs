using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]

public class Weapon : Item
{
    [SerializeField]
    private int damage;

    public int Damage { get => damage; set => damage = value; }
}