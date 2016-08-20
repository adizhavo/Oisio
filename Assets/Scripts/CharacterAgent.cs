using UnityEngine;

public class CharacterAgent : MonoBehaviour
{
    public NavMeshAgent agent;
    public AttackAimer aimer;

    public void Update()
    {
        UpdateAgentSpeed();
        MoveAgent();

        aimer.Aim();
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
