using System;
using UnityEngine;

[Serializable]
public class GameFlag
{
    public GameFlag()
    {
        
    }

    public GameFlag(String flagName)
    {
        this.flagName = flagName;
    }
    
    [SerializeField] private String flagName;
    [SerializeField] private int value;


    public string FlagName => flagName;

    public int Value
    {
        get => value;
        set => this.value = value;
    }
}
