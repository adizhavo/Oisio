using UnityEngine;

public class AttackAnimation : AgentComponent
{
    private GameObject ZoneImage;

    private float timeCounter = 0f;
    private float transitionTime;
    private float targetPercentage;
    private float currentPercentage;

    public void PrepareAttack(GameObject animation, float transitionTime)
    {
        this.transitionTime = transitionTime;
        targetPercentage = 1;
        timeCounter = 0f;
        ZoneImage = animation;
    }

    public void Attack(GameObject animation)
    {
        Debug.Log("Attack animation...");
    }

    public void Recover(GameObject animation, float transitionTime)
    {
        this.transitionTime = transitionTime;
        targetPercentage = 0;
        timeCounter = 0f;
        ZoneImage = animation;
    }

    #region AgentComponent implementaion

    public void FrameFeed()
    {
        if (ZoneImage == null) return;

        timeCounter = Mathf.Clamp01(timeCounter);
        currentPercentage = Mathf.Lerp(currentPercentage, targetPercentage, timeCounter);
        timeCounter += Time.deltaTime / transitionTime;

        SetAlphaValue(ZoneImage.transform, currentPercentage);
    }

    #endregion

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
