using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MapBlock : MonoBehaviour 
{    
    [SerializeField] private float areaRadius;

    public Vector3 GetPositionInArea()
    {
        Vector2 randomPos = Random.insideUnitCircle;
        return new Vector3(transform.position.x + randomPos.x * areaRadius, 0f, transform.position.z + randomPos.y * areaRadius);
    }

	private void Update ()
    {
        DrawArea();
	}

    private void DrawArea()
    {
        Debug.DrawLine(transform.position, transform.position + new Vector3(areaRadius, 0, 0), Color.red);
    }
}