using UnityEngine;
using static UnityEngine.Rendering.STP;

public class Enemy : MonoBehaviour
{
    //components
    public Rigidbody RigidBody {  get; private set; }
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemySenses Senses { get; private set; }
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
    private void Awake()
    {
        StateMachine = new EnemyStateMachine();
        RigidBody = GetComponent<Rigidbody>();
        Senses = GetComponent<EnemySenses>();

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
        StateMachine.FixedUpdate();
    }
    public void OnAnimationFinished()
    {
        StateMachine.OnAnimationFinished();
    }
    public Vector3 GetTargetDirection()
    {
        Vector3 direction = Player.transform.position - transform.position;
        direction.y = 0;
        return direction.normalized;
    }
    public void FaceTarget()
    {
        Vector3 direction = GetTargetDirection();
        if(direction != Vector3.zero) 
            transform.rotation = Quaternion.LookRotation(direction);
    }
    public void ChaseTarget(float speed) 
    { 
        Vector3 direction = GetTargetDirection();
        RigidBody.linearVelocity = new Vector3 (direction.x * speed, RigidBody.linearVelocity.y, direction.z * speed);
    }
    public void StrafeTarget(float speed)
    {
        Vector3 rightDirection = Vector3.Cross(Vector3.up, GetTargetDirection());
        RigidBody.linearVelocity = new Vector3(rightDirection.x * speed, RigidBody.linearVelocity.y, rightDirection.z * speed);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, EnemyConfig.TargetDistance + EnemyConfig.DistanceRange);
        Gizmos.color = Color.orange;
        Gizmos.DrawWireSphere(transform.position, EnemyConfig.TargetDistance - EnemyConfig.DistanceRange);
    }
}
