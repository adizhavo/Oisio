using Oisio.Game;
using UnityEngine;
using Oisio.Events;
using Oisio.Agent.State;
using Oisio.Agent.Component;
using System.Collections.Generic;

namespace Oisio.Agent
{
    public class CharacterAgent : DamageableAgent, EventTrigger
    {
        [Header("Agent Configuration")]
        public GameObject arrowPrefab;
        public GameObject smokeBombPrefab;

        public bool invertAiming;
        public float sensibility;
        public float shootForce;
        public float collectorRange;
        public float walkSpeed;
        public float runSpeed;

        [Header("Stamina Configuration")]
        public float maxStamina;
        public float staminaRegen;
        public float staminaCost;

        [Header("Standart dependencies")]
        public Animator characterAnimator;
        public Transform aimerPivot;
        public Transform arrowParent;

        [Header("Character FX Configuration")]
        public GameObjectPool DeathEffect;

        #region Agent implementation

        protected override void Init()
        {
            base.Init();

            InitInventory();
            ChangeState<NullAgentState>();
            EventObserver.Subscribe(this);
        }

        private void OnDestroy()
        {
            EventObserver.Unsubcribe(this);
        }

        // can be easly changed and configured with a subclass 
        protected override List<AgentComponent> InitComponents()
        {
            return new List<AgentComponent>
                {
                    // list all the agent component
                    new TrajectoryGizmo(30, 3),
                    new AgentHealth(this),
                    new CharacterInventoryComponent(this),
                    new CharacterAttackComponent(this), 
                    new CharcterSmokebombComponent(this),
                    new CharacterStaminaComponent(this),
                    new CharacterMovementComponent(this),
                    new CharacterAnimationComponent(this),
                    new CharacterResourceComponent(this)
                };
        }

        protected override AgentState[] InitStates()
        {
            return new AgentState[]
            {
                new NullAgentState(),
                new AgentDeathState(this, DeathEffect)
            };
        }

        #endregion

        protected virtual void InitInventory()
        {
            Slot arrowSlot = new Slot(ConsumableType.Arrow, GameConfig.arrowInventorySize);
            arrowSlot.StockItem = GameConfig.initialArrows;

            Slot bombSlot = new Slot(ConsumableType.Bomb, GameConfig.smokeBombInvertorySize);
            bombSlot.StockItem = GameConfig.initialBombs;

            CharacterInventoryComponent characterInventory = RequestComponent<CharacterInventoryComponent>();
            characterInventory.AddSlot(arrowSlot);
            characterInventory.AddSlot(bombSlot);
        }

        #region GiantAction implementation
        public bool oneShot
        {
            get 
            {
                return false;
            }
        }

        public bool hasExpired
        {
            get 
            {
                return false;
            }
        }

        public int Priority
        {
            get
            {
                return GameConfig.characterPriority;
            }
        }

        public EventSubject subject
        {
            get
            {
                return EventSubject.NerbyTarget;
            }
        }
        #endregion
    }
}