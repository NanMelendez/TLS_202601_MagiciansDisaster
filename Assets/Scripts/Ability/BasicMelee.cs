using UnityEngine;

[CreateAssetMenu]
public class BasicMelee : Ability
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private GameObject meleeRange;
    [SerializeField]
    private Vector2 offset;

    public override void Activate(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();

        float angle = Mathf.Atan2(movement.AimDirection.y, movement.AimDirection.x) * Mathf.Rad2Deg;

        GameObject area = Instantiate(meleeRange, parent.transform.position + (Vector3)movement.AimDirection, Quaternion.Euler(0.0f, 0.0f, angle + 90));
        area.transform.SetParent(parent.transform);
        MeleeArea ma = area.GetComponent<MeleeArea>();
        ma.Init(damage, activeTime);
    }

    public override void BeginCooldown(GameObject parent)
    {
        // ...
    }
}
