using UnityEngine;

public class CharacterAgent : MonoBehaviour, Collector
{
    public NavMeshAgent agent;
    public AttackAimer aimer;

    protected CollectorChecker checker;
    protected Inventory characterInventory;

    protected virtual void Start()
    {
        checker = new CollectorChecker(this);
        InitInventory();
    }

    protected virtual void InitInventory()
    {
        Slot arrowSlot = new Slot(CollectableType.Arrow, 2);
        arrowSlot.SetStockItems(1);

        Slot bombSlot = new Slot(CollectableType.Bomb, 1);

        characterInventory = new Inventory();
        characterInventory.AddSlot(arrowSlot);
        characterInventory.AddSlot(bombSlot);
    }

    public void Update()
    {
        UpdateAgentSpeed();
        MoveAgent();
        Aim();

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

    private void UpdateAgentSpeed()
    {
        float runningSpeed = InputConfig.Run() ? 2f : 1f;
        agent.speed = runningSpeed;
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

    #region WorldEntity implementation
    public Vector3 WorlPos
    {
        get
        {
            return transform.position;
        }
    }
    #endregion
}