using UnityEngine;

[CreateAssetMenu]
public class PlayerAtkRanged : AttackAbility
{
	[SerializeField] private GameObject projectile;
	[SerializeField] private float speed;
	[SerializeField] private float lifetime;
	[SerializeField] private float spawnOffset;

	private float chargedStrengthFactor = 3.0f;
	private float chargedSpeedFactor = 2.0f;

	public override void Activate(GameObject parent, bool charged)
	{
		PlayerAim pAim = parent.GetComponent<PlayerAim>();

		float angle = Mathf.Atan2(pAim.Direction.y, pAim.Direction.x);

		GameObject iProjectile = Instantiate(projectile, parent.transform.position + (Vector3)pAim.Direction * spawnOffset, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg));
		
		PlayerProjectile pp = iProjectile.GetComponent<PlayerProjectile>();

		pp.Init(charged ? (int)(chargedStrengthFactor * damage) : damage, knockback, lifetime, pAim.Direction, charged ? (int)(chargedSpeedFactor * speed) : speed, effectDuration, charged, effectType, abilitySpawnSFX, abilityLandSFX);
	}

	public override void BeginCooldown(GameObject parent)
	{
		// ...
	}
}
