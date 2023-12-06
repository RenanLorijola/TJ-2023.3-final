using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyBox;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractibleEventController : MonoBehaviour
{
    [SerializeField]
    private List<InteractibleEvent> events;


    private List<InteractibleEventAction> _actionQueue = new List<InteractibleEventAction>();
    private InteractibleEventAction _currentAction;
    private InteractibleEventAction _previousAction;
    private int _skipActions = 0;

    public void TriggerEvent()
    {
        foreach (var e in events)
        {
            if (CheckConditions(e))
            {
                RunActions(e);
                break;
            }
        }
    }

    public bool HasAvailableEvent()
    {
        foreach (var e in events)
        {
            if (CheckConditions(e))
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckConditions(InteractibleEvent interactibleEvent)
    {
        foreach (var condition in interactibleEvent.Conditions)
        {
            if (GameManager.Singleton.GetFlag(condition.FlagName) != condition.FlagValue)
            {
                return false;
            }
        }

        return true;
    }

    private void RunActions(InteractibleEvent interactibleEvent)
    {
        GameManager.Singleton.PlayingEvent = true;
        _previousAction = null;
        _actionQueue.Clear();
        _actionQueue.AddRange(interactibleEvent.Actions);
        _skipActions = 0;
        Debug.Log("Starting Event - " + interactibleEvent.EventName);
        NextAction();
    }

    private void NextAction()
    {
        if (_actionQueue.Count == 0)
        {
            if (_previousAction == null || _previousAction.Type != InteractibleEventAction.ActionType.Event)
            {
                GameManager.Singleton.PlayingEvent = false;
            }

            return;
        }
       var action = _actionQueue.First();
       _actionQueue.RemoveAt(0);
       
       // do action
       _currentAction = action;
       if (_skipActions > 0)
       {
           _skipActions--;
           NextAction();
           return;
       }
       switch (action.Type)
       {
           case InteractibleEventAction.ActionType.Dialog:
               PlayDialogAction(action);
               break;
           case InteractibleEventAction.ActionType.SetFlag:
               PlaySetFlagAction(action);
               break;
           case InteractibleEventAction.ActionType.Wait:
               PlayWaitAction(action);
               break;
           case InteractibleEventAction.ActionType.FadeInOut:
               PlayFadeInOutAction(action);
               break;
           case InteractibleEventAction.ActionType.CustomAction:
               PlayCustomAction(action);
               break;
           case InteractibleEventAction.ActionType.FadeBackgroundMusic:
               PlayFadeMusicAction(action);
               break;
           case InteractibleEventAction.ActionType.SetBackgroundMusic:
               PlaySetBackgroundMusicAction(action);
               break;
           case InteractibleEventAction.ActionType.PlayAudioEffect:
               PlaySoundEffectAction(action);
               break;
           case InteractibleEventAction.ActionType.Event:
               PlayEventAction(action);
               break;
           case InteractibleEventAction.ActionType.LoadScene:
               LoadSceneAction(action);
               break;
           default:
               NextAction();
               break;
       }
    }

    private void ActionEnd(bool success, InteractibleEventAction action)
    {
        if (_currentAction == null || _currentAction != action)
        {
            return;
        }

        _previousAction = _currentAction;
        _currentAction = null;
        NextAction();
    }



    private void PlayDialogAction(InteractibleEventAction action)
    {
        DialogManager.Singleton.DoEventAction(action, ActionEnd);
    }
    

    private void PlaySetFlagAction(InteractibleEventAction action)
    {
        GameManager.Singleton.SetFlag(action.FlagName, action.FlagValue);
        ActionEnd(true, action);
    }

    private void PlayWaitAction(InteractibleEventAction action)
    {
        StartCoroutine(WaitActionCoroutine(action));
    }

    private IEnumerator WaitActionCoroutine(InteractibleEventAction action)
    {
        yield return new WaitForSeconds(action.WaitSeconds);
        ActionEnd(true, action);
    }

    private void PlayFadeInOutAction(InteractibleEventAction action)
    {
        GameHudManager.Singleton.Fade(action.FadeFromAlpha, action.FadeToAlpha, action.FadeSeconds, () => ActionEnd(true, action));
    }
    
    private void PlayFadeMusicAction(InteractibleEventAction action)
    {
        SoundManager.Singleton.FadeBackgroundMusicVolume(action.FadeFromVolume, action.FadeToVolume, action.FadeVolumeSeconds, null);
        ActionEnd(true, action);
    }
    
    private void PlaySetBackgroundMusicAction(InteractibleEventAction action)
    {
        SoundManager.Singleton.PlayBackgroundMusic(action.AudioClip, action.AudioVolume);
        ActionEnd(true, action);
    }
    
    private void PlaySoundEffectAction(InteractibleEventAction action)
    {
        SoundManager.Singleton.PlaySoundEffect(action.AudioClip, action.AudioVolume);
        ActionEnd(true, action);

    }
    
    private void PlayEventAction(InteractibleEventAction action)
    {
        action.NextEvent.TriggerEvent();
        ActionEnd(true, action);
    }
    
    private void LoadSceneAction(InteractibleEventAction action)
    {
        ActionEnd(true, action);
        SceneManager.LoadScene(action.SceneName);
    }

    private void PlayCustomAction(InteractibleEventAction action)
    {
        switch (action.CustomActionName)
        {
            case "testCustomAction":
                Debug.Log("This is a test custom action");
                ActionEnd(true, action);
                break;
            default:
                Debug.LogWarning("Custom action not found: " + action.CustomActionName);
                ActionEnd(true, action);
                break;
                
        }
    }
}