using UnityEngine;

public class ConsumableStatusBar : MonoBehaviour 
{
    public ConsumableAgent consumable;

    public Transform BarPivot;
    public SpriteRenderer ColorSprite;
    public BarColor[] barColors;

    [System.Serializable]
    public struct BarColor
    {
        public ConsumableAgent.ChargableState state;
        public Color color;
    }

    private void Update()
    {
        if (consumable == null) return;

        SetBarView(consumable.percentage, consumable.ConsumableState);
    }

    private void SetBarView(float barPercentage, ConsumableAgent.ChargableState collectableState)
    {
        Vector3 calcScale = new Vector3(BarPivot.localScale.x, barPercentage, BarPivot.localScale.z);
        BarPivot.localScale = calcScale;

        SetBarColor(collectableState);
    }

    private void SetBarColor(ConsumableAgent.ChargableState collectableState)
    {
        foreach(BarColor barSpec in barColors)
        {
            if (barSpec.state.Equals(collectableState))
            {
                ColorSprite.color = barSpec.color;
                return;
            }
        }
    }
}
