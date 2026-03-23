using UnityEngine;

// Classe concrète représentant un bateau
public class Boat : Vehicle
{
    [Header("Spécifique bateau")]
    [SerializeField] private float boatBuoyancy = 1.5f;

    protected override float GetAccelerationMultiplier()
    {
        return 0.7f;
    }

    protected override float GetBrakeMultiplier()
    {
        return 0.6f;
    }

    protected override void HandleSteering(float turnInput, float moveInput)
    {
        // Le bateau tourne plus lentement
        transform.Rotate(
            0f,
            turnInput * handling * speed * 0.05f * Time.deltaTime,
            0f
        );
    }

    protected override void ApplySpecialBehaviour(float turnInput, float moveInput)
    {
        ApplyBoatBuoyancy();
    }

    private void ApplyBoatBuoyancy()
    {
        // Ajuste la hauteur pour rester sur l'eau
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 2.0f))
        {
            if (hit.collider.CompareTag("Water"))
            {
                float desiredHeight = hit.point.y + boatBuoyancy;

                Vector3 pos = transform.position;
                pos.y = Mathf.Lerp(pos.y, desiredHeight, Time.deltaTime * 2.0f);
                transform.position = pos;
            }
        }
    }
}