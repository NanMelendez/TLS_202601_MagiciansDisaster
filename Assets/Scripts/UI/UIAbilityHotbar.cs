using UnityEngine;

public class UIAbilityHotbar : MonoBehaviour
{
	[SerializeField] private PlayerAbilityHotbar abilities;
	[SerializeField] private GameObject slotPrefab;

	private void Awake()
	{
		int slotW, slotH;
		slotW = (int)slotPrefab.GetComponent<RectTransform>().rect.width;
		slotH = (int)slotPrefab.GetComponent<RectTransform>().rect.height;

		for (int i = 0; i < abilities.AbilityCount; i++)
		{
			GameObject slot = Instantiate(slotPrefab, transform);
			int n = i;
			slot.name = $"Slot {n + 1}";
		}
	}
}
