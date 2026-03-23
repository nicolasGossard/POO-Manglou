using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Base Weapon Settings")]
    [SerializeField] protected int maxDurability = 100;
    [SerializeField] protected float attackRange = 2f;
    [SerializeField] protected int damage = 10;
    [SerializeField] protected float attackCooldown = 0.5f;

    [SerializeField] protected int currentDurability;

    protected bool isAttacking = false;

    // Référence vers la main / le pivot qui tient l'arme
    protected Transform weaponHolder;

    // Pose de repos de la main
    protected Vector3 initialHolderLocalPosition;
    protected Quaternion initialHolderLocalRotation;

    public int MaxDurability => maxDurability;
    public int CurrentDurability => currentDurability;
    public int Damage => damage;
    public float AttackRange => attackRange;
    public float AttackCooldown => attackCooldown;
    public bool IsAttacking => isAttacking;

    protected virtual void Start()
    {
        currentDurability = maxDurability;
    }

    public virtual void InitializeWeapon(Transform holder)
    {
        weaponHolder = holder;

        // Sauvegarde la pose de repos de la main
        initialHolderLocalPosition = weaponHolder.localPosition;
        initialHolderLocalRotation = weaponHolder.localRotation;
    }

    public virtual void ReduceDurability(int amount)
    {
        currentDurability -= amount;

        if (currentDurability < 0)
        {
            currentDurability = 0;
        }

        if (currentDurability <= 0)
        {
            Break();
        }
    }

    public abstract void Attack();

    protected virtual void Break()
    {
        Destroy(gameObject);
    }
}