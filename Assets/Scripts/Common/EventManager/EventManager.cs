using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This class handle the Event registration and triggering
/// </summary>
public class EventManager : MonoBehaviour {

    private Dictionary<string, Action<EventParam>> eventDictionary;

    private static EventManager eventManager;

    /// <summary>
    /// Instannce of the Event Manager
    /// </summary>
    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, Action<EventParam>>();
        }
    }

    /// <summary>
    /// Subscribe to an event
    /// </summary>
    /// <param name="eventId">The id of the event to be subscribed</param>
    /// <param name="listener">The Method to be executed when the event is raised</param>
    public static void StartListening(string eventId, Action<EventParam> listener)
    {

        Action<EventParam> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventId, out thisEvent))
        {
            thisEvent += listener;
        }
        else
        {
            thisEvent += listener;
            instance.eventDictionary.Add(eventId, thisEvent);
        }
    }

    /// <summary>
    /// Unsubscribe for an event
    /// </summary>
    /// <param name="eventId">The id of the event to be unsubsribed</param>
    /// <param name="listener">THe method that was subsribed to the event</param>
    public static void StopListening(string eventId, Action<EventParam> listener)
    {
        if (eventManager == null) return;
        Action<EventParam> thisEvent;
        if (instance.eventDictionary.TryGetValue(eventId, out thisEvent))
        {
            thisEvent -= listener;
        }
    }

    /// <summary>
    /// Trigger the Event
    /// </summary>
    /// <param name="eventId">The id of the event to be unsubsribed</param>
    /// <param name="listener">THe method that was subsribed to the event</param>
    public static void TriggerEvent(string eventId, EventParam param)
    {  
        Action<EventParam> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventId, out thisEvent))
        {
            thisEvent.Invoke(param);
        }
    }
}
