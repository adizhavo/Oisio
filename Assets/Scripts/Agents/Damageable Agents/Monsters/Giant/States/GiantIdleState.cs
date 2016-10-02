using UnityEngine;
using Oisio.Agent;
using Oisio.Agent.Component;

namespace Oisio.Agent.State
{
    public class GiantIdleState : MonsterState
    {
        protected float waitTime;

        public GiantIdleState(MonsterAgent monster) : base(monster) { }

        #region implemented abstract members of GiantActionState
        protected override void Init()
        {
            #if UNITY_EDITOR
            Debug.Log("Giant enters into Idle state..");
            #endif

            waitTime = Random.Range(3f, 7f);
            monster.SetSpeed(SpeedLevel.Slow);
        }

        public override void FrameFeed()
        {
            if (waitTime < 0f)
            {
                GotoNearestResource();
                waitTime = Random.Range(3f, 7f);

                if (ShouldChangeResource())
                {
                    GotoRandomResource();
                    waitTime += 5f;
                }
            }

            waitTime -= Time.deltaTime;
        }

        public override void Notify(EventTrigger nerbyEvent)
        {
            if (nerbyEvent.subject.Equals(EventSubject.NerbyTarget) || nerbyEvent.subject.Equals(EventSubject.Attack)) 
            {
                monster.ChangeState<GiantAlertState>(nerbyEvent);
            }
        }

        #endregion

        protected virtual bool ShouldChangeResource()
        {
            return Random.Range(0, 2) == 0;
        }

        public virtual void GotoNearestResource()
        {
            monster.WorlPos = monster.RequestComponent<MapBlockHolder>().GetNearestPosition(monster);
        }

        public virtual void GotoRandomResource()
        {
            monster.WorlPos = monster.RequestComponent<MapBlockHolder>().GetRandomPos();
        }
    }
}