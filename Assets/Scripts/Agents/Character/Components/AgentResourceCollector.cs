using UnityEngine;

public class AgentResourceCollector : CharacterComponent, Consumer
{
    private Inventory characterInventory;

    public AgentResourceCollector(CharacterAgent agent) : base(agent) { }

    #region implemented abstract members of CharacterComponent
    public override void FrameFeed()
    {
        if (characterInventory == null)
        {
            characterInventory = agent.RequestComponent<Inventory>();
            return;
        }

        if (InputConfig.Collect())
        {
            ConsumableAgent[] resources = Resources.FindObjectsOfTypeAll<ConsumableAgent>();

            foreach(ConsumableAgent ctb in resources)
            {
                float distance = Vector3.Distance(ctb.WorlPos, agent.WorlPos);
                if (ctb.gameObject.activeInHierarchy && distance < agent.collectorRange)
                {
                    ctb.Collect(this);
                    return;
                }
            }
        }
    }
    #endregion

    #region Consumer implementation

    public void Collected(ConsumableType collectable)
    {
        if (characterInventory == null) return;
        characterInventory.AddItem(collectable);
    }

    #endregion
}