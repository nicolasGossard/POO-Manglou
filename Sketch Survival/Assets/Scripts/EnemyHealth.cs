using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Image fillImage;

    private void Start()
    {
        sliderHealth.interactable = false;
        sliderHealth.minValue = 0;
        sliderHealth.maxValue = enemy.MaxHealth;
        sliderHealth.wholeNumbers = true;
        sliderHealth.value = enemy.CurrentHealth;

        enemy.OnHealthChanged += UpdateHealthUI;

        UpdateHealthUI(enemy.CurrentHealth, enemy.MaxHealth);
    }

    private void OnDestroy()
    {
        enemy.OnHealthChanged -= UpdateHealthUI;
    }

    private void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        sliderHealth.value = currentHealth;

        float healthPercent = (float)currentHealth / maxHealth;

        fillImage.color = Color.Lerp(Color.red, Color.green, healthPercent);
    }
}