using System;
using System.Collections.Generic;
using UnityEngine;
public class SpriteChangerFlagListener : MonoBehaviour
{
    [Serializable]
    public class SpriteRule
    {
        [SerializeField] public string ruleName;
        [SerializeField] public List<InteractibleEventCondition> conditions;
        [SerializeField] public Sprite sprite;
    }
    
    
    [SerializeField] private List<SpriteRule> spriteRules;
    [SerializeField] private SpriteRenderer spriteRenderer;


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
        CheckRules();
    }

    private void CheckRules()
    {
        foreach (var rule in spriteRules)
        {
            bool activeRule = true;
            foreach (var condition in rule.conditions)
            {
                if (GameManager.Singleton.GetFlag(condition.FlagName) != condition.FlagValue)
                {
                    activeRule = false;
                    break;
                }
            }

            if (activeRule)
            {
                spriteRenderer.sprite = rule.sprite;
                break;
            }
        }
        
    }

    private void OnValidate()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}