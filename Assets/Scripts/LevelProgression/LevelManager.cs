using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelObjective objective;
    [SerializeField] private LevelObjectiveUI objectiveUI;

    private void OnEnable()
    {
        objective.OnCompleted += HandleLevelCompleted;
    }
    private void Start()
    {
        objectiveUI.Initialize(objective);
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