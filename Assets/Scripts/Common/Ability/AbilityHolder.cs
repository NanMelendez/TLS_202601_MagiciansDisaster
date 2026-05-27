using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    [SerializeField] private InputActionReference activatorAction;
    [SerializeField] private bool activatorBool;

    private AbilityState state = AbilityState.READY;
    private float cooldownTime;
    private float activeTime;

	private void OnEnable()
	{
		if (activatorAction)
			activatorAction.action.Enable();
	}

	private void OnDisable()
	{
		if (activatorAction)
			activatorAction.action.Disable();
	}

	private void Update()
    {
        if (ability)
            if (activatorAction)
                MyFunc(activatorAction.action.IsPressed() || activatorBool);
            else
				MyFunc(activatorBool);
		
        if (activatorBool)
			activatorBool = false;
	}

    private void MyFunc(bool activator)
    {
        switch (state)
        {
            case AbilityState.READY:
                if (activator)
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
