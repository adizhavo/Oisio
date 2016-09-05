using UnityEngine;

public class AgentResourceCollector : CharacterComponent, Consumer
{
    public AgentResourceCollector(CharacterAgent agent) : base(agent) { }
    #region implemented abstract members of AgentComponent
    public override void FrameFeed()
    {
        if (InputConfig.Collect())
        {
            ConsumableAgent[] resources = Resources.FindObjectsOfTypeAll<ConsumableAgent>();

            foreach(ConsumableAgent ctb in resources)
            {
                float distance = Vector3.Distance(ctb.WorlPos, agent.WorlPos);
                if (ctb.gameObject.activeInHierarchy && distance < agent.collectorRange)
                {
                    ctb.Consume(this);
                    return;
                }
            }
        }
    }
    #endregion

    #region Collector implementation

    public void CompleteCollection(ConsumableType collectable)
    {
        agent.characterInventory.AddItem(collectable);
    }

    #endregion
}