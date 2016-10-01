using UnityEngine;
using Oisio.Agent;

namespace Oisio.Game
{
    public class ConsumableStatusBar : MonoBehaviour 
    {
        public ConsumableAgent consumable;

        public Transform BarPivot;
        public SpriteRenderer ColorSprite;
        public BarColor[] barColors;

        [System.Serializable]
        public struct BarColor
        {
            public ConsumableAgent.ChargeState state;
            public Color color;
        }

        private void Update()
        {
            if (consumable == null) return;

            SetBarView(consumable.percentage, consumable.ConsumableState);
        }

        private void SetBarView(float barPercentage, ConsumableAgent.ChargeState collectableState)
        {
            Vector3 calcScale = new Vector3(BarPivot.localScale.x, barPercentage, BarPivot.localScale.z);
            BarPivot.localScale = calcScale;

            SetBarColor(collectableState);
        }

        private void SetBarColor(ConsumableAgent.ChargeState collectableState)
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
}