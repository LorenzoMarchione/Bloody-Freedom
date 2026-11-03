using UnityEngine;

public class KillAllEnemiesObjective : LevelObjective
{
    [SerializeField] private int totalEnemies = 10;

    private int killedEnemies;

    public override string Description => "Elimina a todos los enemigos";

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