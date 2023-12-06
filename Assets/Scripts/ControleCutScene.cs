using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ControleCutScene : MonoBehaviour
{
  public VideoClip videoClip;

    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = Camera.main.GetComponent<VideoPlayer>();

        // Atribua o vídeo ao VideoPlayer
        videoPlayer.clip = videoClip;

        videoPlayer.loopPointReached += OnVideoFinished;

        // Iniciaa reprodução do vídeo
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Remove o manipulador de eventos para evitar chamadas repetidas
        videoPlayer.loopPointReached -= OnVideoFinished;

        // Chama jogo
        SceneManager.LoadScene("Floresta");
    }
}
