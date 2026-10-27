using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerGameOver : MonoBehaviour
{
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.OnDeath += GameOver;
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void OnDestroy()
    {
        health.OnDeath -= GameOver;
    }
}