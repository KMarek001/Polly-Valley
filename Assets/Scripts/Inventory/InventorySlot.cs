using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isChestSlot = false;

    private GameObject parentObject;
    private InventoryManager inventoryParent;
    private ChestInventoryManager chestInventoryParent;

    [SerializeField]
    private int slotId;

    [SerializeField]
    protected Item item;
    
    [SerializeField]
    protected Image icon;

    [SerializeField]
    private TextMeshProUGUI countText;

    [SerializeField]
    protected GameObject itemProperties;

    [SerializeField]
    protected TextMeshProUGUI levelValue;

    [SerializeField]
    protected TextMeshProUGUI propertyLabel;

    [SerializeField]
    protected TextMeshProUGUI propertyValue;

    public Item MyItem
    {
        get
        {
            return item;
        }
        set
        {
            item = value;
        }
    }

    void Start()
    {
        parentObject = transform.parent.gameObject;
        if(!parentObject.TryGetComponent(out inventoryParent))
        {
            chestInventoryParent = parentObject.GetComponent<ChestInventoryManager>();
            isChestSlot = true;
        }
        
    }

    private void UpdateCountText()
    {
        if (MyItem != null && MyItem.MyCurrentQuantity > 1)
        {
            countText.text = MyItem.MyCurrentQuantity.ToString();
            countText.color = Color.white;
            return;
        }

        countText.text = string.Empty;
        countText.color = new Color(0, 0, 0, 0);
    }

    public bool AddItem(Item item)
    {
        if (isEmpty())
        {
            MyItem = item;
            MyItem.MySlot = this;
            icon.sprite = item.MyIcon;
            icon.color = Color.white;

            if(MyItem.MyCurrentQuantity == 0)
                MyItem.MyCurrentQuantity = 1;
            else
                UpdateCountText();

            return true;
        }
        else if(item.name == MyItem.name && (MyItem.MyCurrentQuantity + item.MyCurrentQuantity) < MyItem.MyMaxQuantity)
        {
            if (item.MyCurrentQuantity > 0)
                MyItem.MyCurrentQuantity += item.MyCurrentQuantity;
            else
                MyItem.MyCurrentQuantity++;
            UpdateCountText();
            return true;
        }

        return false;
    }

    public Item RemoveItem() 
    {
        Item removedItem = null;
        if(!isEmpty()) 
        {
            removedItem = MyItem;

            if(MyItem.MyCurrentQuantity > 1)
                MyItem.MyCurrentQuantity--;
            else
            {

                MyItem = null;
                icon.sprite = null;
                icon.color = new Color(0, 0, 0, 0);
            }

            UpdateCountText();

            return removedItem;
        }
        else
        {
            Debug.Log("Nie uda³o siê usun¹æ przedmiotu " + slotId);
            return removedItem;
        }
    }

    public Item ReplaceItem()
    {
        Item replacedItem = null;
        if (!isEmpty())
        {
            replacedItem = MyItem;
            MyItem = null;
            icon.sprite = null;
            icon.color = new Color(0, 0, 0, 0);

            UpdateCountText();

            return replacedItem;
        }
        else
        {
            Debug.Log("Nie uda³o siê przenieœæ przedmiotu " + slotId);
            return replacedItem;
        }
    }

    public bool isEmpty()
    {
        if (MyItem != null)
            return false;
        else
            return true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isEmpty() && MyItem.GetType() != typeof(HealthPotion) && MyItem.GetType() != typeof(StaminaPotion))
        {
            itemProperties.SetActive(true);
            levelValue.text = MyItem.MyLevel.ToString();

            if (MyItem.GetType() == typeof(Weapon))
            {
                propertyLabel.text = "Obrazenia";
                Weapon temItem = (Weapon)MyItem;
                propertyValue.text = temItem.Damage.ToString();
            }
            else if (MyItem.GetType() == typeof(Armor))
            {
                propertyLabel.text = "Ochrona";
                Armor temItem = (Armor)MyItem;
                propertyValue.text = temItem.Protection.ToString();
            }
            else if (MyItem.GetType() == typeof(Helmet))
            {
                propertyLabel.text = "Ochrona";
                Helmet temItem = (Helmet)MyItem;
                propertyValue.text = temItem.Protection.ToString();
            }
            else if(MyItem.GetType() == typeof(Shield))
            {
                propertyLabel.text = "Ochrona";
                Shield temItem = (Shield)MyItem;
                propertyValue.text = temItem.Protection.ToString();
            }
        }
        else
        {
            itemProperties.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemProperties.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!isEmpty())
            {
                //Przenoszenie z slotu do reki
                if (HandScript.instance.isHandEmpty)
                {
                    HandScript.instance.MyItem = MyItem;
                    if(!isChestSlot)
                        inventoryParent.ReplaceItem(MyItem);
                    else
                        chestInventoryParent.ReplaceItem(MyItem);

                    UpdateCountText();

                    HandScript.instance.MyMovingIcon.sprite = HandScript.instance.MyItem.MyIcon;
                    HandScript.instance.MyMovingIcon.color = Color.white;
                    HandScript.instance.isHandEmpty = false;
                }
                else
                {
                    if(AddItem(HandScript.instance.MyItem))
                    {
                        HandScript.instance.MyItem = null;

                        UpdateCountText();

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
                    if (HandScript.instance.MyItem.GetType() == typeof(FinalKey))
                        PlayerController.instance.gameObject.GetComponent<FinalMessage>().ShowMessage();
                    if (!isChestSlot)
                        inventoryParent.items.Add(HandScript.instance.MyItem);
                    else
                        chestInventoryParent.chestItems.Add(HandScript.instance.MyItem);

                    AddItem(HandScript.instance.MyItem);
                    HandScript.instance.MyItem = null;

                    UpdateCountText();

                    HandScript.instance.MyMovingIcon.sprite = null;
                    HandScript.instance.MyMovingIcon.color = new Color(0, 0, 0, 0);
                    HandScript.instance.isHandEmpty = true;
                }
            }
        }
    }
}
