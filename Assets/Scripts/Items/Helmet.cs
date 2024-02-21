using UnityEngine;

[CreateAssetMenu(fileName = "Helmet", menuName = "Items/Helmet")]

public class Helmet : Item
{
    [SerializeField]
    private int protection;

    public int Protection { get => protection; set => protection = value; }
}