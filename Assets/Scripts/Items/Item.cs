using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class Item : ScriptableObject
{
    [SerializeField]
    private int level;

    [SerializeField]
    private string itemName;

    [SerializeField]
    private Sprite itemIcon;

    [SerializeField]
    private GameObject itemObject;

    [SerializeField]
    private int currentQuantity;

    [SerializeField]
    private int maxQuantity;

    public InventorySlot slot;

    public Sprite MyIcon
    {
        get
        {
            return itemIcon;
        }
        set
        {
            itemIcon = value;
        }
    }

    public int MyCurrentQuantity
    {
        get 
        {
            return currentQuantity; 
        }
        set
        {
            currentQuantity = value;
        }
    }

    public int MyMaxQuantity
    {
        get
        {
            return maxQuantity;
        }
    }

    public InventorySlot MySlot
    {
        get
        {
            return slot;
        }

        set 
        { 
            slot = value;
        }
    }

    public string MyName
    {
        get 
        {
            return itemName;
        }
    }

    public int MyLevel 
    {
        get
        {
            return level;
        }
    }
}