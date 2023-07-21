using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public float velocity;
    [field: SerializeField] public float dashForce;
    [field: SerializeField] public float jumpForce;
    [field: SerializeField] public InputReader InputReader;
    [field: SerializeField] public CharacterController CharacterController;
    [field: SerializeField] public Animator Animator { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        SwitchState(new PlayerPlayingState(this));
    }

}
