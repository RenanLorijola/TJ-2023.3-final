using System;
using System.Threading;
using UnityEditor;
using UnityEngine;

public abstract class Interactible : MonoBehaviour
{
    
    public void Interact()
    {
        if (GameManager.Singleton.PlayingEvent || !AbleToInteract())
        {
            return;
        }
        
        Debug.Log("Interact");
        OnInteract();
    }

    public virtual bool AbleToInteract()
    {
        return !GameManager.Singleton.PlayingEvent;
    }

    protected abstract void OnInteract();

    protected virtual void OnValidate()
    {
        
    }
}
