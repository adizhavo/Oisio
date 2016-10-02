using UnityEngine;
using Oisio.Agent;
using Oisio.Agent.Component;
using UnityEngine.UI;

namespace Oisio.Game
{
    public class UIStaminaBar : MonoBehaviour 
    {
        [SerializeField] private CharacterAgent observedAgent;

        [Header("UI Dependecies")]
        [SerializeField] private Image fillBar;

        private CharacterStaminaComponent staminaComponent;

    	private void Start () 
        {
            staminaComponent = observedAgent.RequestComponent<CharacterStaminaComponent>();
    	}

    	private void Update () 
        {
            if (staminaComponent == null) return;

            float percentage = staminaComponent.stamina / staminaComponent.maxStamina;
            fillBar.fillAmount = percentage;
    	}
    }
}