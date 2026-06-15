using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private UIStatsHandler statsUI;

    private void Start()
    {
        statsUI.UpdateHealth(currentHealth, maxHealth);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        statsUI.UpdateHealth(currentHealth, maxHealth);
    }

    public override void Heal(int health)
    {
        base.Heal(health);
        statsUI.UpdateHealth(currentHealth, maxHealth);
    }
}
