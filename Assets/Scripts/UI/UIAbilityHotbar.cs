using System.Collections.Generic;
using UnityEngine;

public class UIAbilityHotbar : MonoBehaviour
{
	[SerializeField] private GameObject slotPrefab;
	[SerializeField] private float spacing;

	public void CreateLayout(List<Ability> abilities)
	{
		if (!slotPrefab)
			return;

		int nSlots = abilities.Count;

		float w = slotPrefab.GetComponent<RectTransform>().rect.width;
		float step = w + spacing;
		float totalW = (nSlots * w) + ((nSlots - 1) * spacing);

		float offset = (-totalW / 2.0f) + (w / 2.0f);

		for (int i = 0; i < abilities.Count; i++)
		{
			int _i = i;
			GameObject iSlot = Instantiate(slotPrefab, transform);

			RectTransform slotRect = iSlot.GetComponent<RectTransform>();

			slotRect.anchoredPosition = new Vector2(offset + (i * step), 0);
			iSlot.name = $"Slot {_i + 1}";
		}
	}
}
