using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChestController : MonoBehaviour
{
    [SerializeField]
    private GameObject chestInventory;

    [SerializeField]
    private GameObject playerInventory;

    [SerializeField]
    private NavMeshAgent playerAgent;

    [SerializeField]
    private List<Item> chestItems = new List<Item>();

    [SerializeField]
    private ChestInventoryManager chestInventoryManager;

    [SerializeField]
    private AudioSource openingSound;
    
    [SerializeField]
    private AudioSource closingSound;

    private GameObject loot;

    private Animator chestAnimator;

    private Transform chestTop;

    private void Start()
    {
        loot = transform.Find("Loot").gameObject;
        chestAnimator = GetComponent<Animator>();
        chestTop = transform.Find("Chest_Top");
    }

    public void AlertObserver(string name)
    {
        if (name == "OpeningFinished")
        {
            playerInventory.SetActive(true);
            chestInventory.SetActive(true);
            chestInventoryManager.FillChestInventoryItems(chestItems);
            chestItems.Clear();
            chestTop.SetPositionAndRotation(chestTop.position, new Quaternion(-90, 0, 0, 0));
            ChestInventoryManager.instance.currentChest = this.gameObject;
            GameManager.instance.gameStatus = GameManager.GameStatus.ChestOpened;
        }
        else if (name == "ClosingFinished")
        {
            chestTop.SetPositionAndRotation(chestTop.position, new Quaternion(0, 0, 0, 0));
        }
    }

    public bool isEmpty()
    {
        if(chestItems.Count > 0)
        {
            return false;
        }
        else 
        { 
            return true;
        }
    }

    public void OpenChest()
    {

        if (!isEmpty())
        {
            playerAgent.ResetPath();
            chestAnimator.Play("OpeningChest");
            openingSound.Play();
        }
        else
        {
            playerAgent.ResetPath();
            playerInventory.SetActive(true);
            chestInventory.SetActive(true);
            chestInventoryManager.FillChestInventoryItems(chestItems);
            chestItems.Clear();
            ChestInventoryManager.instance.currentChest = this.gameObject;
            GameManager.instance.gameStatus = GameManager.GameStatus.ChestOpened;
        }
    }

    public void CloseChest()
    {
        foreach (Item item in chestInventoryManager.ClearChestInventory())
            chestItems.Add(item);

        if (!isEmpty())
        {
            closingSound.Play();
            chestInventory.SetActive(false);
            playerInventory.SetActive(false);
            loot.SetActive(true);
            GameManager.instance.gameStatus = GameManager.GameStatus.Play;
            chestAnimator.Play("ClosingChest");
        }
        else
        {
            chestInventory.SetActive(false);
            playerInventory.SetActive(false);
            GameManager.instance.gameStatus = GameManager.GameStatus.Play;
            chestTop.SetPositionAndRotation(chestTop.position, new Quaternion(-90, 0, 0, 0));
            loot.SetActive(false);
        }


    }
}
