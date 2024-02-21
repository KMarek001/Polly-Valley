using System.Collections.Generic;
using UnityEngine;

public class ChestInventoryManager : MonoBehaviour
{
    public static ChestInventoryManager instance;

    [SerializeField]
    private int chestInventorySpace;

    [SerializeField]
    public List<Item> chestItems = new List<Item>();

    public List<InventorySlot> slots = new List<InventorySlot>();

    public GameObject currentChest;

    private void Awake()
    {
        instance = this;
        chestItems.Capacity = chestInventorySpace;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            slots.Add(transform.GetChild(i).GetComponent<InventorySlot>());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.gameStatus == GameManager.GameStatus.ChestOpened)
        {
            currentChest.GetComponent<ChestController>().CloseChest();
        }
    }

    public void AddItem(Item item)
    {
        foreach (InventorySlot slot in slots)
        {
            if (!slot.isEmpty())
            {
                if (slot.AddItem(item))
                    return;
            }
            else
            {
                if (slot.AddItem(item))
                {
                    
                    chestItems.Add(item);
                    return;
                }
            }
        }
    }

    public void RemoveItem(Item item)
    {
        if(item != null)
        {
            chestItems.Remove(item.MySlot.RemoveItem());
        }
    }

    public void ReplaceItem(Item item)
    {
        if (item != null)
            chestItems.Remove(item.MySlot.ReplaceItem());
    }

    public void FillChestInventoryItems(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != null)
            {
                Item clonedItem = Instantiate(items[i]);
                chestItems.Add(clonedItem);
                if (chestItems[i].GetType() == typeof(HealthPotion) || chestItems[i].GetType() == typeof(StaminaPotion))
                {
                    chestItems[i].MyCurrentQuantity = chestItems[i].MyMaxQuantity / 2;
                }
                slots[i].AddItem(chestItems[i]);
            }
        }
    }

    public List<Item> ClearChestInventory()
    {
        List<Item> returnedItems = new List<Item>();

        foreach (Item item in chestItems)
            returnedItems.Add(item);

        foreach(InventorySlot slot in slots)
        {
            if(!slot.isEmpty())
                ReplaceItem(slot.MyItem);
        }

        return returnedItems;
    }
}
