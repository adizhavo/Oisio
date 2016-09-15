using UnityEngine;
using System.Collections;

public abstract class DamageableAgent : Agent
{
    [Header("Health config")]
    public float maxHealth;
    public float healthRegen;
}