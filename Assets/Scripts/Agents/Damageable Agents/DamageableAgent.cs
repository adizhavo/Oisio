using UnityEngine;
using System.Collections;

namespace Oisio.Agent
{
    public abstract class DamageableAgent : Agent
    {
        [Header("Health config")]
        public float maxHealth;
        public float healthRegen;
    }
}