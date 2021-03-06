﻿using Oisio.Game;
using UnityEngine;
using Oisio.Agent;

namespace Oisio.Agent.Component
{
    // Collects nerby resources
    public class CharacterResourceComponent : CharacterComponent, Consumer
    {
        private CharacterInventoryComponent characterInventory;

        public CharacterResourceComponent(CharacterAgent agent) : base(agent) { }

        #region implemented abstract members of CharacterComponent
        public override void FrameFeed()
        {
            if (characterInventory == null)
            {
                characterInventory = agent.RequestComponent<CharacterInventoryComponent>();
                return;
            }

            ConsumableAgent[] resources = Resources.FindObjectsOfTypeAll<ConsumableAgent>();

            foreach(ConsumableAgent ctb in resources)
            {
                float distance = Vector3.Distance(ctb.WorlPos, agent.WorlPos);
                if (ctb.gameObject.activeInHierarchy && distance < agent.collectorRange)
                {
                    if (characterInventory.HasSpace(ctb.Item)) ctb.Collect(this);
                    return;
                }
            }
        }
        #endregion

        #region Consumer implementation

        public void Collected(ConsumableType collectable, int amount)
        {
            if (characterInventory == null) return;
            characterInventory.AddItem(collectable, amount);
        }

        #endregion
    }
}