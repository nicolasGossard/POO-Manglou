using UnityEngine;

public abstract class Enemy : Character
{
    public abstract string EnemyName { get; }

    public abstract void Attack();
    public abstract void Move();

    protected override void Die()
    {
        Debug.Log(EnemyName + " est mort");
        base.Die();
    }
}