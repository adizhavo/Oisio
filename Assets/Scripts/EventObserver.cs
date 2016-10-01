using UnityEngine;
using System.Collections.Generic;

namespace Oisio.Events
{
    // will check for events nerby a listener
    public static class EventObserver 
    {
        private static List<EventTrigger> subscribedAction = new List<EventTrigger>();

        // Events are subscribed or unsubscribed based on varius criteria
        public static void Subscribe(EventTrigger subject)
        {
            subscribedAction.Add(subject);
        }

        public static void Unsubcribe(EventTrigger subject)
        {
            if (subscribedAction.Contains(subject))
                subscribedAction.Remove(subject);
        }

        // is there eny visible event nerby this listener, if yes then notify
        public static void SearchVisibleEvent(EventListener listener)
        {
            EventTrigger highpriorityAction = null;
            float minDistance = Mathf.Infinity;

            List<EventTrigger> expiredEvents = new List<EventTrigger>();

            foreach(EventTrigger act in subscribedAction)
            {
                if (act.hasExpired)
                {
                    expiredEvents.Add(act);
                    continue;
                }

                float distance = Vector3.Distance(listener.WorlPos, act.WorlPos);
                bool isVisible = !Physics.Linecast(listener.WorlPos, act.WorlPos, GameConfig.EVENT_OBSERVER_IGNORE_MASK) && distance < listener.VisibilityRadius;

                if(isVisible && distance < minDistance && (highpriorityAction == null || highpriorityAction.Priority <= act.Priority) && !act.hasExpired)
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

            foreach(EventTrigger exp in expiredEvents)
                Unsubcribe(exp);
            
            expiredEvents = null;
        }
    }
}