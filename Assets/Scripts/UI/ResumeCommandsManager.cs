using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeCommandsManager : MonoBehaviour
{

    public GameObject resumeCommands;
    public bool isOpen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            if (isOpen)
            {
                resumeCommands.SetActive(false);
                isOpen = false;
            }
            else
            {
                resumeCommands.SetActive(true);
                isOpen = true;
            }
        }
    }
}
