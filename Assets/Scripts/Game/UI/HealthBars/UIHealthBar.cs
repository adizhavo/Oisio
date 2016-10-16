using UnityEngine;
using UnityEngine.UI;

namespace Oisio.Game
{
    public class UIHealthBar : HealthBar 
    {
        [SerializeField] private Image fillBar;

    	protected override void Update () 
        {
            base.Update();
            fillBar.fillAmount = percentage;
    	}
    }
}