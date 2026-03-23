using UnityEngine;

public class EnemyTester : MonoBehaviour
{
    [SerializeField]
    private Enemy[] enemies;

    private void Start()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                continue;
            }

            enemies[i].Move();
            enemies[i].Attack();
            enemies[i].TakeDamage(30);
        }
    }
}