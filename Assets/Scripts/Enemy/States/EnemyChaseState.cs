using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enem) : base(enem) { }

    public override void Update()
    {
        if (senses.TargetDirectionFromRange() == 0)
            stateMachine.ChangeState(enemy.PositionState);
    }
    public override void FixedUpdate()
    {
        enemy.FaceTarget();
        enemy.ChaseTarget(config.WalkSpeed * senses.TargetDirectionFromRange());
    }
}
