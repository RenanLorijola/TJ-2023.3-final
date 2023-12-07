using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public void carregarMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
    public void continuar()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {

            Time.timeScale = Time.timeScale > 0 ? 0f : 1f;
        }
        if (Time.timeScale == 0f && !pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(true);
        }
        else if (Time.timeScale == 1f && pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }
    }
}
