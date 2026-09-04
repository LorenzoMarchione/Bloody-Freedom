using UnityEngine;

public class EnemyChaseState : EnemyState
{
    private int direction = 0;
    public EnemyChaseState(Enemy enem) : base(enem) { }

    public override void Update()
    {
        switch (senses.TargetRange(config.EntryDistanceRange))
        {
            case (TargetRangeStatus.TooClose):
                direction = -1;
                break;

            case (TargetRangeStatus.TooFar):
                direction = 1;
                break;
            
            case (TargetRangeStatus.InRange):
                stateMachine.ChangeState(enemy.PositionState);
                break;
        }
    }
    public override void FixedUpdate()
    {
        enemy.FaceTarget();
        enemy.ChaseTarget(config.WalkSpeed * direction);
    }
}
