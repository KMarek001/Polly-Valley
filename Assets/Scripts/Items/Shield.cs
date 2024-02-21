using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "Items/Shield")]

public class Shield : Item
{
    [SerializeField]
    private int protection;

    public int Protection { get => protection; set => protection = value; }
}