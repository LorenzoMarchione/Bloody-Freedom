using UnityEngine;

public class KillAllEnemiesObjective : LevelObjective
{
    private int remainingEnemies;

    public void RegisterEnemy()
    {
        remainingEnemies++;
    }

    public void EnemyKilled()
    {
        remainingEnemies--;

        if (remainingEnemies <= 0)
            Complete();
    }
}