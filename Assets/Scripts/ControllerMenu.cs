using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerMenu : MonoBehaviour
{
    // Chama cena de créditos
    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }
    // Chama cena 1
    public void Iniciar()
    {
        SceneManager.LoadScene("Cutscene");
    }
    // Sai do jogo
    public void Sair()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
