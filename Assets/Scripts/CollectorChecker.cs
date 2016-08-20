using UnityEngine;

public class CollectorChecker
{
    private Collector subscribedEntity;
    public float CollectorRange = 0.5f;

    public CollectorChecker(Collector subscribedEntity)
    {
        this.subscribedEntity = subscribedEntity;
    }

    public void FrameUpdate()
    {
        ResourceCollectable[] resources = Resources.FindObjectsOfTypeAll<ResourceCollectable>();

        foreach(ResourceCollectable ctb in resources)
        {
            float distance = Vector3.Distance(ctb.WorlPos, subscribedEntity.WorlPos);
            if (ctb.gameObject.activeInHierarchy && distance < CollectorRange)
            {
                subscribedEntity.Notify(ctb);
                return;
            }
        }
    }
}