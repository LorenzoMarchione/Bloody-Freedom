using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//public class PlayerGameOver : MonoBehaviour
public class HUDGameOver : MonoBehaviour
{
    public static string EscenaParaReintentar { get; private set; }

    [SerializeField] private Health playerHealth;
    [SerializeField] private string escenaGameOver = "GameOver";
    [SerializeField] private float espera = 3f;

    private bool muriendo;

    private void Start()
    {
        if (playerHealth != null)
            playerHealth.OnDeath += AlMorir;
    }

    //private void AlMorir()
    //{
    //    if (muriendo) return;

    //    muriendo = true;
    //    StartCoroutine(AbrirGameOver());
    //}

    private void AlMorir()
    {
        if (muriendo) return;

        muriendo = true;

        // Recordar el nivel antes de abrir Game Over.
        EscenaParaReintentar = playerHealth.gameObject.scene.path;

        StartCoroutine(AbrirGameOver());
    }

    private IEnumerator AbrirGameOver()
    {
        yield return new WaitForSecondsRealtime(espera);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(escenaGameOver);
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnDeath -= AlMorir;
    }
}