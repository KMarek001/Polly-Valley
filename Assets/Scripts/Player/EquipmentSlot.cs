using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : InventorySlot, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private EquipmentManager equipmentParent;

    public enum EquipmentSlotTypes
    {
        Helmet,
        Armor,
        Sword,
        Shield
    }

    [SerializeField]
    private Sprite emptyIcon;

    [SerializeField]
    private EquipmentSlotTypes equipmentType;

    public EquipmentSlotTypes MyEquipmentType
    { 
        get => equipmentType;
    }

    void Start()
    {
        equipmentParent = GetComponentInParent<EquipmentManager>();

        if (item != null)
        {
            icon.sprite = item.MyIcon;
            icon.color = Color.white;
        }
        else
        {
            icon.sprite = emptyIcon;
            icon.color = Color.white;
        }
    }

    public bool EquipItem(Item item)
    {
        if (isEmpty())
        {
            switch(this.equipmentType)
            {
                case EquipmentSlotTypes.Helmet:
                {
                    if(item.GetType() == typeof(Helmet)) 
                    {
                            MyItem = item;
                            MyItem.MySlot = this;

                            icon.sprite = item.MyIcon;
                            icon.color = Color.white;

                            return true;
                        }
                    break;
                }
                case EquipmentSlotTypes.Armor:
                {
                    if (item.GetType() == typeof(Armor))
                    {
                        MyItem = item;
                        MyItem.MySlot = this;

                        icon.sprite = item.MyIcon;
                        icon.color = Color.white;

                        return true;
                    }
                    break;
                }
                case EquipmentSlotTypes.Sword:
                {
                    if (item.GetType() == typeof(Weapon))
                    {
                        MyItem = item;
                        MyItem.MySlot = this;

                        icon.sprite = item.MyIcon;
                        icon.color = Color.white;

                        return true;
                    }
                    break;
                }
                case EquipmentSlotTypes.Shield:
                {
                    if (item.GetType() == typeof(Shield))
                    {
                        MyItem = item;
                        MyItem.MySlot = this;

                        icon.sprite = item.MyIcon;
                        icon.color = Color.white;

                        return true;
                    }
                    break;
                }
                default:
                {
                    return false;
                }
            }
        }
        return false;
    }

    public Item UnequipItem()
    {
        Item unequippedItem = null;
        if (!isEmpty())
        {
            unequippedItem = MyItem;
            MyItem = null;
            icon.sprite = emptyIcon;

            return unequippedItem;
        }
        else
        {
            Debug.Log("Nie uda³o siê zdj¹æ przedmiotu");
            return unequippedItem;
        }
    }



    public new void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!isEmpty())
            {
                //Przenoszenie z slotu do reki
                if (HandScript.instance.isHandEmpty)
                {
                    HandScript.instance.MyItem = MyItem;
                    equipmentParent.Unequip(this);
                    UnequipItem();

                    HandScript.instance.MyMovingIcon.sprite = HandScript.instance.MyItem.MyIcon;
                    HandScript.instance.MyMovingIcon.color = Color.white;
                    HandScript.instance.isHandEmpty = false;
                }
                else
                {
                    if (EquipItem(HandScript.instance.MyItem))
                    {
                        HandScript.instance.MyItem = null;
                        HandScript.instance.MyMovingIcon.sprite = null;
                        HandScript.instance.MyMovingIcon.color = new Color(0, 0, 0, 0);
                        HandScript.instance.isHandEmpty = true;
                    }
                }
            }
            else
            {
                //Przenoszenie z reki do slotu
                if (!HandScript.instance.isHandEmpty)
                {
                    if(EquipItem(HandScript.instance.MyItem))
                    {
                        equipmentParent.Equip(HandScript.instance.MyItem, this);
                        HandScript.instance.MyItem = null;
                        HandScript.instance.MyMovingIcon.sprite = null;
                        HandScript.instance.MyMovingIcon.color = new Color(0, 0, 0, 0);
                        HandScript.instance.isHandEmpty = true;
                    }
                }
            }
        }
    }

}
