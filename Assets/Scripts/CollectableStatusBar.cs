using UnityEngine;
using System.Collections;

public class CollectableStatusBar : MonoBehaviour 
{
    public Transform BarPivot;
    public SpriteRenderer ColorSprite;

    private float maxBar;
    public BarColor[] barColors;

    [System.Serializable]
    public struct BarColor
    {
        public Collectable.State state;
        public Color color;
    }

    public void Init(float maxBar)
    {
        this.maxBar = maxBar;
    }

    public void SetBarView(float barPercentage, Collectable.State collectableState)
    {
        Vector3 calcScale = new Vector3(BarPivot.localScale.x, barPercentage * maxBar, BarPivot.localScale.z);
        BarPivot.localScale = calcScale;

        SetBarColor(collectableState);
    }

    private void SetBarColor(Collectable.State collectableState)
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
