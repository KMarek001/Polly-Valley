using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    private Vector3 startPosition = new Vector3(42, 0, 45);

    [SerializeField]
    private AudioSource hitSound;

    [SerializeField]
    private AudioSource drinkSound;

    [SerializeField]
    private AudioSource dieSound;

    [SerializeField]
    private AudioSource levelUpSound;

    PlayerProperties playerProperties;

    private HUDController hudController;

    private Animator playerAnimator;

    private NavMeshAgent playerAgent;

    [SerializeField]
    private GameObject playerEquipment;

    [SerializeField]
    private InventoryManager playerInventoryManager;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject settingsMenu;

    [SerializeField]
    private GameObject map;

    [SerializeField]
    private GameObject message;

    [SerializeField]
    private GameObject hud;
        
    [SerializeField]
    private float healthRegenTime;
    private float lastTimeHealthRegen;

    [SerializeField]
    private float staminaRegenTime;
    private float lastTimeStaminaRegen;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerProperties = PlayerProperties.instance;
        playerAnimator = GetComponent<Animator>();
        playerEquipment.SetActive(false);
        hudController = HUDController.instance;
        playerAgent = GetComponent<NavMeshAgent>();
        message.SetActive(true);
        hud.SetActive(false);
        GameManager.instance.gameStatus = GameManager.GameStatus.Pause;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LevelUp();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerInventoryManager.UseHealthPotion();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerInventoryManager.UseStaminaPotion();
        }

        if(Input.GetKeyDown(KeyCode.M)) 
        {
            if(map.activeSelf)
                map.SetActive(false);
            else if(!map.activeSelf && GameManager.instance.gameStatus == GameManager.GameStatus.Play)
                map.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.gameStatus != GameManager.GameStatus.ChestOpened) 
        { 
            if(playerEquipment.activeSelf)
            {
                playerEquipment.SetActive(false);
                GameManager.instance.gameStatus = GameManager.GameStatus.Play;
            }
            else if(!playerEquipment.activeSelf)
            {
                playerEquipment.SetActive(true);
                GameManager.instance.gameStatus = GameManager.GameStatus.Pause;
            }
        }
        if( Input.GetKeyDown(KeyCode.Escape) && message.activeSelf) 
        {
            message.SetActive(false);
            hud.SetActive(true);
            GameManager.instance.gameStatus = GameManager.GameStatus.Play;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && GameManager.instance.gameStatus != GameManager.GameStatus.ChestOpened)
        {
            if(pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                GameManager.instance.gameStatus = GameManager.GameStatus.Play;
            }
            else if(!pauseMenu.activeSelf && !settingsMenu.activeSelf)
            {
                pauseMenu.SetActive(true);
                GameManager.instance.gameStatus = GameManager.GameStatus.Pause;
            }
                
        }

        //Regeneracja zdrowia
        if (playerProperties.CurrentPlayerHealth < playerProperties.MaxPlayerHealth && Time.time > lastTimeHealthRegen + healthRegenTime)
        {
            playerProperties.CurrentPlayerHealth++;
            hudController.UpdateHealth();
            lastTimeHealthRegen = Time.time;
        }

        //Regeneracja staminy
        if (playerProperties.CurrentPlayerStamina < playerProperties.MaxPlayerStamina && Time.time > lastTimeStaminaRegen + staminaRegenTime)
        {
            playerProperties.CurrentPlayerStamina++;
            hudController.UpdateStamina();
            lastTimeStaminaRegen = Time.time;
        }
    }

    public void GetHit(int damage)
    {
        if(playerProperties.CurrentPlayerHealth - (damage - playerProperties.Protection / 10) <= 0)
        {
            dieSound.Play();
            playerAnimator.SetTrigger("Die");
        }
        else
        {
            playerAnimator.SetTrigger("GetHit");
            hitSound.Play();
            playerProperties.CurrentPlayerHealth -= (damage - playerProperties.Protection/10);
            hudController.UpdateHealth();
            hudController.UpdateMaxHealth();
        }
    }

    public bool DealStamina(int stamina)
    {
        if (playerProperties.CurrentPlayerStamina - stamina <= 0)
            return false;
        else
        {
            playerProperties.CurrentPlayerStamina -= stamina;
            hudController.UpdateStamina();
            hudController.UpdateMaxStamina();
            return true;
        }
    }

    public void Die()
    { 
        playerAnimator.SetTrigger("WakeUp");
        transform.position = startPosition;
        playerProperties.CurrentPlayerHealth = playerProperties.MaxPlayerHealth;
        hudController.UpdateHealth();
        playerProperties.CurrentPlayerStamina = playerProperties.MaxPlayerStamina;
        hudController.UpdateStamina();
        playerAgent.ResetPath();
    }

    public void RegenerateHealth(int healthRegen)
    {
        playerAnimator.SetTrigger("DrinkPotion");
        drinkSound.Play();
        playerProperties.CurrentPlayerHealth += healthRegen;
        hudController.UpdateHealth();
        if(playerProperties.CurrentPlayerHealth > playerProperties.MaxPlayerHealth)
        {
            playerProperties.CurrentPlayerHealth = playerProperties.MaxPlayerHealth;
            hudController.UpdateHealth();
        }
    }

    public void RegenerateStamina(int staminaRegen)
    {
        playerAnimator.SetTrigger("DrinkPotion");
        drinkSound.Play();
        playerProperties.CurrentPlayerStamina += staminaRegen;
        hudController.UpdateStamina();
        if (playerProperties.CurrentPlayerStamina > playerProperties.MaxPlayerStamina)
        {
            playerProperties.CurrentPlayerStamina = playerProperties.MaxPlayerStamina;
            hudController.UpdateStamina();
        }
    }

    public void LevelUp()
    {
        playerAnimator.SetTrigger("LevelUp");
        levelUpSound.Play();
        hudController.LevelUp();
        playerProperties.PlayerLevel++;
        playerEquipment.GetComponent<EquipmentManager>().IncreasePoints();
        playerEquipment.GetComponent<EquipmentManager>().UpdatePlayerLevel(playerProperties.PlayerLevel);
    }
}
