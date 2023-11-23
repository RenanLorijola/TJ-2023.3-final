using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class InteractibleEvent
{
    [SerializeField] private string eventName;
    [SerializeField] private List<InteractibleEventCondition> conditions;
    [SerializeField] private List<InteractibleEventAction> actions;


    public string EventName => eventName;
    public List<InteractibleEventCondition> Conditions => conditions;

    public List<InteractibleEventAction> Actions => actions;


}