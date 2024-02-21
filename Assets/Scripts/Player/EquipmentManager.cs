using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class EquipmentManager : MonoBehaviour
{
    private HUDController hudController;
    private PlayerProperties playerProperties;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private Transform head;

    [SerializeField]
    private Transform rightHand;

    [SerializeField]
    private Transform leftHand;

    [SerializeField]
    private RuntimeAnimatorController[] playerAnimators;

    [SerializeField]
    private Animator playerAnimator;

    private int points = 0;

    private int maxHealthPoints;
    private int maxStaminaPoints;
    private int strengthPoints;
    private int speedPoints;

    [SerializeField]
    private TextMeshProUGUI pointsText;

    [SerializeField]
    private TextMeshProUGUI maxHealthText;

    [SerializeField]
    private TextMeshProUGUI maxStaminaText;

    [SerializeField]
    private TextMeshProUGUI strengthText;
    
    [SerializeField]
    private TextMeshProUGUI speedText;

    [SerializeField]
    private TextMeshProUGUI playerLevelText;

    [SerializeField]
    private AudioSource equipSound;

    private Helmet equippedHelmet = null;
    private Armor equippedArmor = null;
    private Weapon equippedSword = null;
    private Shield equippedShield = null;

    private void Start()
    {
        hudController = HUDController.instance;
        playerProperties = PlayerProperties.instance;
        maxHealthPoints = playerProperties.MaxPlayerHealth;
        maxStaminaPoints = playerProperties.MaxPlayerStamina;
        strengthPoints = playerProperties.AttackDamage;
        speedPoints = playerProperties.Speed;

        maxHealthText.text = maxHealthPoints.ToString();
        maxStaminaText.text = maxStaminaPoints.ToString();
        strengthText.text = strengthPoints.ToString();
        speedText.text = speedPoints.ToString();
    }

    public void Equip(Item item, EquipmentSlot slot)
    {
        switch(slot.MyEquipmentType) 
        {
            case EquipmentSlot.EquipmentSlotTypes.Helmet:
                {
                    equippedHelmet = (Helmet)item;
                    head.Find("Hair").gameObject.SetActive(false);
                    head.Find(equippedHelmet.MyName).gameObject.SetActive(true);
                    playerProperties.Protection += equippedHelmet.Protection;
                    equipSound.Play();
                    break;
                }
            case EquipmentSlot.EquipmentSlotTypes.Armor:
                {
                    equippedArmor = (Armor)item;
                    player.Find("BasicBody").gameObject.SetActive(false);
                    player.Find(equippedArmor.MyName).gameObject.SetActive(true);
                    playerProperties.Protection += equippedArmor.Protection;
                    equipSound.Play();
                    break;
                }
            case EquipmentSlot.EquipmentSlotTypes.Sword:
                {
                    equippedSword = (Weapon)item;
                    rightHand.Find(equippedSword.MyName).gameObject.SetActive(true);
                    playerProperties.AttackDamage += equippedSword.Damage;
                    ChangeAnimator();
                    equipSound.Play();
                    break;
                }
            case EquipmentSlot.EquipmentSlotTypes.Shield:
                {
                    equippedShield = (Shield)item;
                    leftHand.Find(equippedShield.MyName).gameObject.SetActive(true);
                    playerProperties.Protection += equippedShield.Protection;
                    ChangeAnimator();
                    equipSound.Play();
                    break;
                }
        }
    }

    public Item Unequip(EquipmentSlot slot)
    {
        Item returnedItem = null;
        switch (slot.MyEquipmentType)
        {
            case EquipmentSlot.EquipmentSlotTypes.Helmet:
                {
                    returnedItem = equippedHelmet;
                    head.Find(equippedHelmet.MyName).gameObject.SetActive(false);
                    head.Find("Hair").gameObject.SetActive(true);
                    playerProperties.Protection -= equippedHelmet.Protection;
                    equippedHelmet = null;
                    break;
                }
            case EquipmentSlot.EquipmentSlotTypes.Armor:
                {
                    returnedItem = equippedArmor;
                    player.Find(equippedArmor.MyName).gameObject.SetActive(false);
                    player.Find("BasicBody").gameObject.SetActive(true);
                    playerProperties.Protection -= equippedArmor.Protection;
                    equippedArmor = null;
                    break;
                }
            case EquipmentSlot.EquipmentSlotTypes.Sword:
                {
                    returnedItem = equippedSword;
                    rightHand.Find(equippedSword.MyName).gameObject.SetActive(false);
                    playerProperties.AttackDamage -= equippedSword.Damage;
                    equippedSword = null;
                    ChangeAnimator();
                    break;
                }
            case EquipmentSlot.EquipmentSlotTypes.Shield:
                {
                    returnedItem = equippedShield;
                    leftHand.Find(equippedShield.MyName).gameObject.SetActive(false);
                    playerProperties.Protection -= equippedShield.Protection;
                    equippedShield = null;
                    ChangeAnimator();
                    break;
                }
        }
        return returnedItem;
    }

    public void IncreasePoints()
    {
        points++;
        pointsText.text = points.ToString();
    }

    public void OnMaxHealthIncrease()
    {
        if(points > 0) 
        {
            maxHealthPoints += 10;
            points--;
            pointsText.text = points.ToString();
            playerProperties.MaxPlayerHealth = maxHealthPoints;
            hudController.UpdateMaxHealth();
            maxHealthText.text = maxHealthPoints.ToString();
        }
    }

    public void OnMaxStaminaIncrease()
    {
        if (points > 0)
        {
            maxStaminaPoints += 10;
            points--;
            pointsText.text = points.ToString();
            playerProperties.MaxPlayerStamina = maxStaminaPoints;
            hudController.UpdateMaxStamina();
            maxStaminaText.text = maxStaminaPoints.ToString();
        }
    }

    public void OnStrengthIcrease()
    {
        if (points > 0 && strengthPoints < 20)
        {
            points--;
            pointsText.text = points.ToString();
            strengthPoints++;
            playerProperties.AttackDamage++;
            strengthText.text = strengthPoints.ToString();
        }
    }

    public void OnSpeedIncrease() 
    {
        if (points > 0 && speedPoints < 20)
        {
            points--;
            pointsText.text = points.ToString();
            speedPoints++;
            playerProperties.Speed++;
            player.GetComponent<NavMeshAgent>().speed += (0.5f);
            player.GetComponent<NavMeshAgent>().acceleration += 5;
            speedText.text = speedPoints.ToString();
        }
    }
    
    public void UpdatePlayerLevel(int level)
    {
        playerLevelText.text = level.ToString();
    }

    public void ChangeAnimator()
    {
        if(equippedSword == null && equippedShield == null) //Brak broni
        {
            playerAnimator.runtimeAnimatorController = playerAnimators[0];
        }
        else if (equippedSword != null && equippedShield == null) //Sam miecz
        {
            playerAnimator.runtimeAnimatorController = playerAnimators[1];
        }
        else if (equippedSword != null && equippedShield != null) //Miecz i tarcza
        {
            playerAnimator.runtimeAnimatorController = playerAnimators[2];
        }
    }
}
