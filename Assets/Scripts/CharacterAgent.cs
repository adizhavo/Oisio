using UnityEngine;
using System.Collections;

public class CharacterAgent : MonoBehaviour
{
    public NavMeshAgent agent;

    public void Update()
    {
        Vector3 moveDirection = transform.position + new Vector3(InputConfig.XDriection(), 0, InputConfig.YDriection());
        agent.SetDestination(moveDirection);
    }
}
