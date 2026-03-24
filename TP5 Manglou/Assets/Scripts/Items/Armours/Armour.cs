using UnityEngine;

public class Armour : Item, IDamageable
{
    [SerializeField] protected int defense;
    [SerializeField] protected string armorType;

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
