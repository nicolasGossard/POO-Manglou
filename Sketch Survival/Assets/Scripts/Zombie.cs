using UnityEngine;

public class Zombie : Enemy
{
    [Header("Zombie Settings")]
    [SerializeField] private int biteDamage = 10;

    public override string EnemyName => "Zombie";

    public override void Attack()
    {
        Debug.Log(EnemyName + " mord et inflige " + biteDamage + " dégâts");
    }

    public override void Move()
    {
        Debug.Log(EnemyName + " marche lentement à la vitesse de " + moveSpeed);
    }

    protected override void Die()
    {
        Debug.Log(EnemyName + " s'effondre au sol");
        base.Die();
    }
}