﻿using UnityEngine;
using Oisio.Agent;
using Oisio.Agent.Component;
using UnityEngine.UI;

namespace Oisio.Game
{
    public class UIHealthBar : MonoBehaviour 
    {
        [SerializeField] private DamageableAgent observedAgent;

        [Header("UI Dependecies")]
        [SerializeField] private Image fillBar;

        private AgentHealth healthComponent;

    	private void Start () 
        {
            healthComponent = observedAgent.RequestComponent<AgentHealth>();
    	}

    	private void Update () 
        {
            if (healthComponent == null) return;

            float percentage = healthComponent.health / healthComponent.maxhealth;
            fillBar.fillAmount = percentage;
    	}
    }
}