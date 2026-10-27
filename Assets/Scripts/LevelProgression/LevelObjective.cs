using System;
using UnityEngine;

public abstract class LevelObjective : MonoBehaviour
{
    public event Action OnCompleted;

    protected bool isCompleted;

    protected void Complete()
    {
        if (isCompleted)
            return;

        isCompleted = true;
        OnCompleted?.Invoke();
    }
}