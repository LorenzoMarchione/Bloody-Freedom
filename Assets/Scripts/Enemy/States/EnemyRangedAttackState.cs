using UnityEngine;

public class EnemyRangedAttackState : EnemyState
{
    public EnemyRangedAttackState (Enemy enemy) : base(enemy) { }

    public override void Enter()
    {
        enemy.RangedAttackAnim();
    }
    public override void OnAnimationFinished()
    {
        enemy.RangedAttack();
        stateMachine.ChangeState(enemy.PositionState);
    }
}
