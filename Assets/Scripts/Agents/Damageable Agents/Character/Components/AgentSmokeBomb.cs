using UnityEngine;
using System.Collections;

public class AgentSmokeBomb : CharacterComponent
{
    private Inventory characterInventory;

    public AgentSmokeBomb(CharacterAgent agent) : base(agent) { }

    #region implemented abstract members of AgentComponent

    public override void FrameFeed()
    {
        if (characterInventory == null)
        {
            characterInventory = agent.RequestComponent<Inventory>();
            return;
        }

        ConsumableType smokeItem = ConsumableType.Bomb;

        if (InputConfig.SmokeBomb() && characterInventory.HasItem(smokeItem))
        {
            characterInventory.UseItem(smokeItem);
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