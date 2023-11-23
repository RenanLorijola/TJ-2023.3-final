
using System;
using UnityEngine;

public class TouchEventTrigger : MonoBehaviour
{
    [SerializeField] private InteractibleEventController eventController;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        eventController.TriggerEvent();
    }

    private void OnValidate()
    {
        if (eventController == null)
        {
            eventController = GetComponent<InteractibleEventController>();
        }
    }
}
