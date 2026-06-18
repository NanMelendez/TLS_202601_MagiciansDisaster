using UnityEngine;

[CreateAssetMenu]
public class PlayerAtkRanged : AttackAbility
{
	[SerializeField] private GameObject projectile;
	[SerializeField] private float speed;
	[SerializeField] private float lifetime;
	[SerializeField] private float spawnOffset;

	public override void Activate(GameObject parent, bool charged)
	{
		PlayerAim pAim = parent.GetComponent<PlayerAim>();

		float angle = Mathf.Atan2(pAim.Direction.y, pAim.Direction.x);

		GameObject iProjectile = Instantiate(projectile, parent.transform.position + (Vector3)pAim.Direction * spawnOffset, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg));
		
		PlayerProjectile pp = iProjectile.GetComponent<PlayerProjectile>();
		pp.Init(damage, knockback, lifetime, pAim.Direction, speed, charged, effectType);
	}

	public override void BeginCooldown(GameObject parent)
	{
		// ...
	}
}
