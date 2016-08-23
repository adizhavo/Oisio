using UnityEngine;
using System.Collections;

public class GiantAgent : MonoBehaviour, WorldEntity
{
    #region WorldEntity implementation

    public Vector3 WorlPos
    {
        get
        {
            return transform.position;
        }
        set
        {
            agent.SetDestination(value);
        }
    }

    #endregion

    [SerializeField] protected NavMeshAgent agent;
    protected MapBlockHolder resourcesBlock;

    protected virtual IEnumerator Start ()
    {   
        resourcesBlock = new MapBlockHolder();

        while(true)
        {
            agent.SetDestination(resourcesBlock.GetNearestPosition(this));

            float waitTime = Random.Range(3f, 7f);

            if (Random.Range(0, 2) == 0)
            {
                agent.SetDestination(resourcesBlock.GetRandomPos());
                waitTime += 5f;
            }

            yield return new WaitForSeconds(waitTime);
        }
	}
}
