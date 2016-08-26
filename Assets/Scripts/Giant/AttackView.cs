using UnityEngine;
using System.Collections;

public class AttackView : MonoBehaviour
{
    public GameObject ZoneImage;

    private float timeCounter = 0f;

    private float speed;
    private float targetPercentage;
    private float currentPercentage;

    public void SetAttackView(float percentage, float AttackSpeed)
    {
        timeCounter = 0f;

        this.targetPercentage = percentage;
        this.speed = AttackSpeed;
    }

    private void LateUpdate()
    {
        timeCounter = Mathf.Clamp01(timeCounter);

        currentPercentage = Mathf.Lerp(currentPercentage, targetPercentage, timeCounter);
        timeCounter += Time.deltaTime / (speed);

        SetAlphaValue(ZoneImage.transform, currentPercentage);
    }

    private void SetAlphaValue(Transform alphaTr, float alphaValue)
    {
        SetAlphaToTr(alphaTr.GetComponent<SpriteRenderer>(), alphaValue);

        for (int i = 0; i < alphaTr.childCount; i++)
        {
            SetAlphaValue(alphaTr.GetChild(i), alphaValue);
        }
    }

    private void SetAlphaToTr(SpriteRenderer GraphicElement, float alphaValue)
    {
        if (GraphicElement == null)
            return;

        Color objectColor = GraphicElement.color;
        objectColor.a = alphaValue;
        GraphicElement.color = objectColor;
    }
}
