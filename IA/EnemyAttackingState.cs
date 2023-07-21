using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    public float attacking => GetNormalizedTime("attack");
    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.NavMeshAgent.SetDestination(stateMachine.transform.position);
        stateMachine.Animator.SetBool("isAttacking", true);
    }

    public override void Exit()
    {
        stateMachine.Animator.SetBool("isAttacking", false);
        stateMachine.AttackRange.enabled = false;
        stateMachine.NavMeshAgent.isStopped = false;
        stateMachine.NavMeshAgent.speed = stateMachine.velocity;
    }

    public override void Tick(float deltaTime)
    {
        
        if(attacking > 0.3f)
        {
            stateMachine.AttackRange.enabled = true;
        }

        if(attacking >= 1f)
        {
            stateMachine.SwitchState(new EnemyPatrollingState(stateMachine));
        }

    }

    private float GetNormalizedTime(string hash)
    {
        AnimatorStateInfo currentStateInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextStateInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);
        if (stateMachine.Animator.IsInTransition(0) & nextStateInfo.IsTag(hash))
        {
            return nextStateInfo.normalizedTime;
        }
        else if (!stateMachine.Animator.IsInTransition(0) & currentStateInfo.IsTag(hash))
        {
            return currentStateInfo.normalizedTime;
        }
        return 0;
    }

}
