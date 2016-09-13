using UnityEngine;

public class JumperAgent : GiantAgent 
{
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
            new JumperAlertState(this)
        };
    }

    #endregion
}
