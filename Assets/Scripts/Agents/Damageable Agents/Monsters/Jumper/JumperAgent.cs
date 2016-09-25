using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class JumperAgent : MonsterAgent 
{
    [Header("Jumper Configuration")]
    public GameObject jumperRoot;
    public float jumpDistance;
    public float heightMultiplier;
    public float jumpSpeed;

    [Header("Jumper FX Configuration")]
    public GameObjectPool AttackEffect;

    #region MonsterAgent implementation

    protected override void Init()
    {
        base.Init();

        monsterAnim = new MonsterAnimation(GetComponent<Animator>(), "PrepareAttack", "PreSpeed", "RecoverAttack", "RecoverSpeed");
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
        DisplayAttackEffect();
    }

    #endregion

    private void DisplayAttackEffect()
    {
        PooledObjects.Instance.RequestGameObject(AttackEffect).transform.position = new Vector3(transform.position.x, 0f, transform.position.z);  
    }
}
