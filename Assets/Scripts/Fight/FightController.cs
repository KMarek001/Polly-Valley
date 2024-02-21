using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class FightController : MonoBehaviour
{
    [SerializeField]
    private GameObject fightArea;

    [SerializeField]
    private Animator playerAnimator;

    [SerializeField]
    private float attackRange = 1.5f;

    [SerializeField]
    private float attackCooldown = 5f;

    [SerializeField]
    private float specialAttackCooldown = 7f;

    private float lastAttackUsedTime;
    private float lastSpecialAttackUsedTime;

    [SerializeField]
    private Camera cam;

    private FightDetection fightDetection = null;

    private NavMeshAgent playerAgent;

    private PlayerController playerController;
    private PlayerProperties playerProperties;

    [SerializeField]
    private AudioSource attackSound;

    private int normalAttackStaminaExpenditiure = 10;
    private int specialAttackStaminaExpenditiure = 20;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerProperties = GetComponent<PlayerProperties>();
        fightArea.GetComponent<SphereCollider>().radius = attackRange;
        playerAgent = GetComponent<NavMeshAgent>();
        fightDetection = fightArea.GetComponent<FightDetection>();
    }

    void Update()
    {
        if(fightDetection.isFightDetected)
        {
            playerAgent.isStopped = false;
            if (Input.GetMouseButtonDown(0) && Time.time > lastAttackUsedTime + attackCooldown && playerProperties.CurrentPlayerStamina - normalAttackStaminaExpenditiure > 0)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitPoint;


                if (Physics.Raycast(ray, out hitPoint))
                {
                    if (hitPoint.collider.tag == "Enemy")
                    {
                        playerAgent.SetDestination(hitPoint.collider.transform.position);

                        if (playerAgent.remainingDistance < (hitPoint.collider.gameObject.GetComponent<CapsuleCollider>().radius + GetComponent<CapsuleCollider>().radius + 0.4f))
                        {
                            playerAgent.ResetPath();
                        }

                        playerAnimator.SetTrigger("Attack1");
                        attackSound.Play();
                        hitPoint.collider.transform.GetComponentInParent<EnemyController>().GetHit(playerProperties.AttackDamage);
                        playerController.DealStamina(normalAttackStaminaExpenditiure);
                    }
                }
                lastAttackUsedTime = Time.time;
            }
            else if (Input.GetMouseButtonDown(1) && Time.time > lastSpecialAttackUsedTime + specialAttackCooldown && playerProperties.CurrentPlayerStamina - specialAttackStaminaExpenditiure > 0)
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitPoint;


                if (Physics.Raycast(ray, out hitPoint))
                {
                    if (hitPoint.collider.tag == "Enemy")
                    {
                        playerAgent.SetDestination(hitPoint.collider.transform.position);

                        if (playerAgent.remainingDistance < (hitPoint.collider.gameObject.GetComponent<CapsuleCollider>().radius + GetComponent<CapsuleCollider>().radius + 0.4f))
                        {
                            playerAgent.ResetPath();
                        }

                        playerAnimator.SetTrigger("Attack2");
                        attackSound.Play();
                        hitPoint.collider.transform.GetComponentInParent<EnemyController>().GetHit(playerProperties.AttackDamage + playerProperties.AttackDamage/2);
                        playerController.DealStamina(specialAttackStaminaExpenditiure);
                    }
                }
                lastSpecialAttackUsedTime = Time.time;
            }
        }
    }
}
