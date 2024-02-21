using UnityEngine;

[CreateAssetMenu(fileName = "Stamina Potion", menuName = "Items/Stamina Potion")]

public class StaminaPotion : Item
{
    [SerializeField]
    private int staminaRegen;

    public int MyStaminaRegen
    {
        get
        {
            return staminaRegen;
        }
    }
}
