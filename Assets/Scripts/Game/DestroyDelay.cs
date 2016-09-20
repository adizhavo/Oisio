using UnityEngine;
using System.Collections;

public class DestroyDelay : MonoBehaviour
{
    [SerializeField] private float destroyAfter;

    private IEnumerator Start () 
    {
        yield return new WaitForSeconds(destroyAfter);

        Destroy(gameObject);
	}
}