using UnityEngine;
using Oisio.Agent;

namespace Oisio.Agent.State
{
    public class JumperIdleState : GiantIdleState
    {
        public JumperIdleState(JumperAgent giant) : base (giant) { }

        #region GiantIdleState implementation

        public override void Notify(EventTrigger nerbyEvent)
        {
            if (nerbyEvent.subject.Equals(EventSubject.NerbyTarget) || nerbyEvent.subject.Equals(EventSubject.Attack))
            {
                monster.ChangeState<JumperAlertState>(nerbyEvent);
            }
        }

        #endregion
    }
}