using UnityEngine;

[CreateAssetMenu(fileName = "Health Potion", menuName = "Items/Health Potion")]

public class HealthPotion : Item
{
    [SerializeField]
    private int healthRegen;

    public int MyHealthRegen
    {
        get
        {
            return healthRegen;
        }
    }
}
