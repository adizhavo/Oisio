using UnityEngine;

public class MapBlockHolder 
{    
    public Vector3 GetNearestPosition(WorldEntity caller)
    {
        MapBlock[] blocks = Resources.FindObjectsOfTypeAll<MapBlock>();

        float? min = null;
        Vector3 nerbyPos = Vector3.zero;

        foreach(MapBlock mb in blocks)
        {
            Vector3 areaPos = mb.GetPositionInArea();
            float areDistance = Vector3.Distance(caller.WorlPos, areaPos);
            if (!min.HasValue || min > areDistance)
            {
                min = areDistance;
                nerbyPos = areaPos;
            }
        }

        return nerbyPos;
    }

    public Vector3 GetRandomPos()
    {
        MapBlock[] blocks = Resources.FindObjectsOfTypeAll<MapBlock>();

        int randomIndex = Random.Range(0, blocks.Length);

        return blocks[randomIndex].GetPositionInArea();
    }
}