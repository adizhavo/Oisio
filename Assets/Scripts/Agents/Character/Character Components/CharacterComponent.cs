﻿using UnityEngine;

public abstract class CharacterComponent : AgentComponent
{
    protected CharacterAgent agent;

    public CharacterComponent(CharacterAgent agent)
    {
        this.agent = agent;
    }

    #region AgentComponent implementaion

    public abstract void FrameFeed();

    #endregion
}