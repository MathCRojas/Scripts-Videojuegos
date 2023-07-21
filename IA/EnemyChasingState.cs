using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private float targetDeviation => stateMachine.NavMeshAgent.remainingDistance ;
    private Transform target => stateMachine.Sight.Target;
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.NavMeshAgent.speed = stateMachine.chasingVelocity;
    }

    public override void Exit()
    {
        stateMachine.NavMeshAgent.speed = stateMachine.velocity;
    }

    public override void Tick(float deltaTime)
    {
        if(target == null)
        {
            stateMachine.SwitchState(new EnemyWaitState(stateMachine));
            return;
        }

        stateMachine.Animator.SetFloat("Movement", 2, 0.5f, 0.2f);
        stateMachine.NavMeshAgent.SetDestination(target.position);

        if(targetDeviation <= 1)
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
        }

    }

}
