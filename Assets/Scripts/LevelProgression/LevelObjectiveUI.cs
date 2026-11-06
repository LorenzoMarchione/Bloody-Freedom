using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelObjectiveUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text progressText;
    [SerializeField] private Slider progressBar;

    private LevelObjective objective;

    public void Initialize(LevelObjective levelObjective)
    {
        if (objective != null)
            objective.OnProgressChanged -= UpdateUI;

        objective = levelObjective;

        if (objective == null)
            return;

        objective.OnProgressChanged += UpdateUI;

        progressBar.minValue = 0f;
        progressBar.maxValue = 1f;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (objective == null)
            return;

        descriptionText.text = objective.Description;
        progressText.text = objective.ProgressText;
        progressBar.value = objective.Progress;
    }

    private void OnDestroy()
    {
        if (objective != null)
            objective.OnProgressChanged -= UpdateUI;
    }
}