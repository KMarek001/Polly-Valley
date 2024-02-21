using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerProperties : MonoBehaviour
{
    public static PlayerProperties instance = null;

    [HeaderAttribute("Health")]
    [SerializeField]
    private Slider healthBar;
    [SerializeField]
    private int currentPlayerHealth;
    [SerializeField]
    private int maxPlayerHealth;

    [HeaderAttribute("Stamina")]
    [SerializeField]
    private Slider staminaBar;
    [SerializeField]
    private int currentPlayerStamina;
    [SerializeField]
    private int maxPlayerStamina;

    [HeaderAttribute("Experience")]
    [SerializeField]
    private Slider expBar;
    [SerializeField]
    private int currentPlayerExp;
    [SerializeField]
    private int maxPlayerExp;

    [ReadOnly(true)]
    private int playerLevel;

    [SerializeField]
    private int attackDamage;

    [SerializeField]
    private int speed;

    [SerializeField]
    private int protection;

    [SerializeField]
    private InventoryManager inventory;

    [SerializeField]
    private EquipmentManager equipment;

    private PlayerController playerController;

    private HUDController hudController;

    public int CurrentPlayerHealth { get => currentPlayerHealth; set => currentPlayerHealth = value; }
    public int MaxPlayerHealth { get => maxPlayerHealth; set => maxPlayerHealth = value; }

    public int CurrentPlayerStamina { get => currentPlayerStamina; set => currentPlayerStamina = value; }
    public int MaxPlayerStamina { get => maxPlayerStamina; set => maxPlayerStamina = value; }

    public int CurrentPlayerExp { get => currentPlayerExp; set => currentPlayerExp = value; }
    public int MaxPlayerExp { get => maxPlayerExp; set => maxPlayerExp = value; }

    public int PlayerLevel { get => playerLevel; set => playerLevel = value; }

    public int AttackDamage { get => attackDamage; set => attackDamage = value; }

    public int Speed { get => speed; set => speed = value; }

    public int Protection { get => protection; set => protection = value; }

    void Awake()
    {
        instance = this;
        currentPlayerHealth = maxPlayerHealth;
        currentPlayerStamina = maxPlayerStamina;
        currentPlayerExp = 0;
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        hudController = HUDController.instance;
    }

    public void UpdateCurrentHealth(int health)
    {
        currentPlayerHealth += health;
    }

    public void UpdateCurrentExperience(int experience)
    {
        currentPlayerExp += experience;
        if (currentPlayerExp >= maxPlayerExp)
        {
            int diff = currentPlayerExp - maxPlayerExp;
            currentPlayerExp = diff;
            playerController.LevelUp();
        }
        hudController.UpdateExperience();
    }

    public void UpdateCurrentStamina(int stamina)
    {
        currentPlayerStamina += stamina;
    }
}
