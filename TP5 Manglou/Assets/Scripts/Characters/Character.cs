using UnityEngine;

public abstract class Character : MonoBehaviour, IDamageable
{
    [SerializeField] protected string nameCharacter;
    [SerializeField] protected int healthMax;
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected float range;

    private int currentHealth;

    protected virtual void Start()
    {
        currentHealth = healthMax;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        Debug.Log("J'ai pris " + amount + " degats");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    protected abstract void Attack();
    protected abstract void Move();
}
