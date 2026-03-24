using UnityEngine;

public abstract class Potion : Item
{
    [SerializeField] protected int healthRestored;
    [SerializeField] protected float duration;
}
