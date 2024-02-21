using UnityEngine;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    public static HandScript instance;

    public Item item;

    [SerializeField]
    private GameObject movingObject;

    private Image movingIcon;

    public bool isHandEmpty = true;

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

    public Image MyMovingIcon 
    {
        get
        {
            return movingIcon;
        }
        set
        {
            movingIcon = value;
        }
    }

    public GameObject MyMovingObject
    {
        get 
        {
            return movingObject;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MyMovingIcon = movingObject.GetComponent<Image>();
    }

    private void Update()
    {
        if(!isHandEmpty)
        {
            movingObject.GetComponent<RectTransform>().position = Input.mousePosition;
        }
    }
}
