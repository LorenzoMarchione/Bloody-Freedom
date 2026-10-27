using UnityEngine;

public class SurviveObjective : LevelObjective
{
    [SerializeField] private float duration = 60f;

    private float timer;

    private void Update()
    {
        if (isCompleted)
            return;

        timer += Time.deltaTime;

        if (timer >= duration)
            Complete();
    }
}