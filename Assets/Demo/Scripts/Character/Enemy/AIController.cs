using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AIController : MonoBehaviour
{
    [SerializeField]
    EnemyDetector _enemyDetector;
    [SerializeField]
    NavMeshAgent _navAgent;
    [SerializeField]
    BehaviorGraphAgent _agent;

    public UnityEvent OnAttack;
    public UnityEvent OnHeavyAttack;
    public UnityEvent OnStartBlock;
    public UnityEvent OnStopBlock;

    public EnemyDetector EnemyDetector { get => _enemyDetector; }
    public NavMeshAgent NavAgent { get => _navAgent; }
    public BehaviorGraphAgent Agent { get => _agent; }
    public bool IsAbleToMove { get; set; } = true;

    private void Awake()
    {
        if (!_enemyDetector)
        {
            _enemyDetector = GetComponent<EnemyDetector>();
        }
        if (!_navAgent)
        {
            _navAgent = GetComponent<NavMeshAgent>();
        }
        if (!_agent)
        {
            _agent = GetComponent<BehaviorGraphAgent>();
        }
    }
}
