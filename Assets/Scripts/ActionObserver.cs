﻿using UnityEngine;
using System.Collections.Generic;

public static class ActionObserver 
{
    public static List<Action> subscribedAction = new List<Action>();

    public static void CheckForActions(ActionListener listener)
    {
        Action highpriorityAction = null;
        float minDistance = Mathf.Infinity;

        foreach(Action act in subscribedAction)
        {
            float distance = Vector3.Distance(listener.WorlPos, act.WorlPos);
            bool isVisible = !Physics.Linecast(listener.WorlPos, act.WorlPos) && distance < listener.VisibilityRadius;

            if(isVisible && distance < minDistance && (highpriorityAction == null || highpriorityAction.Priority <= act.Priority))
            {
                highpriorityAction = act;
                minDistance = distance;
            }
        }

        if (highpriorityAction != null) listener.Notify(highpriorityAction);
    }
}