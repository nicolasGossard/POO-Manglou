using UnityEngine;

// Classe concrète représentant une voiture
public class Car : Vehicle
{
    [Header("Spécifique voiture")]
    [SerializeField] private float carTraction = 0.9f;

    protected override float GetAccelerationMultiplier()
    {
        return 1.0f;
    }

    protected override float GetBrakeMultiplier()
    {
        return 1.0f;
    }

    protected override void HandleSteering(float turnInput, float moveInput)
    {
        // Direction classique d'une voiture
        transform.Rotate(
            0f,
            turnInput * handling * speed * 0.1f * Time.deltaTime,
            0f
        );
    }

    protected override void ApplySpecialBehaviour(float turnInput, float moveInput)
    {
        // La traction s'applique surtout quand la voiture roule
        ApplyCarTraction();
    }

    private void ApplyCarTraction()
    {
        // Simule la perte d'adhérence selon le sol
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1.0f))
        {
            float surfaceFactor = 1.0f;

            if (hit.collider.CompareTag("Dirt"))
            {
                surfaceFactor = 0.7f;
            }
            else if (hit.collider.CompareTag("Ice"))
            {
                surfaceFactor = 0.3f;
            }

            speed *= (1.0f - (1.0f - carTraction) * (1.0f - surfaceFactor));
        }
    }
}