using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class GiantAgent : MonsterAgent
{
    [Header("Giant FX Configuration")]
    public GameObjectPool AttackEffect;

    #region MonsterAgent implementation

    protected override void Init ()
    {   
        base.Init();

        monsterAnim = new MonsterAnimation(GetComponent<Animator>(), "PrepareAttack", "PreSpeed", "RecoverAttack", "RecoverSpeed");
        ChangeState<GiantIdleState>();
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

    public override void Attack()
    {
        base.Attack();
        CameraShake.Instance.StartShake(ShakeType.GiantAttack);
        DisplayAttackEffect();
    }

    #endregion

    private void DisplayAttackEffect()
    {
        PooledObjects.Instance.RequestGameObject(AttackEffect).transform.position = new Vector3(transform.position.x, 0f, transform.position.z);  
    }
}
