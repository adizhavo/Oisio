using UnityEngine;
using UnityEngine.UI;

public class UIStaminaBar : MonoBehaviour 
{
    [SerializeField] private Agent observedAgent;

    [Header("UI Dependecies")]
    [SerializeField] private Image fillBar;

    private AgentStamina staminaComponent;

	private void Start () 
    {
        staminaComponent = observedAgent.RequestComponent<AgentStamina>();
	}

	private void Update () 
    {
        if (staminaComponent == null) return;

        float percentage = staminaComponent.stamina / staminaComponent.maxStamina;
        fillBar.fillAmount = percentage;
	}
}
