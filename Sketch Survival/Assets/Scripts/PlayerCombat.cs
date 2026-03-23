using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [Header("Unarmed Settings")]
    [SerializeField] private int unarmedDamage = 5;
    [SerializeField] private float unarmedRange = 1f;
    [SerializeField] private float unarmedCooldown = 0.1f;

    [Header("Unarmed Hand References")]
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    [Header("Unarmed Animation")]
    [SerializeField] private float punchDistance = 0.2f;
    [SerializeField] private float punchDuration = 0.06f;

    [Header("Weapon Settings")]
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private LayerMask attackableLayer;

    private Weapon equippedWeapon;
    private float nextAttackTime = 0f;

    // Permet d'alterner : main gauche, puis main droite, puis gauche, etc.
    private bool useLeftHand = true;

    // On garde la position de repos des mains
    private Vector3 leftHandStartLocalPosition;
    private Vector3 rightHandStartLocalPosition;

    private void Start()
    {
        // On sauvegarde la position locale de départ des mains
        if (leftHand != null)
        {
            leftHandStartLocalPosition = leftHand.localPosition;
        }

        if (rightHand != null)
        {
            rightHandStartLocalPosition = rightHand.localPosition;
        }
    }

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
            StartCoroutine(DoUnarmedAttackAnimation());
        }

        DealDamageToClosestTarget(range, damage);
    }

    private IEnumerator DoUnarmedAttackAnimation()
    {
        // Sélection de la main qui attaque cette fois-ci
        Transform attackingHand = useLeftHand ? leftHand : rightHand;

        if (attackingHand == null)
        {
            yield break;
        }

        Vector3 startPos = useLeftHand ? leftHandStartLocalPosition : rightHandStartLocalPosition;

        // Ici, je pars du principe que "avant" = vers la droite locale.
        // Si ton perso regarde toujours à droite, ça ira.
        // Sinon il faudra le lier à la direction du personnage.
        Vector3 endPos = startPos + Vector3.right * punchDistance;

        float time = 0f;

        // Aller
        while (time < punchDuration)
        {
            time += Time.deltaTime;
            float t = time / punchDuration;
            attackingHand.localPosition = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        time = 0f;

        // Retour
        while (time < punchDuration)
        {
            time += Time.deltaTime;
            float t = time / punchDuration;
            attackingHand.localPosition = Vector3.Lerp(endPos, startPos, t);
            yield return null;
        }

        attackingHand.localPosition = startPos;

        // On alterne pour la prochaine attaque
        useLeftHand = !useLeftHand;
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