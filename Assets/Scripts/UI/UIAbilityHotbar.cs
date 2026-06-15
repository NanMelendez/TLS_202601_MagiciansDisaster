using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAbilityHotbar : MonoBehaviour
{
	[SerializeField] private GameObject slotPrefab;
	[SerializeField] private float spacing;
	[SerializeField] private Color unselectedColor = Color.white;
	[SerializeField] private Color selectedColor = Color.gold;

	private List<Image> slotsBgImgs = new List<Image>();
	private List<Slider> slotsSliders = new List<Slider>();

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

			iSlot.transform.Find("Background").GetComponent<Image>().color = unselectedColor;
            iSlot.transform.Find("AbilitySprite").GetComponent<Image>().sprite = abilities[i].refImage;

            slotsBgImgs.Add(iSlot.transform.Find("Background").GetComponent<Image>());
			slotsSliders.Add(iSlot.transform.Find("CooldownSlider").GetComponent<Slider>());
		}
	}

	public void UpdateSelection(int idx)
	{
		for (int i = 0; i < slotsBgImgs.Count; i++)
            slotsBgImgs[i].color = i == idx ? selectedColor : unselectedColor;
	}

	public void SetCooldownSlider(int idx, float cooldownTime)
	{
		slotsSliders[idx].value = 1.0f;
        StartCoroutine(AnimateCooldownSlider(idx, cooldownTime));
	}

	private IEnumerator AnimateCooldownSlider(int idx, float cooldownTime)
	{
		float elapsed = 0.0f;

		while (elapsed < cooldownTime)
		{
			elapsed += Time.deltaTime;

			slotsSliders[idx].value = 1.0f - elapsed / cooldownTime;

			yield return null;
		}

		slotsSliders[idx].value = 0.0f;
	}
}
