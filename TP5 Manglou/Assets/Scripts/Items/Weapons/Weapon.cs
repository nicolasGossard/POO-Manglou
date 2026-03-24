using UnityEngine;

public abstract class Weapon : Item, IDamageable
{
    [SerializeField] protected int damage;
    [SerializeField] protected float range;

    private int currentHealth;

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Break();
        }
    }

    protected virtual void Break()
    {
        Destroy(gameObject);
    }
}