using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody RigidBody {  get; private set; }
    public EnemyStateMachine StateMachine { get; private set; }
    public EnemySenses Senses { get; private set; }
    public EnemyConfig EnemyConfig { get => Ec; }
    [SerializeField] private EnemyConfig Ec;

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
        StateMachine = GetComponent<EnemyStateMachine>();
        Senses = GetComponent<EnemySenses>();
    }
}
