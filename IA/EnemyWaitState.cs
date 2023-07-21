using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaitState : EnemyBaseState
{
    private float timer = 0;

    public EnemyWaitState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.NavMeshAgent.isStopped = true;
        stateMachine.Animator.SetFloat("Movement", 0);
        stateMachine.Sight.onPlayerView += onPlayerView;
    }

    private void onPlayerView()
    {
        stateMachine.SwitchState(new EnemyChasingState(stateMachine));
    }

    public override void Exit()
    {
        stateMachine.NavMeshAgent.isStopped = false;
        stateMachine.Sight.onPlayerView -= onPlayerView;
    }

    public override void Tick(float deltaTime)
    {
        if (timer < 3)
        {
            timer += deltaTime;
            return;
        }

        stateMachine.SwitchState(new EnemyPatrollingState(stateMachine));
    }

}
