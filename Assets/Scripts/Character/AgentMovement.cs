using UnityEngine;
using System.Collections;

public class CharacterMovement : AgentComponent
{
    public CharacterMovement(CharacterAgent agent) : base (agent) { }

    #region implemented abstract members of AgentComponent
    public override void FrameFeed()
    {
        float runningSpeed = InputConfig.Run() ? GameConfig.maxCharacterSpeed : GameConfig.minCharacterSpeed;
        agent.navMeshAgent.speed = runningSpeed;

        Vector3 moveDirection = agent.transform.position + new Vector3(InputConfig.XDriection(), 0, InputConfig.YDriection());
        agent.navMeshAgent.SetDestination(moveDirection);
    }
    #endregion
}
