using System;
using System.Collections;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }

    [SerializeField] private List<String> gameFlags;

    public bool PlayingEvent { get; set; } = false;

    public List<string> GameFlagsNames => gameFlags;

    private Dictionary<String, GameFlag> _flagsDictionary = new Dictionary<string, GameFlag>();
    private event Action FlagChangedEvent;

    private void Awake()
    {
        Singleton = this;
        SetupFlags();
    }

    
    // -------------------------------- FLAGS -------------------------------//

    private void SetupFlags()
    {
        for (int i = 0; i < gameFlags.Count; i++)
        {
            string f = gameFlags[i];
            _flagsDictionary.Add(f, new GameFlag(f));
        }
    }
    public int GetFlag(string flagName)
    {
        if (!_flagsDictionary.ContainsKey(flagName))
        {
            throw new Exception("Missing flag with name: " + flagName);
        }
        return _flagsDictionary[flagName].Value;
    }

    public void SetFlag(string flagName, int value)
    {
        if (!_flagsDictionary.ContainsKey(flagName))
        {
            throw new Exception("Missing flag with name: " + flagName);
        }
        _flagsDictionary[flagName].Value = value;
        NotifyFlagsChanged();
    }

    public void NotifyFlagsChanged()
    {
        if (FlagChangedEvent != null)
        {
            FlagChangedEvent();
        }
    }

    public void RegisterFlagsChangedCallback(Action a)
    {
        FlagChangedEvent += a;
    }

    public void UnregisterFlagsChangedCallback(Action a)
    {
        FlagChangedEvent -= a;
    }
    
    // -------------------------------- Editor -------------------------------//

    [ButtonMethod()]
    public void DebugFlags()
    {
        string dFlags = "";
        foreach (var key in _flagsDictionary.Keys)
        {
            dFlags += String.Format("{0} - {1}\n", key, _flagsDictionary[key].Value);
        }
        Debug.Log(dFlags);
    }

    [ButtonMethod()]
    public void TestRender()
    {
        Debug.Log(UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset);
    }


}
