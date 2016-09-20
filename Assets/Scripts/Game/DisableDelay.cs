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
        ResetTransform();
        gameObject.SetActive(false);
	}

    private void ResetTransform()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}