using UnityEngine;
using System.Collections;

public abstract class AgentComponent 
{
    protected CharacterAgent agent;

    public AgentComponent(CharacterAgent agent)
    {
        this.agent = agent;
    }

    public abstract void FrameFeed();
}
