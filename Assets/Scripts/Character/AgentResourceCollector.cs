using UnityEngine;

public class AgentResourceCollector : AgentComponent, Collector
{
    public AgentResourceCollector(CharacterAgent agent) : base(agent) { }
    #region implemented abstract members of AgentComponent
    public override void FrameFeed()
    {
        if (InputConfig.Collect())
        {
            ResourceCollectable[] resources = Resources.FindObjectsOfTypeAll<ResourceCollectable>();

            foreach(ResourceCollectable ctb in resources)
            {
                float distance = Vector3.Distance(ctb.WorlPos, agent.WorlPos);
                if (ctb.gameObject.activeInHierarchy && distance < agent.collectorRange)
                {
                    ctb.Gather(this);
                    return;
                }
            }
        }
    }
    #endregion

    #region Collector implementation

    public void CompleteCollection(CollectableType collectable)
    {
        agent.characterInventory.AddItem(collectable);
    }

    #endregion
}