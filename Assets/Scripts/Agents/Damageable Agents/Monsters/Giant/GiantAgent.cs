using UnityEngine;
using System.Collections.Generic;

public class GiantAgent : MonsterAgent
{
    #region MonsterAgent implementation

    protected override void Init ()
    {   
        base.Init();

        ChangeState<GiantIdleState>();
	}

    protected override List<AgentComponent> InitComponents()
    {
        return new List<AgentComponent>
        {
            new AttackAnimation(),
            new MapBlockHolder(),
            new AgentHealth(this),
            new GiantAttackComponent(this)
        };
    }

    protected override AgentState[] InitStates()
    {
        // all available states will be inserted in this array
        return new MonsterState[]
            {
                new GiantIdleState(this),
                new GiantAlertState(this),
                new GiantAttackState(this),
                new GiantHuntState(this),
                new GiantRageState(this), 
                new GiantBlindState(this)
                // next state
                // ...
            };
    }

    #endregion
}
