using UnityEngine;

public class Chargable : AgentComponent
{
    private float minCollectPercentage = Mathf.Epsilon;

    private ConsumableAgent agent;

    public Chargable(ConsumableAgent agent)
    {
        this.agent = agent;
    }

    #region AgentComponent implementation
    public void FrameFeed()
    {
        UpdateState();  
    }
    #endregion

    private void UpdateState()
    {
        if (agent.percentage < minCollectPercentage && agent.current.Equals(ConsumableAgent.ChargableState.Charged))
        {
            agent.current = ConsumableAgent.ChargableState.Charging;
        }
        else
        {
            agent.percentage += Time.deltaTime/agent.reloadTime;
            if (agent.percentage > 1) agent.current = ConsumableAgent.ChargableState.Charged;
        }

        agent.percentage = Mathf.Clamp01(agent.percentage);
    }

    public void Consume(Consumer consumer)
    {
        if (agent.current.Equals(ConsumableAgent.ChargableState.Charging)) return;

        agent.percentage -= Time.deltaTime/agent.collectionTime;

        if (agent.percentage < minCollectPercentage) consumer.CompleteCollection(agent.type);
    }
}