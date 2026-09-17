using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public void VolverAlMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}