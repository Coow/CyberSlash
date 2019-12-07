using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/EnemyStats")]
public class EnemyStats : ScriptableObject {

    public float lookRange = 40f;
    public float lookSphereCastRadius = 5f;

    public float AttackRange = 10f;
    public float AttackRate = 1f;
    public int AttackDamage = 50;

    public float TurnSpeed = 5f;
}
