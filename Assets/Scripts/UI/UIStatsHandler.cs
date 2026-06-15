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
        healthSlider.value = (float)currentHealth / (float)maxHealth;
        healthText.text = $"HP\n{currentHealth} / {maxHealth}";
    }

    public void UpdateMana(int currentMana, int maxMana)
    {

        manaSlider.value = (float)currentMana / (float)maxMana;
        manaText.text = $"MANA\n{currentMana} / {maxMana}";
    }
}
