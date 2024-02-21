using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int level;

    [SerializeField]
    private int health;

    [SerializeField]
    private int damage;

    [SerializeField]
    private int experience;

    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float followRange;

    [SerializeField]
    private Animator enemyAnimator;

    [SerializeField]
    private AudioSource hitSound;

    private NavMeshAgent enemyAgent;

    [SerializeField]
    private EnemyFightDetection fightDetection;

    [SerializeField]
    private EnemyFollowDetection followDetection;

    [SerializeField]
    private FightDetection playerFightDetection;

    [SerializeField]
    private float attackCooldown = 5f;


    private float lastUsedTime;

    private PlayerProperties playerProperties;

    private void Start()
    {
        playerProperties = PlayerProperties.instance;
        enemyAgent = GetComponent<NavMeshAgent>();
        fightDetection.GetComponent<SphereCollider>().radius = attackRange;
        followDetection.GetComponent<SphereCollider>().radius = followRange;
    }

    private void Update()
    {
        if(followDetection.isFollowDetected)
        {
            if (enemyAgent.isStopped == false) 
            {
                enemyAnimator.SetBool("IsWalking", true);
                enemyAnimator.SetBool("InBattle", false);
            }

            if (!fightDetection.isFightDetected)
            {
                enemyAgent.isStopped = false;
                enemyAgent.SetDestination(followDetection.detectedAgent.transform.position);
            }
            else
            {
                enemyAgent.isStopped = true;
                enemyAnimator.SetBool("IsWalking", false);
                enemyAnimator.SetBool("InBattle", true);
                if (Time.time > lastUsedTime + attackCooldown)
                {
                    enemyAnimator.SetTrigger("Attack");

                    lastUsedTime = Time.time;
                }
            }
        }
        else
        {
            enemyAnimator.SetBool("InBattle", false);
        }
        
    }

    public void AlertObserver(string name)
    {
        if (name == "AttackEnded")
        {
            followDetection.detectedAgent.GetComponent<PlayerController>().GetHit(damage);
        }
        else if(name == "DyingEnded")
        {
            Destroy(this.gameObject);
            playerProperties.UpdateCurrentExperience(experience);
        }
    }

    public void GetHit(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
        else
        {
            enemyAnimator.Play("GetHit");
            hitSound.Play();
        }
    }

    public void Die()
    {
        playerFightDetection.isFightDetected = false;
        enemyAnimator.Play("Die");
    }
}
