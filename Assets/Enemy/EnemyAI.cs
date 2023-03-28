using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] Transform playerTransform;
    NavMeshAgent _navMeshAgent;

    [SerializeField] float chaseDistance = 5f;
    
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanceToTarget = Vector3.Distance(transform.position, playerTransform.position);
        
        ProvokingEnemy();
        EngageTarget();
        AttackTarget();
        
    }

    void ProvokingEnemy()
    {
        if (distanceToTarget <= chaseDistance && !isProvoked)
        {
            isProvoked = true;
        }
    }

    void EngageTarget()
    {
        if (distanceToTarget > _navMeshAgent.stoppingDistance && isProvoked)
        {
            _navMeshAgent.SetDestination(playerTransform.position);
            transform.LookAt(playerTransform);
        }
    }

    void AttackTarget()
    {
        if (distanceToTarget <= _navMeshAgent.stoppingDistance)
        {
            Debug.Log(transform.name + " hits " + playerTransform.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }
}
