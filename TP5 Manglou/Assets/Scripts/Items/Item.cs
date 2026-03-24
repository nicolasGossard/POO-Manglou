using NUnit.Framework.Internal;
using UnityEngine;

// Chaque Item peut être ramassé (potions, armes et armures)

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected string nameItem;
    [SerializeField] protected string description;
    [SerializeField] protected int weight;
    [SerializeField] public int value;

    public int Weight => weight;
}