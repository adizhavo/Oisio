using UnityEngine;
using Oisio.Agent;

namespace Oisio.Agent.State
{
    public class GiantBlindState : MonsterState 
    {
        private Vector3? eventPos;

        public GiantBlindState(MonsterAgent monster) : base(monster) { }

        #region implemented abstract members of GiantActionState
        protected override void Init()
        {
            #if UNITY_EDITOR
            Debug.Log("Giant enters into Blind state..");
            #endif

            eventPos = null;
        }

        public override void FrameFeed()
        {
            CheckDistance();
            DrawEscapePos();
        }

        public override void Notify(EventTrigger nerbyEvent)
        {
            if (!eventPos.HasValue && nerbyEvent.subject.Equals(EventSubject.SmokeBomb))
            {
                Escape(nerbyEvent);
            }
        }
        #endregion

        private void CheckDistance()
        {
            if (!eventPos.HasValue) return;

            float distance = (int)(Vector3.Distance(eventPos.Value, monster.WorlPos) * 10);
            if (distance  < Mathf.Epsilon)
            {
                monster.ChangeState<GiantAlertState>();
            }
        }
        
        private void Escape(EventTrigger nerbyEvent)
        {
            Vector3 bombDirection = (monster.navMeshAgent.pathEndPosition - monster.WorlPos).normalized;
            bombDirection.y = 0;

            eventPos = nerbyEvent.WorlPos + bombDirection * GameConfig.monsterSmokeEscapeDistance;
            monster.WorlPos = eventPos.Value;
            monster.SetSpeed(SpeedLevel.Fast);
        }

        private void DrawEscapePos()
        {
            #if UNITY_EDITOR

            if (eventPos.HasValue) Debug.DrawLine(monster.WorlPos, eventPos.Value, Color.red);

            #endif
        }
    }
}