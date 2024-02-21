using UnityEngine;
using UnityEngine.AI;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private FightDetection fightArea;

    private NavMeshAgent playerAgent;
    private Animator playerAnimator;

    [SerializeField]
    private AudioSource walkingSound;

    private void Start()
    {
        playerAgent = this.GetComponent<NavMeshAgent>();
        playerAnimator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0) && GameManager.instance.gameStatus == GameManager.GameStatus.Play) 
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;
            

            if(Physics.Raycast(ray, out hitPoint))
            {
                if (hitPoint.collider.tag == "Ground" && hitPoint.collider.tag != "Enemy")
                {
                    playerAgent.SetDestination(hitPoint.point);
                }
                else if(hitPoint.collider.tag == "Chest" && fightArea.isOpeningChestAvailable)
                {
                    hitPoint.collider.gameObject.GetComponent<ChestController>().OpenChest();
                }
            }

        }

        if (!playerAgent.hasPath && GameManager.instance.gameStatus == GameManager.GameStatus.Play)
        {
            playerAnimator.SetBool("isWalking", false);
            walkingSound.enabled = false;
        }
        else if(playerAgent.hasPath && GameManager.instance.gameStatus == GameManager.GameStatus.Play)
        {
            playerAnimator.SetBool("isWalking", true);
            walkingSound.enabled = true;
        }
    }
}
