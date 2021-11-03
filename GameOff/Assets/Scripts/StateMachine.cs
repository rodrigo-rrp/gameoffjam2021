using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class State {
    public Entity owner;
    public StateMachine stateMachine;

    public virtual void OnEnter() {}
    public virtual void OnStay() {}
    public virtual void OnExit() {}

    public State(Entity instance, StateMachine sm) { 
       this.owner = instance;
       this.stateMachine = sm;
    }

}

public class StateMachine
{
    public State currentState;
    public List<State> states = new List<State>();

    public void AddState(State state)
    {
        states.Add(state);
    }

    public void Start()
    {
        currentState = states[0];
        currentState.OnEnter();
    }

    public void ChangeState(System.Type newStateType){
        if (currentState != null)
            currentState.OnExit();
        currentState = states.Where(l => l.GetType() == newStateType).First();
        currentState.OnEnter();
    }

    public void Update()
    {
        if (currentState != null)
            currentState.OnStay();
    }
}
