using UnityEngine;
using Oisio.Agent;

namespace Oisio.Agent.State
{
    public class GiantAlertState : MonsterState 
    {
        protected Vector3? eventPos = null;

        public float reactionSpeed
        {
            get { return monster.alertReactionSpeed * Time.deltaTime; } 
        }

        public GiantAlertState(MonsterAgent monster) : base(monster) 
        {
            monster.SetSpeed(SpeedLevel.Medium);
        }

        #region implemented abstract members of GiantActionState

        protected override void Init()
        {
            #if UNITY_EDITOR
            Debug.Log("Giant enters into Alert state..");
            #endif

            eventPos = null;
            IncreaseAlert();
        }

        public override void FrameFeed()
        {
            MovetoEventPos();
            DecreseAlert();
            CheckAlertLevel();
            eventPos = null;
        }

        public override void Notify(EventTrigger nerbyEvent)
        {
            if (nerbyEvent.subject.Equals(EventSubject.NerbyTarget)) 
            {
                IncreaseAlert();
                eventPos = nerbyEvent.WorlPos;
            }
            else if (nerbyEvent.subject.Equals(EventSubject.Attack))
            {
                monster.ChangeState<GiantRageState>(nerbyEvent);
            }
            else if (nerbyEvent.subject.Equals(EventSubject.SmokeBomb))
            {
                monster.ChangeState<GiantBlindState>(nerbyEvent);
            }
        }

        #endregion

        protected void IncreaseAlert()
        {
            monster.AlertLevel += reactionSpeed * 2;
        }

        protected void DecreseAlert()
        {
            monster.AlertLevel -= reactionSpeed;
        }

        protected virtual void MovetoEventPos()
        {
            if (eventPos.HasValue)
            {
                monster.WorlPos = eventPos.Value;
            }
        }

        protected virtual void CheckAlertLevel()
        {
            if (monster.AlertLevel < GameConfig.minAlertLevel + Mathf.Epsilon)
            {
                monster.ChangeState<GiantIdleState>();
                ResetState();
            }
            else if (monster.AlertLevel >= GameConfig.maxAlertLevel - Time.deltaTime - Mathf.Epsilon)
            {
                monster.ChangeState<GiantHuntState>();
            }
            else
            {
                monster.SetSpeed(SpeedLevel.Medium);
            }
        }

        protected virtual void ResetState()
        {
            if (eventPos.HasValue) monster.WorlPos = eventPos.Value;
            monster.AlertLevel = GameConfig.minAlertLevel;
            eventPos = null;
        }
    }
}