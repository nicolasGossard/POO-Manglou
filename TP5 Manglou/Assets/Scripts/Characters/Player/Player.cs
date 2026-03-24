using UnityEngine;

public class Player : Character
{
    // L'inventaire est directement intégré dans la classe Player
    private Inventory inventory = new Inventory();

    [SerializeField] private Transform holderTransform;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private LayerMask itemLayer;

    private Vector3 move;

    protected override void Attack()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, itemLayer);

        foreach (Collider hit in hits)
        {
            Character enemy = hit.GetComponent<Character>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void PickUp()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, range, enemyLayer);

        foreach (Collider hit in hits)
        {
            Item item = hit.GetComponent<Item>();

            if (item != null)
            {
                inventory.AddItem(item);
            }
        }
    }

    protected override void Move()
    {
        transform.position += move * speed * Time.deltaTime;
    }

    // Méthodes publiques pour être apellées depuis l'exterieur

    public void HandleAttack()
    {
        Attack();
    }

    public void HandleMove(Vector3 vec)
    {
        move = vec;
        Move();
    }
}