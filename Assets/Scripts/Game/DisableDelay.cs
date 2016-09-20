using UnityEngine;
using System.Collections;

public class DestroyDelay : MonoBehaviour
{
    [SerializeField] private float destroyAfter;

    private void OnDisable()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown () 
    {
        yield return new WaitForSeconds(destroyAfter);
        gameObject.SetActive(false);
	}
}