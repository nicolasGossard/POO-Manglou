using UnityEngine;

public class Player : Character
{
    [Header("Player Settings")]
    [SerializeField] private int attackDamage = 20;

    protected override void Die()
    {
        Debug.Log("Le joueur est mort");
        base.Die();
    }
}