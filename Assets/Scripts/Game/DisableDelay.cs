using UnityEngine;
using System.Collections;

public class DisableDelay : MonoBehaviour
{
    [SerializeField] private float disableAfter;

    private void OnEnable()
    {
        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown () 
    {
        yield return new WaitForSeconds(disableAfter);
        gameObject.SetActive(false);
	}
}