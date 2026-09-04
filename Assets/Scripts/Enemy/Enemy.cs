using UnityEngine;
using static UnityEngine.Rendering.STP;

public class Enemy : MonoBehaviour
{
    //components
    public Rigidbody RigidBody {  get; private set; }
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemySenses Senses { get; private set; }
    public Animator Anim {  get; private set; }
    public EnemyConfig EnemyConfig { get => EConfig; }
    public Transform Player { get => player; }
    [SerializeField] private Transform player;
    [SerializeField] private EnemyConfig EConfig;

    //States
    public EnemyChaseState ChaseState { get; private set; }
    public EnemyPositioningState PositionState { get; private set; }
    public EnemyAttackState AttackState { get; private set; }
    public EnemyHitState HitState { get; private set; }
    public EnemyDeadState DeadState { get; private set; }

    //useful data
    private Vector3 targetDirection;
    public Vector3 TargetDirection { get => targetDirection; }
    private void Awake()
    {
        StateMachine = new EnemyStateMachine();
        RigidBody = GetComponent<Rigidbody>();
        Senses = GetComponent<EnemySenses>();
        Anim = GetComponent<Animator>();

        ChaseState = new EnemyChaseState(this);
        PositionState = new EnemyPositioningState(this);
        AttackState = new EnemyAttackState(this);
        HitState = new EnemyHitState(this);
        DeadState = new EnemyDeadState(this);
    }
    public void Initialize(Transform player)
    {
        this.player = player;
        StateMachine.Initialize(ChaseState);
    }
    private void Start()
    {
        StateMachine.Initialize(ChaseState);
    }
    private void Update()
    {
        StateMachine.Update();
    }
    private void FixedUpdate()
    {
        SetTargetDirection();
        StateMachine.FixedUpdate();
    }
    public void OnAnimationFinished()
    {
        StateMachine.OnAnimationFinished();
    }
    public void SetTargetDirection()
    {
        Vector3 direction = Player.transform.position - transform.position;
        direction.y = 0;
        targetDirection = direction.normalized;
    }
    public void FaceTarget()
    {
        if(targetDirection != Vector3.zero) 
            transform.rotation = Quaternion.LookRotation(targetDirection);
    }
    public void StopMovement() => RigidBody.linearVelocity = new Vector3(0, RigidBody.linearVelocity.y, 0);
    public void ChaseTarget(float speed) => RigidBody.linearVelocity = new Vector3 (targetDirection.x * speed, RigidBody.linearVelocity.y, targetDirection.z * speed);
    public void StrafeTarget(float speed)
    {
        Vector3 rightDirection = Vector3.Cross(Vector3.up, targetDirection);
        RigidBody.linearVelocity = new Vector3(rightDirection.x * speed, RigidBody.linearVelocity.y, rightDirection.z * speed);
    }
    public void BasicAttack() => Anim.Play("EnemyAttackTest");
    public void StepIn(float speed, Vector3 direction) => RigidBody.linearVelocity = new Vector3(direction.x * speed, RigidBody.linearVelocity.y, direction.z * speed);

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, EnemyConfig.TargetDistance + EnemyConfig.EntryDistanceRange);
        Gizmos.DrawWireSphere(transform.position, EnemyConfig.TargetDistance - EnemyConfig.EntryDistanceRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, EnemyConfig.TargetDistance + EnemyConfig.ExitDistanceRange);
        Gizmos.DrawWireSphere(transform.position, EnemyConfig.TargetDistance - EnemyConfig.ExitDistanceRange);
    }
}
