using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;

    [SerializeField]
    private Slider healthSlider;

    [SerializeField]
    private Slider staminaSlider;

    [SerializeField]
    private Slider experienceSlider;

    [SerializeField]
    private TextMeshProUGUI levelValue;

    [SerializeField]
    private Transform minimapCamera;
    private int zoom = 10;

    private PlayerProperties playerProperties;

    [SerializeField]
    private TextMeshProUGUI currentHealthText;
    [SerializeField]
    private TextMeshProUGUI maxHealthText;

    [SerializeField]
    private TextMeshProUGUI currentStaminaText;
    [SerializeField]
    private TextMeshProUGUI maxStaminaText;

    [SerializeField]
    private TextMeshProUGUI currentExperienceText;
    [SerializeField]
    private TextMeshProUGUI maxExperienceText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerProperties = PlayerProperties.instance;
        if (playerProperties.isActiveAndEnabled)
        {
            UpdateMaxHealth();
            UpdateHealth();

            UpdateMaxStamina();
            UpdateStamina();

            UpdateExperience();
            UpdateMaxExperience();

            UpdateLevel();
        }
    }

    private void Update()
    {
        MinimapZoom();
    }

    public void UpdateHealth()
    {
        healthSlider.value = playerProperties.CurrentPlayerHealth;
        currentHealthText.text = playerProperties.CurrentPlayerHealth.ToString();
    }

    public void UpdateMaxHealth()
    {
        healthSlider.maxValue = playerProperties.MaxPlayerHealth;
        maxHealthText.text = playerProperties.MaxPlayerHealth.ToString();
    }

    public void UpdateStamina()
    {
        staminaSlider.value = playerProperties.CurrentPlayerStamina;
        currentStaminaText.text = playerProperties.CurrentPlayerStamina.ToString();
    }

    public void UpdateMaxStamina()
    {
        staminaSlider.maxValue = playerProperties.MaxPlayerStamina;
        maxStaminaText.text = playerProperties.MaxPlayerStamina.ToString() ;
    }

    public void UpdateExperience()
    {
        experienceSlider.value = playerProperties.CurrentPlayerExp;
        currentExperienceText.text = playerProperties.CurrentPlayerExp.ToString();
    }

    public void UpdateMaxExperience()
    {
        experienceSlider.maxValue = playerProperties.MaxPlayerExp;
        maxExperienceText.text = playerProperties.MaxPlayerExp.ToString();
    }

    public void UpdateLevel()
    {
        levelValue.text = playerProperties.PlayerLevel.ToString();
    }

    public void LevelUp()
    {
        int levelNumber = int.Parse(levelValue.text);
        levelNumber++;
        levelValue.text = levelNumber.ToString();
        playerProperties.MaxPlayerExp += levelNumber * 10;
        UpdateMaxExperience();
    }

    private void MinimapZoom()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(minimapCamera.position.y > 20)
                minimapCamera.SetPositionAndRotation(new Vector3(minimapCamera.position.x, minimapCamera.position.y - zoom, minimapCamera.position.z), minimapCamera.rotation);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (minimapCamera.position.y < 110)
                minimapCamera.SetPositionAndRotation(new Vector3(minimapCamera.position.x, minimapCamera.position.y + zoom, minimapCamera.position.z), minimapCamera.rotation);
        }
    }

}
