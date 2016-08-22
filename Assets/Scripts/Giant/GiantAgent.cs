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

        WaitForSeconds waiTime = new WaitForSeconds(3);
        while(true)
        {
            agent.SetDestination(resourcesBlock.GetNearestPosition(this));
            yield return waiTime;
        }
	}
}
