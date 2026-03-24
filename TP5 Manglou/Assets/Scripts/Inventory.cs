using UnityEngine;

public class Inventory
{
    public Item[] items = new Item[20];
    public int itemCount;

    public void Start()
    {
        itemCount = 0;
    }

    public void AddItem(Item item)
    {
        if (itemCount >= items.Length)
        {
            Debug.Log("Impossible de rajouter l'item dans l'inventaire (manque de place)");
            Debug.Log("Il ne vous reste plus de place dans l'inventaire");
            return;
        }

        items[itemCount] = item;
        itemCount++;
        Debug.Log("Objet rajouté à l'inventaire");
    }
    
    public void RemoveItem(int index)
    {
        if (index >= 0 && index < itemCount)
        {
            for (int i = index; i < itemCount - 1; i++)
            {
                items[i] = items[i + 1];
            }
            items[itemCount - 1] = null;
            itemCount--;
        }
    }
}