using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityHotbar : MonoBehaviour
{
	[SerializeField] private PlayerMana mana;
	[SerializeField] private InputActionReference activator;
	[SerializeField] private InputActionReference charger;
	[SerializeField] private List<InputActionReference> hotbarNumbers = new();
	[SerializeField] private List<Ability> abilities = new();
	[SerializeField] private UIAbilityHotbar UIHotbar;
	[SerializeField] private float chargeThreshold;
	[SerializeField] private UIStatsHandler UIHandler;
	[SerializeField] private GameObject chargeParticles;
	[SerializeField] private FlashController flash;
	
	private int currentAbilityIdx = 0;
	private List<AbilityState> states = new();
	private List<float> activeTimes = new();
	private List<float> cooldownTimes = new();

	private float chargeTime;
	private bool isUsingLaser = false;

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
		HandleManualSelection();
		ControlScroll();

		if (charger)
			ChargeAttackHandler(charger.action.IsPressed());
		if (activator)
			UpdateAbilitiesStatus(activator.action.IsPressed());
	}

	private bool SlotIsLaser(int idx)
	{
		return abilities[idx] is PlayerLaserAbility;
	}

	private void HandleManualSelection()
	{
		for (int i = 0; i < hotbarNumbers.Count; i++)
		{
			if (hotbarNumbers[i].action.WasPressedThisFrame())
			{
				currentAbilityIdx = i;
				UIHotbar.UpdateSelection(currentAbilityIdx);
				chargeTime = 0.0f;
				SFXManager.instance.PlayClip(abilities[currentAbilityIdx].abilityInvokeSFX, transform, 1.0f);
			}
		}
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
			SFXManager.instance.PlayClip(abilities[currentAbilityIdx].abilityInvokeSFX, transform, 1.0f);
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
					if (i == currentAbilityIdx)
					{
						if (activatorBool)
						{
							EvalAbilityUsage(i);
						}
						else
						{
							if (SlotIsLaser(i) && isUsingLaser)
								isUsingLaser = false;
						}
					}
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
			if (SlotIsLaser(i))
			{
				if (!isUsingLaser)
				{
					isUsingLaser = true;
					abilities[i].Activate(gameObject, chargeTime >= chargeThreshold); ;
				}
			}
			else
				abilities[i].Activate(gameObject, chargeTime >= chargeThreshold);

			states[i] = AbilityState.ACTIVE;
			activeTimes[i] = abilities[i].activeTime;
			mana.ConsumeMana(abilities[i].manaCost);
			UIHandler.UpdateMana(mana.CurrentMana, mana.MaxMana);
			UIHotbar.SetCooldownSlider(i, abilities[i].cooldownTime);
		}
		else
		{
			if (SlotIsLaser(i) && isUsingLaser)
				isUsingLaser = false;
		}
	}

	private void ChargeAttackHandler(bool isPressed)
	{
		if (isPressed)
		{
			chargeParticles.SetActive(true);
			if (chargeTime == 0.0f)
				flash.TintThenFlash(Color.purple, Color.white, chargeThreshold, 0.25f);
			chargeTime += Time.deltaTime;
		}
		else
		{
			chargeTime = 0.0f;
			chargeParticles.SetActive(false);
			flash.StopTint();
			flash.StopFlash();
		}
	}
}
