using UnityEngine;
using System.Collections.Generic;

public class JumperAgent : MonsterAgent 
{
    [Header("Jumper Configuration")]
    public GameObject jumperRoot;
    public float jumpDistance;
    public float heightMultiplier;
    public float jumpSpeed;

    #region MonsterAgent implementation

    protected override void Init()
    {
        base.Init();

        ChangeState<JumperIdleState>();
    }

    protected override List<AgentComponent> InitComponents()
    {
        return new List<AgentComponent>
        {
            new AttackAnimation(),
            new MapBlockHolder(),
            new AgentHealth(this),
            new MonsterAttackComponent(this)
        };
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

    public override void Attack()
    {
        base.Attack();
        CameraShake.Instance.StartShake(ShakeType.JumperAttack);
    }

    #endregion
}
