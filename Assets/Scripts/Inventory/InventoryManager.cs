using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerProperties playerProperites;

    [SerializeField]
    private int inventorySpace;

    [SerializeField]
    public List<Item> items = new List<Item>();

    public List<InventorySlot> slots = new List<InventorySlot>();

    private void Start()
    {
        playerController = PlayerController.instance;
        playerProperites = PlayerProperties.instance;
        items.Capacity = inventorySpace;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i).GetComponent<InventorySlot>());
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                Item clonedItem = Instantiate(items[i]);
                items[i] = clonedItem;
                if (items[i].GetType() == typeof(HealthPotion) || items[i].GetType() == typeof(StaminaPotion))
                {
                    items[i].MyCurrentQuantity = items[i].MyMaxQuantity / 2;
                }
                slots[i].AddItem(items[i]);
            }
        }

    }

    public void AddItem(Item item)
    {
        foreach(InventorySlot slot in slots)
        {
            if (!slot.isEmpty())
            {
                if(slot.AddItem(item))
                    return;
            }
            else
            {
                if (slot.AddItem(item))
                {
                    items.Add(item);
                    return;
                }
            }
        }
    }

    public void RemoveItem(Item item)
    {
        if (item != null)
            items.Remove(item.MySlot.RemoveItem());
    }

    public void ReplaceItem(Item item)
    {
        if (item != null)
        {
            items.Remove(item.MySlot.ReplaceItem());
        }
    }

    public void UseHealthPotion()
    {
        HealthPotion selectedItem;
        int healthRegenerated = 0;

        if(playerProperites.CurrentPlayerHealth < playerProperites.MaxPlayerHealth && playerProperites.isActiveAndEnabled)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.MyItem != null && slot.MyItem.GetType() == typeof(HealthPotion))
                {
                    selectedItem = slot.MyItem as HealthPotion;
                    healthRegenerated = selectedItem.MyHealthRegen;
                    items.Remove(slot.RemoveItem());
                    playerController.RegenerateHealth(healthRegenerated);
                    Debug.Log("Odnowiono " + healthRegenerated + " punktów ¿ycia");
                    break;
                }
            }
            if (healthRegenerated == 0)
                Debug.Log("W ekwipunku nie ma mikstur regeneracji ¿ycia");
        }
    }

    public void UseStaminaPotion()
    {
        StaminaPotion selectedItem;
        int staminaRegenerated = 0;

        if (playerProperites.CurrentPlayerStamina < playerProperites.MaxPlayerStamina && playerProperites.isActiveAndEnabled)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.MyItem != null && slot.MyItem.GetType() == typeof(StaminaPotion))
                {
                    selectedItem = slot.MyItem as StaminaPotion;
                    staminaRegenerated = selectedItem.MyStaminaRegen;
                    items.Remove(slot.RemoveItem());
                    playerController.RegenerateStamina(staminaRegenerated);
                    Debug.Log("Odnowiono " + staminaRegenerated + " punktów staminy");
                    break;
                }
            }
            if (staminaRegenerated == 0)
                Debug.Log("W ekwipunku nie ma mikstur regeneracji staminy");
        }
    }
}
