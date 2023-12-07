using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonRestart : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void BRestart()
    {
        SceneManager.LoadScene("Floresta");
    }

    public void BVMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
