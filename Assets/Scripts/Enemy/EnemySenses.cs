using UnityEngine;

public enum TargetRangeStatus
{
    TooClose,
    InRange,
    TooFar
}
public class EnemySenses : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    private EnemyConfig config;
    private Transform player;

    private void Start()
    {
        config = enemy.EnemyConfig;
        player = enemy.Player;
    }
    public float GetSqrDistanceFromTarget()
    {
        return (player.position - transform.position).sqrMagnitude;
    }
    public TargetRangeStatus TargetRange(float distanceRange)
    {
        if (player == null)
        {
            // Retornamos un valor grande o infinito para que el enemigo 
            // entienda que el objetivo ya no está a rango
            return TargetRangeStatus.TooFar;
        }
        float minDistance = config.TargetDistance - distanceRange;
        float maxDistance = config.TargetDistance + distanceRange;

        float sqrMinDistance = minDistance * minDistance;
        float sqrMaxDistance = maxDistance * maxDistance;

        float sqrDistanceFromTarget = GetSqrDistanceFromTarget();

        if (sqrDistanceFromTarget < sqrMinDistance) return TargetRangeStatus.TooClose;
        if (sqrDistanceFromTarget > sqrMaxDistance  ) return TargetRangeStatus.TooFar;
        return TargetRangeStatus.InRange;
    }
}
