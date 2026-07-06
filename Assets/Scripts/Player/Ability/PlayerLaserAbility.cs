using UnityEngine;

[CreateAssetMenu]
public class PlayerLaserAbility : AttackAbility
{
	[SerializeField] private GameObject laser;
	[SerializeField] [Min(0.01f)] protected float maxRange = 0.01f;

	public override void Activate(GameObject parent, bool charged)
	{
		PlayerAim pAim = parent.GetComponent<PlayerAim>();
		PlayerAbilityHotbar atkHotbar = parent.GetComponent<PlayerAbilityHotbar>();

		GameObject iLaser = Instantiate(laser, parent.transform.position, Quaternion.identity);
		iLaser.transform.SetParent(parent.transform);

		PlayerLaser pl = iLaser.GetComponent<PlayerLaser>();
		pl.Init(damage, knockback, pAim, maxRange, effectDuration, charged, effectType, atkHotbar);
	}
}
