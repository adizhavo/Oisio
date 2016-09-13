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

    [Header("Health Configuration")]
    public float maxHealth;
    public float healthRegen;

    [Header("Stamina Configuration")]
    public float maxStamina;
    public float staminaRegen;

    [Header("Standart dependencies")]
    public Animator characterAnimator;
    public Transform aimerPivot;
    public Transform arrowParent;

    #region Agent implementation

    protected override void Init()
    {
        base.Init();

        InitInventory();
        ChangeState<NullAgentState>();
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
                new Inventory(this),
                new AgentAttack(this), 
                new AgentSmokeBomb(this),
                new AgentStamina(this),
                new AgentHealth(this),
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

    protected virtual void InitInventory()
    {
        Slot arrowSlot = new Slot(ConsumableType.Arrow, GameConfig.arrowInventorySize);
        arrowSlot.StockItem = GameConfig.initialArrows;

        Slot bombSlot = new Slot(ConsumableType.Bomb, GameConfig.smokeBombInvertorySize);
        bombSlot.StockItem = GameConfig.initialBombs;

        Inventory characterInventory = RequestComponent<Inventory>();
        characterInventory.AddSlot(arrowSlot);
        characterInventory.AddSlot(bombSlot);
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