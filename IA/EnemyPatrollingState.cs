using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollingState : EnemyBaseState
{
    private Vector3 pointer;
    public EnemyPatrollingState(EnemyStateMachine stateMachine) : base(stateMachine) {  }

    public override void Enter()
    {
        endPathPointer();
        stateMachine.Sight.onPlayerView += onPlayerView;
        stateMachine.Animator.SetFloat("Movement", 1);
        stateMachine.NavMeshAgent.SetDestination(pointer);
    }

    private void onPlayerView()
    {
        stateMachine.SwitchState(new EnemyChasingState(stateMachine));
    }

    public override void Exit()
    {
        stateMachine.Sight.onPlayerView -= onPlayerView;
    }

    public override void Tick(float deltaTime)
    {

        if (stateMachine.transform.position == stateMachine.NavMeshAgent.destination)
        {
            stateMachine.SwitchState(new EnemyWaitState(stateMachine));
        }

    }

    public void endPathPointer()
    {
        if (stateMachine.enemyView)
        {
            pointer.Set(Mathf.Round(stateMachine.pointerPatroll.position.x - stateMachine.pointerPatroll.localScale.x / 2 - 0.10f), 0, 0);
            return;
        }
        pointer.Set(Mathf.Round(stateMachine.pointerPatroll.position.x + stateMachine.pointerPatroll.localScale.x / 2 - 0.10f), 0, 0);
        
    }

}
