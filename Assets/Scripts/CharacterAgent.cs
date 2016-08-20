using UnityEngine;

public class CharacterAgent : MonoBehaviour, Collector
{
    #region Collector implementation
    public void Notify(Collectable nerbyCollectable)
    {
        
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

    public NavMeshAgent agent;
    public AttackAimer aimer;

    protected CollectorChecker checker;

    private void Start()
    {
        checker = new CollectorChecker(this);
    }

    public void Update()
    {
        UpdateAgentSpeed();
        MoveAgent();

        aimer.Aim();
        checker.FrameUpdate();
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
}

public class CollectorChecker
{
    private Collector subscribedEntity;
    private ResourceCollectable[] resources;

    public float CollectorRange = 0.5f;

    public CollectorChecker(Collector subscribedEntity)
    {
        this.subscribedEntity = subscribedEntity;
        resources = Resources.FindObjectsOfTypeAll<ResourceCollectable>();
    }

    public void FrameUpdate()
    {
        foreach(Collectable ctb in resources)
        {
            float distance = Vector3.Distance(ctb.WorlPos, subscribedEntity.WorlPos);
            if (distance < CollectorRange)
            {
                subscribedEntity.Notify(ctb);
                return;
            }
        }
    }
}