using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon weaponPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCombat playerCombat = other.GetComponent<PlayerCombat>();

        if (playerCombat == null)
        {
            return;
        }

        Weapon newWeaponInstance = Instantiate(weaponPrefab);
        playerCombat.EquipWeapon(newWeaponInstance);

        Destroy(gameObject);
    }
}