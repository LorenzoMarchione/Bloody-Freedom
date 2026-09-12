using System.Xml;
using UnityEngine;

public abstract class EnemyState
{
    protected Enemy enemy;
    protected Rigidbody rb;
    protected EnemySenses senses;
    protected EnemyConfig config;
    protected EnemyStateMachine stateMachine;
    protected Health health;
    
    public EnemyState (Enemy enem)
    {
        enemy = enem;
        rb = enem.RigidBody;
        senses = enem.Senses;
        config = enem.EnemyConfig;
        stateMachine = enem.StateMachine;
        health = enem.Health;
    }

    public virtual void Enter() 
    {
        health.OnDamaged += HandleDamage;
        health.OnDeath += HandleDeath;
    }
    public virtual void Exit()
    {
        health.OnDamaged -= HandleDamage;
        health.OnDeath -= HandleDeath;
    }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public virtual void OnAnimationFinished() { }
    protected virtual void HandleDamage()
    {
        stateMachine.ChangeState(enemy.HitState);
    }
    protected virtual void HandleDeath()
    {
        stateMachine.ChangeState(enemy.DeadState);
    }
}
