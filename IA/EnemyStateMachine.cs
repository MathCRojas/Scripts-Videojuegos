using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    public bool enemyView => transform.rotation.eulerAngles.y <= 180;
    public float velocity;
    public float chasingVelocity;
    [field: SerializeField] public Sight Sight;
    [field: SerializeField] public NavMeshAgent NavMeshAgent;
    [field: SerializeField] public Transform pointerPatroll;
    [field: SerializeField] public BoxCollider AttackRange;
    [field: SerializeField] public Animator Animator { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        AttackRange.enabled = false;
        SwitchState(new EnemyPatrollingState(this));
    }
}
