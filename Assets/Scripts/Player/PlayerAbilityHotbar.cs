using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityHotbar : MonoBehaviour
{
	[SerializeField] private PlayerMana mana;
	[SerializeField] private InputActionReference activator;
	[SerializeField] private InputActionReference charger;
	[SerializeField] private List<Ability> abilities;
	[SerializeField] private UIAbilityHotbar UIHotbar;
	[SerializeField] private float chargeThreshold;
	[SerializeField] private UIStatsHandler UIHandler;
	[SerializeField] private GameObject chargeParticles;

	private int currentAbilityIdx = 0;
	private List<AbilityState> states;
	private List<float> activeTimes;
	private List<float> cooldownTimes;

	private float chargeTime;

	public int CurrentAbilityIdx
	{
		get { return currentAbilityIdx; }
	}

	public int AbilityCount
	{
		get { return abilities.Count; }
	}

	public bool IsAttacking
	{
		get { return activator ? activator.action.IsPressed() : false; }
	}

	public bool IsCharging
	{
		get { return charger ? charger.action.IsPressed() : false; }
	}

	private void Awake()
	{
		states = Enumerable.Repeat(AbilityState.READY, abilities.Count).ToList();
		activeTimes = Enumerable.Repeat(0.0f, abilities.Count).ToList();
		cooldownTimes = Enumerable.Repeat(0.0f, abilities.Count).ToList();

		UIHotbar.CreateLayout(abilities);
		UIHotbar.UpdateSelection(currentAbilityIdx);
    }

	private void OnEnable()
	{
		if (activator)
			activator.action.Enable();
	}

	private void OnDisable()
	{
		if (activator)
			activator.action.Disable();
	}

	private void Update()
	{
		ControlScroll();
		if (charger)
			ChargeAttackHandler(charger.action.IsPressed());
		if (activator)
			UpdateAbilitiesStatus(activator.action.IsPressed());
	}

	private void ControlScroll()
	{
		Vector2 scrollDelta = Mouse.current.scroll.ReadValue();
		float scrollY = scrollDelta.y;

		if (scrollY > 0.0f)
		{
			currentAbilityIdx++;
			if (currentAbilityIdx >= abilities.Count)
				currentAbilityIdx = 0;
			chargeTime = 0.0f;
		}
		else if (scrollY < 0.0f)
		{
			currentAbilityIdx--;
			if (currentAbilityIdx < 0)
				currentAbilityIdx = abilities.Count - 1;
			chargeTime = 0.0f;
		}

		if (scrollY != 0.0f)
		{
			Debug.Log($"Now using {abilities[currentAbilityIdx].name}");
			UIHotbar.UpdateSelection(currentAbilityIdx);
		}
	}

	private void UpdateAbilitiesStatus(bool activatorBool)
	{
		for (int i = 0; i < abilities.Count; i++)
		{
            switch (states[i])
            {
                case AbilityState.READY:
                    if (activatorBool && i == currentAbilityIdx)
                        EvalAbilityUsage(i);
                    break;
                case AbilityState.ACTIVE:
                    if (activeTimes[i] > 0)
                        activeTimes[i] -= Time.deltaTime;
                    else
                    {
                        abilities[i].BeginCooldown(gameObject);
                        states[i] = AbilityState.COOLDOWN;
                        cooldownTimes[i] = abilities[i].cooldownTime;
                    }
                    break;
                case AbilityState.COOLDOWN:
                    if (cooldownTimes[i] > 0)
                        cooldownTimes[i] -= Time.deltaTime;
                    else
                        states[i] = AbilityState.READY;
                    break;
            }
        }
	}

	private void EvalAbilityUsage(int i)
	{
		if (mana.CurrentMana - abilities[i].manaCost >= 0)
		{
			abilities[i].Activate(gameObject, chargeTime >= chargeThreshold);
			states[i] = AbilityState.ACTIVE;
			activeTimes[i] = abilities[i].activeTime;
			mana.ConsumeMana(abilities[i].manaCost);
			UIHandler.UpdateMana(mana.CurrentMana, mana.MaxMana);
			UIHotbar.SetCooldownSlider(i, abilities[i].cooldownTime);
		}
		else
			Debug.Log("Not enough mana!");
	}

	private void ChargeAttackHandler(bool isPressed)
	{
		if (isPressed)
		{
            chargeTime += Time.deltaTime;
			chargeParticles.SetActive(true);
        }
		else
		{
            chargeTime = 0.0f;
			chargeParticles.SetActive(false);
        }
	}
}
