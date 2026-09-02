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
        rb = enem.RigidBody;
        senses = enem.Senses;
        config = enem.EnemyConfig;
        stateMachine = enem.StateMachine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void OnAnimationFinished() { }
}
