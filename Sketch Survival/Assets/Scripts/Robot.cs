using UnityEngine;

public class Robot : Enemy
{
    [Header("Robot Settings")]
    [SerializeField] private int laserDamage = 25;

    public override string EnemyName => "Robot";

    public override void Attack()
    {
        Debug.Log(EnemyName + " tire un laser et inflige " + laserDamage + " dégâts");
    }

    public override void Move()
    {
        Debug.Log(EnemyName + " se déplace mécaniquement à la vitesse de " + moveSpeed);
    }

    protected override void Die()
    {
        Debug.Log(EnemyName + " explose en morceaux");
        base.Die();
    }
}