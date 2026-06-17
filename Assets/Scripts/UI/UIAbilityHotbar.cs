using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAbilityHotbar : MonoBehaviour
{
	[SerializeField] private GameObject slotPrefab;
	[SerializeField] private float spacing;
	[SerializeField] private Color unselectedColor = Color.white;
	[SerializeField] private Color selectedColor = Color.gold;
	[SerializeField] private TextMeshProUGUI currentAbilityText;

    private List<Image> slotsBgImgs = new List<Image>();
	private List<Slider> slotsSliders = new List<Slider>();
	private List<string> slotsNames = new List<string>();

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
			slotsNames.Add(abilities[i].name);
		}
	}

	public void UpdateSelection(int idx)
	{
		for (int i = 0; i < slotsBgImgs.Count; i++)
            slotsBgImgs[i].color = i == idx ? selectedColor : unselectedColor;
		currentAbilityText.text = slotsNames[idx];
		StartCoroutine(AnimateTextFade(1.5f));
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

	private IEnumerator AnimateTextFade(float textFadeTime)
	{
        currentAbilityText.gameObject.SetActive(true);
        float elapsed = 0.0f;

		while (elapsed < textFadeTime)
		{
			elapsed += Time.deltaTime;

			currentAbilityText.color = new Color(currentAbilityText.color.r, currentAbilityText.color.g, currentAbilityText.color.r, 1.0f - elapsed / textFadeTime);

			yield return null;
		}

        currentAbilityText.color = new Color(currentAbilityText.color.r, currentAbilityText.color.g, currentAbilityText.color.r, 1.0f);
        currentAbilityText.gameObject.SetActive(false);
    }
}
