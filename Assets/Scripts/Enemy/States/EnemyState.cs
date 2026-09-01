using UnityEngine;

public abstract class EnemyState
{
    protected Enemy enemy;
    protected Rigidbody rb;
    protected EnemySenses senses;
    protected EnemyConfig config;
    protected EnemyStateMachine stateMachine;
    
    public EnemyState (Enemy enem)
    {
        enemy = enem;
        rb = enem.GetComponent<Rigidbody>();
        senses = enem.GetComponent<EnemySenses>();
        config = enem.GetComponent<EnemyConfig>();
        stateMachine = enem.GetComponent<EnemyStateMachine>();
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void OnAnimationFinished() { }
}
