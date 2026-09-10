using UnityEngine;

public class EnemyDeadState : EnemyState
{
    public EnemyDeadState(Enemy enem) : base(enem) { }

    public override void Enter()
    {
        base.Enter();

        enemy.StopMovement();
    }
}
