using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float stepInTimer;
    public EnemyAttackState(Enemy enem) : base(enem) { }

    public override void Enter()
    {
        stepInTimer = config.StepInDuration;
        enemy.StepIn(config.StepInSpeed, enemy.TargetDirection);
        enemy.BasicAttack();
    }
    public override void Update() => stepInTimer -= Time.deltaTime;
    public override void FixedUpdate()
    {
        if(stepInTimer <= 0)
            enemy.StopMovement();
    }
    public override void OnAnimationFinished()
    {
        stateMachine.ChangeState(enemy.PositionState);
    }
}
