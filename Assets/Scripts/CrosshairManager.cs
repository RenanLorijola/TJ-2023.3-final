using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairManager : MonoBehaviour
{
    void Update()
    {
        var child = gameObject.transform.GetChild(0).gameObject;
        if (Time.timeScale == 0f && child.activeSelf)
        {
            child.SetActive(false);
        }
        else if (Time.timeScale == 1f && !child.activeSelf)
        {
            child.SetActive(true);
        }
    }
}
