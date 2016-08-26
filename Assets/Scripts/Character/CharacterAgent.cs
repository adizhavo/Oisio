using UnityEngine;

public class CharacterAgent : MonoBehaviour, Collector, EventTrigger
{
    public NavMeshAgent agent;
    public Animator characterAnimator;
    public AttackAimer aimer;
    public SmokeBomb smokeLouncher;

    protected CharacterAnimation animation;
    protected CollectorChecker checker;
    protected Inventory characterInventory;

    private void Awake()
    {
        ActionObserver.subscribedAction.Add(this);
    }

    protected virtual void Start()
    {
        checker = new CollectorChecker(this);
        animation = new CharacterAnimation(characterAnimator);
        InitInventory();
    }

    protected virtual void InitInventory()
    {
        Slot arrowSlot = new Slot(CollectableType.Arrow, 100);
        arrowSlot.SetStockItems(100);

        Slot bombSlot = new Slot(CollectableType.Bomb, 2);
        bombSlot.SetStockItems(2);

        characterInventory = new Inventory();
        characterInventory.AddSlot(arrowSlot);
        characterInventory.AddSlot(bombSlot);
    }

    public void Update()
    {
        UpdateAgentSpeed();
        MoveAgent();
        Aim();
        UpdateSmokeLouncher();

        checker.FrameUpdate();
    }

    private void Aim()
    {
        CollectableType arrow = CollectableType.Arrow;

        if (characterInventory.HasItem(arrow))
        {
            if(InputConfig.Aim())
            {
                aimer.Aim();

                if (InputConfig.ActionDown())
                {
                    aimer.ThrowArrow();
                    characterInventory.UseItem(arrow);
                }
            }
            else
                aimer.ResetAim();
        }
    }

    private void UpdateSmokeLouncher()
    {
        CollectableType smokeItem = CollectableType.Bomb;

        if (InputConfig.SmokeBomb() && characterInventory.HasItem(smokeItem))
        {
            characterInventory.UseItem(smokeItem);
            smokeLouncher.Fire(transform.position);
        }
    }

    private void UpdateAgentSpeed()
    {
        float runningSpeed = InputConfig.Run() ? 2f : 1f;
        agent.speed = runningSpeed;
        animation.SetRun(runningSpeed);
    }

    private void MoveAgent()
    {
        Vector3 moveDirection = transform.position + new Vector3(InputConfig.XDriection(), 0, InputConfig.YDriection());
        agent.SetDestination(moveDirection);
    }

    #region Collector implementation
    public void CompleteCollection(CollectableType collectable)
    {
        characterInventory.AddItem(collectable);
    }

    public void Notify(Collectable nerbyCollectable)
    {
        if (InputConfig.Collect())
        {
            nerbyCollectable.Collect(this);
        }
    }
    #endregion

    #region GiantAction implementation
    public int Priority
    {
        get
        {
            return 10;
        }
    }

    public EventSubject actionEvent
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
            agent.SetDestination(value);
        }
    }
    #endregion
}