using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerMenu : MonoBehaviour
{
    // Chama cena de créditos
    public void Creditos() {
        SceneManager.LoadScene("Creditos");
    }
    // Chama cena 1
     public void Iniciar() {
        SceneManager.LoadScene("Floresta");
    }
    // Sai do jogo
     public void Sair() {
        print("Saindo do jogo");
        Application.Quit();
    }
}
