using UnityEngine;

public class EnemyHitState : EnemyState
{
    private float recoilTimer;
    public EnemyHitState(Enemy enem) : base (enem) { }

    public override void Enter()
    {
        base.Enter();

        recoilTimer = config.RecoilDuration;
        enemy.StepIn(config.StepInSpeed, -enemy.TargetDirection);
        enemy.BasicAttack();
    }
    public override void Update() => recoilTimer -= Time.deltaTime;
    public override void FixedUpdate()
    {
        if (recoilTimer <= 0)
        {
            enemy.StopMovement();
            stateMachine.ChangeState(enemy.ChaseState);
        }
    }
}
