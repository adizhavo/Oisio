using UnityEngine;
using System.Collections.Generic;

public class JumperAgent : GiantAgent 
{
    [Header("Jumper Configuration")]
    public GameObject jumperRoot;
    public float jumpDistance;
    public float heightMultiplier;
    public float jumpSpeed;

    #region GiantAgent implementation

    protected override void Init()
    {
        base.Init();
    }

    protected override AgentState[] InitStates()
    {
        return new AgentState[]
        {
            new JumperIdleState(this),
            new JumperAlertState(this), 
            new JumperAttackState(this), 
            new GiantBlindState(this)
        };
    }

    #endregion
}
