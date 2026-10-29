using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    // Public method you can hook up to your button with a delay
    public void LoadSceneWithDelay(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        // Wait for 0.5 seconds (allows the click sound to play fully)
        yield return new WaitForSeconds(0.5f);

        // Then load the scene
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneInstant(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}