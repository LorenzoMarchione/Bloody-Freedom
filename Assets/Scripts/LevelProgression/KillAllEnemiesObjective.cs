using UnityEngine;

public class KillAllEnemiesObjective : LevelObjective
{
    private int totalEnemies = 0;

    private int killedEnemies = 0;

    public override string Description => "Defeat all enemies";

    public override string ProgressText =>
        $"{killedEnemies}/{totalEnemies}";

    public override float Progress =>
        totalEnemies > 0
            ? (float)killedEnemies / totalEnemies
            : 0f;

    public void EnemyKilled()
    {
        if (isCompleted)
            return;

        killedEnemies++;

        if (killedEnemies >= totalEnemies)
        {
            Complete();
            return;
        }

        NotifyProgressChanged();
    }
    public void RegisterEnemy()
    {
        totalEnemies++;
    }
}