using UnityEngine;

public class CharacterAgent : MonoBehaviour, EventTrigger
{
    [Header("Agent Configuration")]
    public GameObject arrowPrefab;
    public GameObject smokeBombPrefab;

    public bool invertAiming;
    public float sensibility;
    public float shootForce;
    public float collectorRange;

    [Header("Standart dependencies")]
    public Transform aimerPivot;
    public Transform arrowParent;
    public NavMeshAgent navMeshAgent;
    public Animator characterAnimator;

    public Inventory characterInventory;
    public AgentComponent[] components;

    private void Awake()
    {
        EventObserver.subscribedAction.Add(this);
    }

    protected virtual void Start()
    {
        components = InitComponents();
        characterInventory = InitInventory();
    }

    // can be easly changed and configured with a subclass 
    protected virtual AgentComponent[] InitComponents()
    {
        return components = new AgentComponent[]
            {
                // list all the agent component
                new AttackAimer(this), 
                new SmokeBomb(this),
                new CharacterMovement(this),
                new CharacterAnimation(this),
                new ResourceCollector(this)
            };
    }

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

    public void Update()
    {
        FeedComponents();
    }

    private void FeedComponents()
    {
        if (components == null) return;

        foreach(AgentComponent cmp in components)
        {
            cmp.FrameFeed();
        }
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

    #region WorldEntity implementation
    public Vector3 WorlPos
    {
        get
        {
            return transform.position;
        }
        set
        {
            navMeshAgent.SetDestination(value);
        }
    }
    #endregion
}