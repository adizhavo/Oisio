using UnityEngine;
using System.Collections;

public class DestroyDelay : MonoBehaviour
{
    [SerializeField] private float destroyAfter = 1;

    private IEnumerator Start () 
    {
        yield return new WaitForSeconds(destroyAfter);

        Destroy(gameObject);
	}
}