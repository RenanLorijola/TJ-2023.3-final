using System;
using System.Collections.Generic;
using MyBox;
using UnityEngine;

[Serializable]
public class InteractibleEventAction
{
    public enum ActionType
    {
        Dialog, SetFlag, Wait, FadeInOut, CustomAction, FadeBackgroundMusic, PlayAudioEffect, SetBackgroundMusic, Event
    }
    
    [SerializeField] private string actionName;
    [SerializeField] private ActionType type;

    public string ActionName => actionName;
    public ActionType Type => type;
    
    //Dialog
    [ConditionalField(nameof(type), false, ActionType.Dialog)][SerializeField] private CollectionWrapper<DialogData> dialogs;

    public CollectionWrapper<DialogData> Dialogs => dialogs;
    
    //SetFlag
    [ConditionalField(nameof(type), false, ActionType.SetFlag)][SerializeField] private string flagName;
    [ConditionalField(nameof(type), false, ActionType.SetFlag)][SerializeField] private int flagValue;

    public string FlagName => flagName;

    public int FlagValue => flagValue;
    
    
    // Wait
    [ConditionalField(nameof(type), false, ActionType.Wait)][SerializeField] private float waitSeconds;

    public float WaitSeconds => waitSeconds;
    
    // FadeInOut
    [ConditionalField(nameof(type), false, ActionType.FadeInOut)][SerializeField] private float fadeFromAlpha;
    [ConditionalField(nameof(type), false, ActionType.FadeInOut)][SerializeField] private float fadeToAlpha;
    [ConditionalField(nameof(type), false, ActionType.FadeInOut)][SerializeField] private float fadeSeconds;

    public float FadeFromAlpha => fadeFromAlpha;

    public float FadeToAlpha => fadeToAlpha;

    public float FadeSeconds => fadeSeconds;
    
    //Custom
    [ConditionalField(nameof(type), false, ActionType.CustomAction)][SerializeField] private string customActionName;

    public string CustomActionName => customActionName;
    
    // Fade Music
    [ConditionalField(nameof(type), false, ActionType.FadeBackgroundMusic)][SerializeField] private float fadeFromVolume;
    [ConditionalField(nameof(type), false, ActionType.FadeBackgroundMusic)][SerializeField] private float fadeToVolume;
    [ConditionalField(nameof(type), false, ActionType.FadeBackgroundMusic)][SerializeField] private float fadeVolumeSeconds;

    public float FadeFromVolume => fadeFromVolume;

    public float FadeToVolume => fadeToVolume;

    public float FadeVolumeSeconds => fadeVolumeSeconds;
    
    
    // Audio
    [ConditionalField(nameof(type), false, ActionType.PlayAudioEffect, ActionType.SetBackgroundMusic)][SerializeField] private AudioClip audioClip;
    [ConditionalField(nameof(type), false, ActionType.PlayAudioEffect, ActionType.SetBackgroundMusic)][SerializeField] private float audioVolume;

    public AudioClip AudioClip => audioClip;

    public float AudioVolume => audioVolume;
    
    // Event
    [ConditionalField(nameof(type), false, ActionType.Event)][SerializeField] private InteractibleEventController nextEvent;

    public InteractibleEventController NextEvent => nextEvent;
    

}
