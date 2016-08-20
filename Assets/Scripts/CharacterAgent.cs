using UnityEngine;
using System.Collections;

public class CharacterAgent : MonoBehaviour {

    public NavMeshAgent agent;
    public LayerMask grounMask;

    public void Update()
    {
        float xDirection = Input.GetAxis("Horizontal");
        float yDirection = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.position + new Vector3(xDirection, 0, yDirection);
        agent.SetDestination(moveDirection);
    }
}
