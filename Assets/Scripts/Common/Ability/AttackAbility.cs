using UnityEngine;

public class AttackAbility : Ability
{
	[SerializeField] protected int damage;
	[SerializeField] protected float knockback;
	[SerializeField] protected AttackEffectType effectType = AttackEffectType.None;
	[SerializeField] protected float effectDuration = 0.0f;
}
