using System;
using UnityEngine;

[Serializable]
public class InteractibleEventCondition
{
    [SerializeField] private String flagName;
    [SerializeField] private int flagValue;

    public string FlagName => flagName;

    public int FlagValue => flagValue;
}
