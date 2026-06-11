using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsHandler : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider manaSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth;
        healthText.text = $"HP\n{currentHealth} / {maxHealth}";
    }

    public void UpdateMana(int currentMana, int maxMana)
    {
        manaSlider.value = currentMana / maxMana;
        manaText.text = $"MANA\n{currentMana} / {maxMana}";
    }
}
