using UnityEngine;
using System.Collections;

public class AttackAimer : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowParent;
    public bool InvertAiming;

    private GameObject arrowInstance;

    public void Aim()
    {
        if (InputConfig.Aim())
        {
            CheckArrow();
            RotateAimer();
        }
        else
        {
            ResetAim();
        }
    }

    private void CheckArrow()
    {
        if (!arrowInstance)
        {
            arrowInstance = Instantiate(arrowPrefab) as GameObject;
            arrowInstance.transform.SetParent(arrowParent);
            arrowInstance.transform.localPosition = Vector3.zero;
        }

        arrowInstance.SetActive(true);
    }

    private void RotateAimer()
    {
        float cursorDeltaX = InputConfig.GetCursorMovement().x * AimingDirection();
        transform.rotation = Quaternion.Euler(0f, 0f, transform.localEulerAngles.z + cursorDeltaX);
    }

    private void ResetAim()
    {
        if (arrowInstance) arrowInstance.SetActive(false);
        transform.localEulerAngles = Vector3.zero;
    }

    private int AimingDirection()
    {
        return InvertAiming ? 1 : -1;
    }
}