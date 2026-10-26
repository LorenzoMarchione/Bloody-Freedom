using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour
{
    public static ProgressionManager Instance { get; private set; }

    [SerializeField] private List<string> levels;
    [SerializeField] private string victoryScene;

    private int currentLevelIndex = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGame()
    {
        currentLevelIndex = 0;
        LoadCurrentLevel();
    }

    private void LoadCurrentLevel()
    {
        SceneManager.LoadScene(levels[currentLevelIndex]);
    }

    public void CompleteLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= levels.Count)
        {
            SceneManager.LoadScene(victoryScene);
            return;
        }

        LoadCurrentLevel();
    }
}