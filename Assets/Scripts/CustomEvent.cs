using UnityEngine;

public class CustomEvent : EventTrigger
{
    private EventSubject eventSubject;
    private int priority;
    private bool oneShotEvent;
    private float activeTime;

    private float creationTime;
    private bool expired = false;

    public CustomEvent(Vector3 worldPos, EventSubject eventSubject, int priority, float activeTime = Mathf.Infinity, bool oneShotEvent = false)
    {
        this.WorlPos = worldPos;
        this.eventSubject = eventSubject;
        this.priority = priority;
        this.oneShotEvent = oneShotEvent;
        this.activeTime = activeTime;

        creationTime = Time.timeSinceLevelLoad;
    }

    #region EventTrigger implementation
    public bool hasExpired
    {
        get 
        {
            return Time.timeSinceLevelLoad - creationTime > activeTime;
        }
    }

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