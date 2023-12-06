using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSkip : MonoBehaviour
{
    public void ChamaVila() {
        SceneManager.LoadScene("Vila_com_player");
    }
    
}
