using UnityEngine;
using System.Collections;

public class AgentSmokeBomb : CharacterComponent
{
    public AgentSmokeBomb(CharacterAgent agent) : base(agent) { }

    #region implemented abstract members of AgentComponent

    public override void FrameFeed()
    {
        ConsumableType smokeItem = ConsumableType.Bomb;

        if (InputConfig.SmokeBomb() && agent.characterInventory.HasItem(smokeItem))
        {
            agent.characterInventory.UseItem(smokeItem);
            Fire();
        }
    }

    #endregion

    public void Fire()
    {
        GameObject smokeInsr = GameObject.Instantiate(agent.smokeBombPrefab) as GameObject;
        Vector3 deployPosition = agent.transform.position;
        smokeInsr.transform.position = deployPosition;
        EventTrigger smokeBomb = new CustomEvent(deployPosition, EventSubject.SmokeBomb, GameConfig.smokeBombPriotity, GameConfig.smokeBombEnableTime, true);
        EventObserver.Subscribe(smokeBomb);
    }
}