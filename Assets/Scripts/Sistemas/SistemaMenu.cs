using UnityEngine;
using UnityEngine.SceneManagement;

public class SistemaMenu : MonoBehaviour
{
    public void Jogar()
    {
        SceneManager.LoadScene("CenaJogo");
    }
    public void MontarBaralho()
    {
        SceneManager.LoadScene("CenaBaralho");
    }
    public void VoltarProMenuPrincipal()
    {
        SceneManager.LoadScene("CenaMenuPrincipal");
    }

    public void Sair()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
