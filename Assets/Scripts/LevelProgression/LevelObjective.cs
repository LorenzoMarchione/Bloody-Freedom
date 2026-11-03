using System;
using UnityEngine;

public abstract class LevelObjective : MonoBehaviour
{
    public event Action OnCompleted;
    public event Action OnProgressChanged;

    public abstract string Description { get; }
    public abstract string ProgressText { get; }
    public abstract float Progress { get; }

    protected bool isCompleted;

    protected void NotifyProgressChanged()
    {
        OnProgressChanged?.Invoke();
    }
    protected void Complete()
    {
        if (isCompleted)
            return;

        isCompleted = true;

        NotifyProgressChanged();
        OnCompleted?.Invoke();
    }
}