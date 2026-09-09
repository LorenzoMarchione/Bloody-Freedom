using UnityEngine;

public class EnemyStateMachine
{
    private EnemyState currentState;
    
    public void ChangeState(EnemyState state)
    {
        if(currentState != null)
            currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
    public void Initialize(EnemyState state)
    {
        ChangeState(state);
    }
    public void Update()
    {
        currentState.Update();
    }
    public void FixedUpdate()
    {
        currentState.FixedUpdate();
    }
    public void OnAnimationFinished()
    {
        currentState.OnAnimationFinished();
    }
}
