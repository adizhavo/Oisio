using UnityEngine;
using System.Collections;

public class CharacterAgent : MonoBehaviour
{
    public NavMeshAgent agent;

    public void Update()
    {
        UpdateAgentSpeed();
        MoveAgent();
    }

    private void UpdateAgentSpeed()
    {
        float runningSpeed = InputConfig.Run() ? 2f : 1f;
        agent.speed = runningSpeed;
    }

    private void MoveAgent()
    {
        Vector3 moveDirection = transform.position + new Vector3(InputConfig.XDriection(), 0, InputConfig.YDriection());
        agent.SetDestination(moveDirection);
    }
}
