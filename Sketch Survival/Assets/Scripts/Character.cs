using System;
using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    [Header("Base Character Settings")]
    [SerializeField] protected int maxHealth = 100;
    [SerializeField] protected float moveSpeed = 3f;

    [SerializeField] protected int currentHealth;

    public int MaxHealth => maxHealth;
    public int CurrentHealth => currentHealth;
    public float MoveSpeed => moveSpeed;

    public event Action<int, int> OnHealthChanged;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        NotifyHealthChanged();
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        NotifyHealthChanged();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        NotifyHealthChanged();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected void NotifyHealthChanged()
    {
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}