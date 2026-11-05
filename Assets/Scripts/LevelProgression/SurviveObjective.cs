using UnityEngine;

public class SurviveObjective : LevelObjective
{
    [SerializeField] private float duration = 30f;

    private float elapsedTime;

    public override string Description => "Survive";

    public override string ProgressText =>
        $"{FormatTime(elapsedTime)} / {FormatTime(duration)}";

    public override float Progress =>
        duration > 0
            ? Mathf.Clamp01(elapsedTime / duration)
            : 1f;

    private void Update()
    {
        if (isCompleted)
            return;

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= duration)
        {
            elapsedTime = duration;
            Complete();
            return;
        }

        NotifyProgressChanged();
    }

    private string FormatTime(float seconds)
    {
        int totalSeconds = Mathf.FloorToInt(seconds);

        int minutes = totalSeconds / 60;
        int remainingSeconds = totalSeconds % 60;

        return $"{minutes:00}:{remainingSeconds:00}";
    }
}