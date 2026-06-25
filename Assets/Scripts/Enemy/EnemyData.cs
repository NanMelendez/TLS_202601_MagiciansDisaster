using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Stats")]
    public int health;
    public float hurtCooldown;
    public float speed;
    public float destroyAfterSeconds = 1.5f;

    [Header("Attack")]
    public int damage;
    public EnemyAtkType attackType = EnemyAtkType.Melee;
    public AttackEffectType effectType = AttackEffectType.None;
    public EnemyActionSpot actionOnSpot = EnemyActionSpot.Follow;
    
    [Header("Vision range")]
    [Range(1.0f, 360.0f)] public float visionAngle;
    public float visionRadius;
}
