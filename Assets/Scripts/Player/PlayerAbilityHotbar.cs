using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityHotbar : MonoBehaviour
{
	[SerializeField] private PlayerMana mana;
	[SerializeField] private InputActionReference activator;
	[SerializeField] private List<Ability> abilities;
	[SerializeField] private UIAbilityHotbar UIHotbar;

	private int currentAbilityIdx = 0;
	private List<AbilityState> states;
	private List<float> activeTimes;
	private List<float> cooldownTimes;

	public int CurrentAbilityIdx
	{
		get { return currentAbilityIdx; }
	}

	public int AbilityCount
	{
		get { return abilities.Count; }
	}

	private void Awake()
	{
		states = Enumerable.Repeat(AbilityState.READY, abilities.Count).ToList();
		activeTimes = Enumerable.Repeat(0.0f, abilities.Count).ToList();
		cooldownTimes = Enumerable.Repeat(0.0f, abilities.Count).ToList();
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
		}
		else if (scrollY < 0.0f)
		{
			currentAbilityIdx--;
			if (currentAbilityIdx < 0)
				currentAbilityIdx = abilities.Count - 1;
		}

		if (scrollY != 0.0f)
			Debug.Log($"Now using {abilities[currentAbilityIdx].name}");
	}

	private void UpdateAbilitiesStatus(bool activatorBool)
	{
		for (int i = 0; i < abilities.Count; i++)
		{
			switch (states[i])
			{
				case AbilityState.READY:
					if (activatorBool)
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
		if (mana.CurrentMana - abilities[i].manaCost > 0)
		{
			abilities[i].Activate(gameObject);
			states[i] = AbilityState.ACTIVE;
			activeTimes[i] = abilities[i].activeTime;
			mana.ConsumeMana(abilities[i].manaCost);
		}
		else
			Debug.Log("Not enough mana!");
	}
}
