using UnityEngine;

public class EnemyRangedAttackState : EnemyState
{
    public EnemyRangedAttackState (Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        base.Enter();

        enemy.StopMovement();

        enemy.ConsumeAttack();
        enemy.RangedAttackAnim();
    }
    public override void FixedUpdate()
    {
        enemy.FaceTarget();
    }
    public override void OnAnimationTrigger() => enemy.RangedAttack();
    public override void OnAnimationFinished() => stateMachine.ChangeState(enemy.PositionState);
}
