using UnityEngine;

public class CustomEvent : EventTrigger
{
    private EventSubject eventSubject;
    private int priority;
    private bool oneShotEvent;

    public CustomEvent(Vector3 worldPos, EventSubject eventSubject, int priority, bool oneShotEvent = false)
    {
        this.WorlPos = worldPos;
        this.eventSubject = eventSubject;
        this.priority = priority;
        this.oneShotEvent = oneShotEvent;
    }

    #region EventTrigger implementation
    public bool oneShot
    {
        get 
        {
            return oneShotEvent;
        }
    }

    public int Priority
    {
        get
        {
            return priority;
        }
    }
    public EventSubject subject
    {
        get
        {
            return eventSubject;
        }
    }

    public Vector3 WorlPos { set; get; }
    #endregion
}