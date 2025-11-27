using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    public NavMeshAgent agent;
    public Transform player;
    public GameManager gm;

    [Header("AI Settings")]
    public float detectRange = 20f;
    public float attackRange = 2f;
    public float patrolRange = 10f;
    public float patrolWaitTime = 2f;

    private Vector3 patrolPoint;
    private bool patrolPointSet = false;
    private float patrolTimer = 0f;
    private bool gameEnded = false;

    void Awake()
    {
        if (agent == null)
           
           agent = GetComponent<NavMeshAgent>();

        agent.speed = 3.5f;
        agent.angularSpeed = 120f;
        agent.acceleration = 8f;
        agent.stoppingDistance = 0.5f;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) player = playerObj.transform;
            else Debug.LogError("Player not found! Add tag 'Player'.");
        }

        if (gm == null)
            gm = GameObject.FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (player == null || gameEnded) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackRange) AttackPlayer();
        else if (distance <= detectRange) ChasePlayer();
        else Patrol();
    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        if (gm != null)
        {
            gm.GameOver();
            gameEnded = true;
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    void Patrol()
    {
        if (!patrolPointSet)
        {
            Vector3 randomPoint = transform.position + new Vector3(
                Random.Range(-patrolRange, patrolRange), 
                0, 
                Random.Range(-patrolRange, patrolRange)
            );

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 2f, NavMesh.AllAreas))
            {
                patrolPoint = hit.position;
                patrolPointSet = true;
                patrolTimer = 0f;
            }
        }

        if (patrolPointSet)
        {
            agent.SetDestination(patrolPoint);
            if (Vector3.Distance(transform.position, patrolPoint) < 1f)
            {
                patrolTimer += Time.deltaTime;
                if (patrolTimer >= patrolWaitTime)
                    patrolPointSet = false;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow; Gizmos.DrawWireSphere(transform.position, detectRange);
        Gizmos.color = Color.blue; Gizmos.DrawWireSphere(transform.position, patrolRange);
    }
#endif
}
