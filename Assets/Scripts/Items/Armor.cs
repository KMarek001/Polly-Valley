using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor")]

public class Armor : Item
{
    [SerializeField]
    private int protection;

    public int Protection { get => protection; set => protection = value; }
}