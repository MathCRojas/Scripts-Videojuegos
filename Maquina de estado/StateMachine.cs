using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;

    // Start is called before the first frame update
    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    // Update is called once per frame
    public void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }
}
