using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityHolder : MonoBehaviour
{
    enum AbilityState
    {
        READY,
        ACTIVE,
        COOLDOWN
    };

    public Ability ability;
    [SerializeField]
    private InputActionReference activateAction;
    private AbilityState state = AbilityState.READY;
    private float cooldownTime;
    private float activeTime;

    void Update()
    {
        if (ability)
            switch (state)
            {
                case AbilityState.READY:
                    if (activateAction.action.IsPressed())
                    {
                        ability.Activate(gameObject);
                        state = AbilityState.ACTIVE;
                        activeTime = ability.activeTime;
                    }
                    break;
                case AbilityState.ACTIVE:
                    if (activeTime > 0)
                        activeTime -= Time.deltaTime;
                    else
                    {
                        ability.BeginCooldown(gameObject);
                        state = AbilityState.COOLDOWN;
                        cooldownTime = ability.cooldownTime;
                    }
                    break;
                case AbilityState.COOLDOWN:
                    if (cooldownTime > 0)
                        cooldownTime -= Time.deltaTime;
                    else
                        state = AbilityState.READY;
                break;
            }
    }
}
