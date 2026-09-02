using UnityEngine;

public class EnemyPositioningState : EnemyState
{
    public EnemyPositioningState(Enemy enem) : base(enem) { }

    public override void Update()
    {
        if (senses.TargetDirectionFromRange() != 0)
            stateMachine.ChangeState(enemy.ChaseState);
    }
    public override void FixedUpdate()
    {
        enemy.FaceTarget();
        enemy.StrafeTarget(config.WalkSpeed);
    }
}
