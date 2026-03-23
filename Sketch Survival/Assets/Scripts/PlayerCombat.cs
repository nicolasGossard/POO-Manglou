using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private int unarmedDamage = 5;
    [SerializeField] private float unarmedRange = 1f;
    [SerializeField] private float unarmedCooldown = 0.1f;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private LayerMask attackableLayer;

    private Weapon equippedWeapon;
    private float nextAttackTime = 0f;

    public void EquipWeapon(Weapon newWeapon)
    {
        if (newWeapon == null)
        {
            return;
        }

        if (equippedWeapon != null)
        {
            Destroy(equippedWeapon.gameObject);
        }

        equippedWeapon = newWeapon;
        equippedWeapon.transform.SetParent(weaponHolder);
        equippedWeapon.transform.localPosition = Vector3.zero;
        equippedWeapon.transform.localRotation = Quaternion.identity;

        equippedWeapon.InitializeWeapon(weaponHolder);

        Collider2D weaponCollider = equippedWeapon.GetComponent<Collider2D>();
        if (weaponCollider != null)
        {
            weaponCollider.enabled = false;
        }

        Rigidbody2D weaponRigidbody = equippedWeapon.GetComponent<Rigidbody2D>();
        if (weaponRigidbody != null)
        {
            weaponRigidbody.simulated = false;
        }
    }

    public void TryAttack()
    {
        float cooldown = equippedWeapon != null ? equippedWeapon.AttackCooldown : unarmedCooldown;

        if (Time.time < nextAttackTime)
        {
            return;
        }

        nextAttackTime = Time.time + cooldown;

        int damage = equippedWeapon != null ? equippedWeapon.Damage : unarmedDamage;
        float range = equippedWeapon != null ? equippedWeapon.AttackRange : unarmedRange;

        if (equippedWeapon != null)
        {
            equippedWeapon.Attack();
        }
        else
        {
            Debug.Log("Le joueur attaque à mains nues");
        }

        DealDamageToClosestTarget(range, damage);
    }

    private void DealDamageToClosestTarget(float range, int damage)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, range, attackableLayer);

        if (hits.Length == 0)
        {
            return;
        }

        IDamageable closestTarget = null;
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < hits.Length; i++)
        {
            IDamageable damageable = hits[i].GetComponent<IDamageable>();

            if (damageable == null)
            {
                continue;
            }

            if (hits[i].gameObject == gameObject)
            {
                continue;
            }

            float distance = Vector2.Distance(transform.position, hits[i].transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = damageable;
            }
        }

        if (closestTarget != null)
        {
            closestTarget.TakeDamage(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, unarmedRange);

        if (equippedWeapon != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, equippedWeapon.AttackRange);
        }
    }
}