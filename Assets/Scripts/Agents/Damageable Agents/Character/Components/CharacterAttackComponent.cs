using Oisio.Game;
using UnityEngine;
using Oisio.Events;
using Oisio.Agent;

namespace Oisio.Agent.Component
{
    // Handles character aim and attack with an arrow
    public class CharacterAttackComponent : CharacterComponent
    {
        private Projectile arrowInstance;

        private float cursorDeltaY;
        private float cursorDeltaX;

        private CharacterInventoryComponent characterInventory;
        private TrajectoryGizmo throwTrajectory;

        private ConsumableType arrow = ConsumableType.Arrow;

        public CharacterAttackComponent(CharacterAgent agent) : base (agent) { }

        #region implemented abstract members of AgentComponent

        public override void FrameFeed()
        {
            if (characterInventory == null)
            {
                characterInventory = agent.RequestComponent<CharacterInventoryComponent>();
                throwTrajectory = agent.RequestComponent<TrajectoryGizmo>();
                return;
            }

            if (characterInventory.HasItem(arrow))
            {
                if(InputConfig.Aim())  Aim();
                else if (InputConfig.ActionUp())
                {
                    ThrowArrow();
                    characterInventory.UseItem(arrow);
                }
                else ResetAim();
            }
            else ResetAim();
        }

        #endregion

        public void Aim()
        {
            UpdateCursorPosition();
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

        private void UpdateCursorPosition()
        {
            cursorDeltaX += InputConfig.GetCursorMovement().x * AimingDirection() * agent.sensibility * Time.deltaTime;
            cursorDeltaX = Mathf.Clamp(cursorDeltaX, -agent.shootRadius, agent.shootRadius - Mathf.Epsilon);

            cursorDeltaY += InputConfig.GetCursorMovement().y * AimingDirection() * agent.sensibility * Time.deltaTime;
            cursorDeltaY = Mathf.Clamp(cursorDeltaY, -agent.shootRadius, agent.shootRadius);
        }

        private void RotateAimer()
        {
            Vector3 aimPos = new Vector3(cursorDeltaX, 0f, cursorDeltaY);
            float lookAngle = Mathf.Atan(cursorDeltaY/cursorDeltaX) * Mathf.Rad2Deg;
            float sinus = Physics.gravity.y * aimPos.magnitude / Mathf.Pow(agent.shootForce, 2);
            sinus = Mathf.Clamp(sinus, -1f, 1f);
            float throwAngle = Mathf.Asin(sinus) * Mathf.Rad2Deg / 2f;
            float angleOffset = Mathf.Sign(Vector3.Cross(aimPos, Vector3.back).y) == 1 ? 0f : 180f;
            agent.aimerPivot.eulerAngles = new Vector3(0f, -(angleOffset + lookAngle), throwAngle);
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

            cursorDeltaX = Mathf.Epsilon;
            cursorDeltaY = Mathf.Epsilon;
        }

        private int AimingDirection()
        {
            return agent.invertAiming ? -1 : 1;
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
}