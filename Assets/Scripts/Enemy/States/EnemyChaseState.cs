using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(Enemy enem) : base(enem) { }

    public override void FixedUpdate()
    {
        enemy.ChaseTarget(config.WalkSpeed);
    }
}
