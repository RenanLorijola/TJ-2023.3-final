using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHudManager : MonoBehaviour
{
    public static GameHudManager Singleton { get; private set; }

    public GameHudManager()
    {
        Singleton = this;
    }

    [SerializeField] private Image fadeOverlayImage;

    [SerializeField] private GameObject interactFeedback;
    
    

    public void Fade(float from, float to, float seconds, Action callback)
    {
        StartCoroutine(FadeCoroutine(from, to, seconds, callback));
    }

    private IEnumerator FadeCoroutine(float from, float to, float seconds, Action callback)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * (1f/seconds);
            Mathf.Clamp01(t);
            float a = Mathf.Lerp(from, to, t);
            Color c = fadeOverlayImage.color;
            fadeOverlayImage.color = new Color(c.r, c.g, c.b, a);
            yield return null;
        }

        if (callback != null)
        {
            callback();
        }
    }

    public void ShowInteract(bool show)
    {
        interactFeedback.SetActive(show);
    }
    
    
}
