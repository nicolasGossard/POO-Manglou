using UnityEngine;

public class BreakableCrate : MonoBehaviour, IDamageable
{
    [Header("Crate Settings")]
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float shrinkPerHit = 0.1f;
    [SerializeField] private float minimumScale = 0.5f;

    [Header("Drop Settings")]
    [SerializeField] private GameObject[] possibleDrops;

    private int currentHealth;
    private Vector3 currentScale;

    private void Awake()
    {
        currentHealth = maxHealth;
        currentScale = transform.localScale;
    }

    public void TakeDamage(int damage)
    {
         Debug.Log("La boite prend " + damage + " dégâts. PV avant = " + currentHealth);
         
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        Debug.Log("PV après = " + currentHealth);

        Shrink();

        if (currentHealth <= 0)
        {
            Break();
        }
    }

    private void Shrink()
    {
        float newScaleX = Mathf.Max(minimumScale, transform.localScale.x - shrinkPerHit);
        float newScaleY = Mathf.Max(minimumScale, transform.localScale.y - shrinkPerHit);

        transform.localScale = new Vector3(newScaleX, newScaleY, transform.localScale.z);
    }

    private void Break()
    {
        SpawnRandomDrop();
        Destroy(gameObject);
    }

    private void SpawnRandomDrop()
    {
        if (possibleDrops == null || possibleDrops.Length == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, possibleDrops.Length);
        GameObject selectedDrop = possibleDrops[randomIndex];

        Instantiate(selectedDrop, transform.position, Quaternion.identity);
    }
}