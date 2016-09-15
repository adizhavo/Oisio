using UnityEngine;
using System.Collections;

public class CharcterSmokebombComponent : CharacterComponent
{
    private CharacterInventoryComponent characterInventory;

    public CharcterSmokebombComponent(CharacterAgent agent) : base(agent) { }

    #region implemented abstract members of AgentComponent

    public override void FrameFeed()
    {
        if (characterInventory == null)
        {
            characterInventory = agent.RequestComponent<CharacterInventoryComponent>();
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