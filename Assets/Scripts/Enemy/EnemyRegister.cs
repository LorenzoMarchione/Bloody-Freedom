using UnityEngine;

public class EnemyRegister : MonoBehaviour
{
    [SerializeField] private KillAllEnemiesObjective killAllEnemiesObjective;
    private Health health;

    private void Awake() => health = GetComponent<Health>();
    private void OnEnable() => health.OnDeath += HandleDeath;
    private void OnDisable() => health.OnDeath -= HandleDeath;
    private void Start()
    { 
        if(killAllEnemiesObjective != null)
            killAllEnemiesObjective.RegisterEnemy(); 
    }
    private void HandleDeath() => killAllEnemiesObjective.EnemyKilled();
}
