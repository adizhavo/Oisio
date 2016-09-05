using UnityEngine;
using System.Collections.Generic;

public class CharacterAgent : Agent, EventTrigger
{
    [Header("Agent Configuration")]
    public GameObject arrowPrefab;
    public GameObject smokeBombPrefab;

    public bool invertAiming;
    public float sensibility;
    public float shootForce;
    public float collectorRange;
    public float maxStamina;
    public float staminaRegen;

    [Header("Standart dependencies")]
    public Animator characterAnimator;
    public Transform aimerPivot;
    public Transform arrowParent;

    public Inventory characterInventory;

    #region Agent implementation

    protected override void Init()
    {
        base.Init();

        ChangeState<NullAgentState>();
        characterInventory = InitInventory();
        EventObserver.Subscribe(this);
    }

    private void OnDestroy()
    {
        EventObserver.Unsubcribe(this);
    }

    // can be easly changed and configured with a subclass 
    protected override List<AgentComponent> InitComponents()
    {
        return new List<AgentComponent>
            {
                // list all the agent component
                new AgentAttack(this), 
                new AgentSmokeBomb(this),
                new AgentStamina(this),
                new AgentMovement(this),
                new AgentAnimation(this),
                new AgentResourceCollector(this)
            };
    }

    protected override AgentState[] InitStates()
    {
        return new AgentState[]
        {
            new NullAgentState()
        };
    }

    #endregion

    protected virtual Inventory InitInventory()
    {
        Slot arrowSlot = new Slot(CollectableType.Arrow, GameConfig.arrowInventorySize);
        arrowSlot.SetStockItems(GameConfig.initialArrows);

        Slot bombSlot = new Slot(CollectableType.Bomb, GameConfig.smokeBombInvertorySize);
        bombSlot.SetStockItems(GameConfig.initialBombs);

        Inventory characterInventory = new Inventory();
        characterInventory.AddSlot(arrowSlot);
        characterInventory.AddSlot(bombSlot);

        return characterInventory;
    }

    #region GiantAction implementation
    public bool oneShot
    {
        get 
        {
            return false;
        }
    }

    public bool hasExpired
    {
        get 
        {
            return false;
        }
    }

    public int Priority
    {
        get
        {
            return GameConfig.characterPriority;
        }
    }

    public EventSubject subject
    {
        get
        {
            return EventSubject.NerbyTarget;
        }
    }
    #endregion
}