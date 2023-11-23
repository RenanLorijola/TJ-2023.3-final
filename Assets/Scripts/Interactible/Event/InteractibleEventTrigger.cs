using UnityEngine;

public class InteractibleEventTrigger : Interactible
{
    
    [SerializeField] private InteractibleEventController eventController;
    protected override void OnInteract()
    {
        eventController.TriggerEvent();
    }

    protected override void OnValidate()
    {
        base.OnValidate();
        if (eventController == null)
        {
            eventController = GetComponent<InteractibleEventController>();
        }
    }
}
