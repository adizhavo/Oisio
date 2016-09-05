using UnityEngine;
using System.Collections.Generic;

public static class EventObserver 
{
    private static List<EventTrigger> subscribedAction = new List<EventTrigger>();

    public static void Subscribe(EventTrigger subject)
    {
        subscribedAction.Add(subject);
    }

    public static void Unsubcribe(EventTrigger subject)
    {
        if (subscribedAction.Contains(subject))
            subscribedAction.Remove(subject);
    }

    public static void SearchVisibleEvent(EventListener listener)
    {
        EventTrigger highpriorityAction = null;
        float minDistance = Mathf.Infinity;

        foreach(EventTrigger act in subscribedAction)
        {
            float distance = Vector3.Distance(listener.WorlPos, act.WorlPos);
            bool isVisible = !Physics.Linecast(listener.WorlPos, act.WorlPos) && distance < listener.VisibilityRadius;

            if(isVisible && distance < minDistance && (highpriorityAction == null || highpriorityAction.Priority <= act.Priority))
            {
                highpriorityAction = act;
                minDistance = distance;
            }
        }

        if (highpriorityAction != null)
        {
            Debug.DrawLine(listener.WorlPos, highpriorityAction.WorlPos, Color.cyan);
            listener.Notify(highpriorityAction);

            if (highpriorityAction.oneShot)
                subscribedAction.Remove(highpriorityAction);
        }
    }
}