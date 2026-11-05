//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class GameOverButtons : MonoBehaviour
//{
//    public void VolverAlMenu()
//    {
//        SceneManager.LoadScene("NewMainMenu" +
//            "");
//    }
//}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("NewMainMenu");
    }

    public void Reintentar()
    {
        string escena = HUDGameOver.EscenaParaReintentar;

        if (string.IsNullOrEmpty(escena))
        {
            Debug.LogWarning(
                "Primero iniciá un combate y morí para probar Retry."
            );
            return;
        }

        if (!Application.CanStreamedLevelBeLoaded(escena))
        {
            Debug.LogError(
                "Agregá la escena de combate a Build Profiles: " + escena
            );
            return;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(escena);
    }
}