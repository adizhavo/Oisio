using UnityEngine;
using System.Collections;

public abstract class DamagableAgent : Agent
{
    [Header("Health config")]
    public float maxHealth;
    public float healthRegen;
}