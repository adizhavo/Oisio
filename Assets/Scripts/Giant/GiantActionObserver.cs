using UnityEngine;
using System.Collections.Generic;

public static class GiantActionObserver 
{
    public static List<GiantAction> subscribedAction = new List<GiantAction>();

    public static void CheckForActions(GiantAgent giant)
    {
        GiantAction highpriorityAction = null;
        float minDistance = Mathf.Infinity;

        foreach(GiantAction act in subscribedAction)
        {
            float distance = Vector3.Distance(giant.WorlPos, act.WorlPos);
            bool isVisible = !Physics.Linecast(giant.WorlPos, act.WorlPos) && distance < giant.visibilityRadius;

            if(isVisible && distance < minDistance && (highpriorityAction == null || highpriorityAction.Priority <= act.Priority))
            {
                highpriorityAction = act;
                minDistance = distance;
            }
        }

        if (highpriorityAction != null) giant.Notify(highpriorityAction.actionEvent);
    }
}