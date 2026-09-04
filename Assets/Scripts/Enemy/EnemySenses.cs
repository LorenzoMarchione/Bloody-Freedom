using UnityEngine;

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
    public int TargetDirectionFromRange()
    {
        float minDistance = config.TargetDistance - config.DistanceRange;
        float maxDistance = config.TargetDistance + config.DistanceRange;

        float sqrMinDistance = minDistance * minDistance;
        float sqrMaxDistance = maxDistance * maxDistance;

        float sqrDistanceFromTarget = GetSqrDistanceFromTarget();
        if (sqrDistanceFromTarget < sqrMinDistance)
            return -1;
        else if(sqrDistanceFromTarget > sqrMaxDistance)
            return 1;
        return 0;
    }
}
