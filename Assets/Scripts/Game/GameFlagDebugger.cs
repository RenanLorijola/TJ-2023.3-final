using System;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

public class GameFlagDebugger : MonoBehaviour
{

    public bool setOnStart;
    public List<GameFlag> debugFlags;

    private void Start()
    {
        if (setOnStart)
        {
            SetFlags();
        }
    }

    [ButtonMethod]
    private void SetFlags()
    {
        foreach (var flag in debugFlags)
        {
            GameManager.Singleton.SetFlag(flag.FlagName, flag.Value);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F6))
        {
            SetFlags();
        }
    }

    [ButtonMethod]
    private void CopyFlags()
    {
        debugFlags = new List<GameFlag>();
        foreach (var flagName in FindObjectOfType<GameManager>().GameFlagsNames)
        {
            debugFlags.Add(new GameFlag(flagName));
        }
    }
}
