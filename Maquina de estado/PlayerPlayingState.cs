using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlayingState : PlayerBaseState
{
    private bool isDashing;
    private bool isJumping;
    private Vector3 movement;
    private bool IsGrounded => stateMachine.CharacterController.isGrounded;
    public PlayerPlayingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.DashEvent += onDash;
        stateMachine.InputReader.JumpEvent += onJump;
    }

    private void onDash()
    {
        if (isDashing | isJumping) { return; }
        isDashing = true;
        stateMachine.Animator.SetBool("isDashing", true);
    }

    private void onJump()
    {
        isJumping = true;
        movement.y = stateMachine.jumpForce;
        stateMachine.Animator.SetBool("isJumping", true);
    }


    public override void Tick(float deltaTime)
    {

        //Simula el dash
        if (isDashing)
        {
            movement.x = stateMachine.transform.forward.x * stateMachine.dashForce;
            movement.z = stateMachine.transform.forward.z* stateMachine.dashForce;

            if (GetNormalizedTime("dash") >= 1f)
            {
                isDashing = false;
                stateMachine.Animator.SetBool("isDashing", false);
            }
        }
        else
        {
            //Controla el movimiento y la accion de salto 
            movement.x = MovementPlayer().x * stateMachine.velocity;
            movement.z = MovementPlayer().z * stateMachine.velocity;
       
            if (isJumping)
            {
                if (GetNormalizedTime("jump") >= 1f)
                {
                    isJumping = false;
                    stateMachine.Animator.SetBool("isJumping", false);
                }
            }
        }

        //Simula la gravedad
        if (IsGrounded)
        {
            movement.y = -deltaTime;
        }
        else
        {
            movement.y -= Physics.gravity.y * deltaTime;
        }


        stateMachine.CharacterController.Move(movement * deltaTime);
    }


    private Vector3 MovementPlayer()
    {
        float movementX = stateMachine.InputReader.movementValue.x;
        if(movementX > 0)
        {
            stateMachine.transform.rotation =  Quaternion.Euler(0,90,0);
        }
        
        if(movementX < 0)
        {
            stateMachine.transform.rotation = Quaternion.Euler(0, -90, 0);
        }

        stateMachine.Animator.SetFloat("movement", movementX, 0.5f, 0.2f);
        return new Vector3(
            movementX,
            0,
            0);
    }

    private float GetNormalizedTime(String hash)
    {
        AnimatorStateInfo currentStateInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextStateInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if(stateMachine.Animator.IsInTransition(0) & nextStateInfo.IsTag(hash))
        {
            return nextStateInfo.normalizedTime;
        }
        else if (!stateMachine.Animator.IsInTransition(0) & currentStateInfo.IsTag(hash))
        {
            return currentStateInfo.normalizedTime;
        }
        return 0;
    }

    public override void Exit()
    {
        
    }

}
