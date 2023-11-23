using System.Collections.Generic;
using System.Xml;
using UnityEngine;


public abstract class BaseFlagListener : MonoBehaviour
{
    [SerializeField] private bool not;
    [SerializeField] private List<InteractibleEventCondition> conditions;
    

    protected virtual void Awake()
    {
        GameManager.Singleton.RegisterFlagsChangedCallback(FlagsChanged);
        FlagsChanged();
    }

    protected  virtual void OnDestroy()
    {
        GameManager.Singleton.UnregisterFlagsChangedCallback(FlagsChanged);
    }

    private void FlagsChanged()
    {
        FlagChecked(CheckConditions());
    }

    private bool CheckConditions()
    {
        foreach (var condition in conditions)
        {
            if (GameManager.Singleton.GetFlag(condition.FlagName) != condition.FlagValue)
            {
                return not? true : false;
            }   
        }

         return not? false : true;
    }

    protected abstract void FlagChecked(bool enabled);
}

        
