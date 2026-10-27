using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelObjective objective;

    private void OnEnable()
    {
        objective.OnCompleted += HandleLevelCompleted;
    }

    private void OnDisable()
    {
        objective.OnCompleted -= HandleLevelCompleted;
    }

    private void HandleLevelCompleted()
    {
        ProgressionManager.Instance.CompleteLevel();
    }
}