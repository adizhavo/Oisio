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

    public override void PrepareAttack(float preparationTime)
    {
        RequestComponent<AttackAnimation>().PrepareAttack(attackGameObject, preparationTime);
    }

    public override void Attack()
    {
        RequestComponent<AttackAnimation>().Attack(attackGameObject);
        RequestComponent<MonsterAttackComponent>().Attack<CharacterAgent>();
        CameraShake.Instance.StartShake(ShakeType.JumperAttack);
    }

    public override void RecoverAttack(float recoverTime)
    {
        RequestComponent<AttackAnimation>().Recover(attackGameObject, recoverTime);
    }

    #endregion
}
