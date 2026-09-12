using UnityEngine;

public enum Stance
{
    Stay,
    OrbitLeft,
    OrbitRight
}
public class EnemyPositioningState : EnemyState
{
    private float stanceTimer;
    private Stance currentStance;
    private float attackTimer;
    public EnemyPositioningState(Enemy enem) : base(enem) { }

    public override void Enter()
    {
        base.Enter();

        ChooseNewStance();
        SetNextAttack();
    }

    public override void Update()
    {
        stanceTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;

        if (stanceTimer <= 0)
            ChooseNewStance();

        if (senses.TargetRange(config.ExitDistanceRange) != TargetRangeStatus.InRange)
            stateMachine.ChangeState(enemy.ChaseState);
        else if (attackTimer <= 0 && senses.GetSqrDistanceFromTarget() < (config.RangedRange * config.RangedRange))
            stateMachine.ChangeState(enemy.RangedAttackState);
        else if (attackTimer <= 0 && senses.GetSqrDistanceFromTarget() < (config.MeleeRange * config.MeleeRange))
            stateMachine.ChangeState(enemy.MeleeAttackState);
    }
    public override void FixedUpdate()
    {
        enemy.FaceTarget();
        switch (currentStance)
        {
            case Stance.OrbitRight:
                enemy.StrafeTarget(config.WalkSpeed);
                break;

            case Stance.OrbitLeft:
                enemy.StrafeTarget(-config.WalkSpeed);
                break;

            case Stance.Stay:
                enemy.StopMovement();
                break;
        }
    }
    private void ChooseNewStance()
    {
        Stance newStance;
        do
        {
            newStance = (Stance)Random.Range(0, 3);
        }
        while (currentStance == newStance);
        currentStance = newStance;
        Debug.Log(currentStance.ToString()); 
        stanceTimer = Random.Range(config.MinStanceTime, config.MaxStanceTime);
    }
    private void SetNextAttack() => attackTimer = Random.Range(config.MinAttackCooldown, config.MaxAttackCooldown);
}
