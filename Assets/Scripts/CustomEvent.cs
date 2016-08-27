using UnityEngine;

public class CustomEvent : EventTrigger
{
    private EventSubject eventSubject;
    private int priority;

    public CustomEvent(Vector3 worldPos, EventSubject eventSubject, int priority)
    {
        this.WorlPos = worldPos;
        this.eventSubject = eventSubject;
        this.priority = priority;
    }

    #region EventTrigger implementation
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