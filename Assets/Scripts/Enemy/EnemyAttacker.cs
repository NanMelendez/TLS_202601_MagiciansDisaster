using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private GameObject projectileInstance;

    private int damage;
    private AttackEffectType effectType;

    public int Damage
    {
        get
        {
            return damage;
        }
    }

    public void Init(int damage, AttackEffectType effectType)
    {
        this.damage = damage;
        this.effectType = effectType;
    }

    public void Fire(Vector2 direction, float speed)
    {

    }
}
