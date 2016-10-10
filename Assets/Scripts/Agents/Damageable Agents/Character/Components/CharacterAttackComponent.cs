using Oisio.Game;
using UnityEngine;
using Oisio.Events;
using Oisio.Agent;
using System.Collections.Generic;

namespace Oisio.Agent.Component
{
    // Handles character aim and attack with an arrow
    public class CharacterAttackComponent : CharacterComponent
    {
        private Projectile arrowInstance;
        private EnemyTarger targetCollection;

        private float cursorDeltaY;

        private CharacterInventoryComponent characterInventory;
        private TrajectoryGizmo throwTrajectory;

        private ConsumableType arrow = ConsumableType.Arrow;

        public CharacterAttackComponent(CharacterAgent agent) : base(agent)
        {
            targetCollection = new EnemyTarger();
        }

        #region implemented abstract members of AgentComponent

        public override void FrameFeed()
        {
            if (characterInventory == null)
            {
                characterInventory = agent.RequestComponent<CharacterInventoryComponent>();
                throwTrajectory = agent.RequestComponent<TrajectoryGizmo>();
                return;
            }

            targetCollection.CollectAvailables(agent.WorlPos);

            if (characterInventory.HasItem(arrow) && targetCollection.hasTarget)
            {
                if (InputConfig.ChangeTarget()) targetCollection.ChangeTarget();

                if(InputConfig.Aim())
                {
                    Aim();
                }
                else if (InputConfig.ActionUp())
                {
                    ThrowArrow();
                    characterInventory.UseItem(arrow);
                }
                else
                    ResetAim();
            }
            else
                ResetAim();
        }

        #endregion

        public void Aim()
        {
            CheckArrow();
            RotateAimer();
            DisplayTrajectory();
            agent.aimerPivot.gameObject.SetActive(true);
        }

        private void CheckArrow()
        {
            if (!arrowInstance)
            {
                arrowInstance = PooledObjects.Instance.RequestGameObject(GameObjectPool.Arrow).GetComponent<Projectile>();
                arrowInstance.transform.SetParent(agent.arrowParent, false);
                arrowInstance.transform.localPosition = Vector3.zero;
            }

            arrowInstance.gameObject.SetActive(true);
        }

        private void RotateAimer()
        {
            cursorDeltaY += InputConfig.GetCursorMovement().y * AimingDirection() * agent.sensibility;
            agent.aimerPivot.rotation = Quaternion.LookRotation(GetTargetNormalizedDirection());
            agent.aimerPivot.rotation = Quaternion.Euler(agent.aimerPivot.localEulerAngles.x + cursorDeltaY, 
                                                         agent.aimerPivot.localEulerAngles.y, 
                                                         agent.aimerPivot.localEulerAngles.z);
        }

        private Vector3 GetTargetNormalizedDirection()
        {
            Vector3 direction = (targetCollection.choosenTarget.transform.position - agent.WorlPos).normalized;
            direction.y = 0;
            return direction;
        }

        private void DisplayTrajectory()
        {
            Vector3 shootForce = (agent.arrowParent.position - agent.aimerPivot.position).normalized * agent.shootForce;
            throwTrajectory.Display(agent.aimerPivot.position, shootForce);
        }

        public void ResetAim()
        {
            if (arrowInstance) arrowInstance.gameObject.SetActive(false);
            agent.aimerPivot.localEulerAngles = Vector3.zero;
            agent.aimerPivot.gameObject.SetActive(false);
            cursorDeltaY = 0f;

            targetCollection.Reset();
        }

        private int AimingDirection()
        {
            return agent.invertAiming ? 1 : -1;
        }

        public void ThrowArrow()
        {
            if (!arrowInstance) return;

            EventTrigger attackEvent = new CustomEvent(agent.aimerPivot.position, EventSubject.Attack, GameConfig.projectilePriority);

            // TODO : fix the direction calculation, the base position should be the head of the character, or the end of the arrow, not the agent
            Vector3 shootDirection = agent.arrowParent.position - agent.aimerPivot.position;
            arrowInstance.Shoot(attackEvent, shootDirection.normalized * agent.shootForce);

            arrowInstance.transform.SetParent(null);
            arrowInstance = null;
        }
    }

    public class EnemyTarger
    {
        private GameObject[] activeEnemies;
        private List<GameObject> targets = new List<GameObject>();
        private List<GameObject> alreadyChoosenTargets = new List<GameObject>();

        public GameObject choosenTarget 
        {
            private set;
            get;
        }

        public bool hasTarget
        {
            get { return choosenTarget != null; }
        }

        public EnemyTarger()
        {
            activeEnemies = GameObject.FindGameObjectsWithTag(GameConfig.MONSTER_TAG);
        }

        public void CollectAvailables(Vector3 pivot)
        {
            targets.Clear();

            for (int i = 0; i < GameConfig.MAX_TARGABLE_MONSTERS; i ++) targets.Add( GetClosestEnemy(pivot) );

            if(choosenTarget == null)
            {
                foreach(GameObject t in targets)
                    if (t != null)
                    {
                        choosenTarget = t;
                        return;
                    }
            }
        }

        public void ChangeTarget()
        {
            if (alreadyChoosenTargets.Count < GameConfig.MAX_TARGABLE_MONSTERS)
            {
                foreach(GameObject t in targets)
                {
                    if (!alreadyChoosenTargets.Contains(t) && choosenTarget != t)
                    {
                        choosenTarget = t;
                        alreadyChoosenTargets.Add(t);
                        return;
                    }
                }
            }
            else
            {
                alreadyChoosenTargets.Clear();
                ChangeTarget();
            }
        }

        public void Reset()
        {
            choosenTarget = null;
            alreadyChoosenTargets.Clear();
        }

        private GameObject GetClosestEnemy(Vector3 pivot)
        {
            GameObject closestTarget = null;
            float targetDistance = Mathf.Infinity;

            foreach(GameObject enemy in activeEnemies)
            {
                if (enemy == null || targets.Contains(enemy) || !enemy.activeInHierarchy) continue;

                float distance = Vector3.Distance(pivot, enemy.transform.position);
                if (closestTarget == null || distance < targetDistance)
                {
                    closestTarget = enemy;
                    targetDistance = distance;
                }
            }

            return closestTarget;
        }
    }
}