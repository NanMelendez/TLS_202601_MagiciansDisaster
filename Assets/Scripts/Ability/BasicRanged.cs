using UnityEngine;

[CreateAssetMenu]
public class BasicRanged : Ability
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private GameObject pellet;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float lifetime;
    [SerializeField]
    private Vector2 offset;

    public override void Activate(GameObject parent)
    {
        PlayerMovement movement = parent.GetComponent<PlayerMovement>();

        float angle = Mathf.Atan2(movement.AimDirection.y, movement.AimDirection.x);
        Vector2 rotatedOffset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * offset.magnitude;

        GameObject iPellet = Instantiate(pellet, parent.transform.position + (Vector3)rotatedOffset, Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg + 90));
        Pellet p = iPellet.GetComponent<Pellet>();
        p.Init(damage, lifetime, speed, movement.AimDirection);
    }

    public override void BeginCooldown(GameObject parent)
    {
        // ...
    }
}
