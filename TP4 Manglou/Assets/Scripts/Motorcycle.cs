using UnityEngine;

// Classe concrète représentant une moto
public class Motorcycle : Vehicle
{
    [Header("Spécifique moto")]
    [SerializeField] private float motorcycleLeanAngle = 25f;

    protected override float GetAccelerationMultiplier()
    {
        // Les motos accélèrent plus vite
        return 1.2f;
    }

    protected override float GetBrakeMultiplier()
    {
        return 0.8f;
    }

    protected override void HandleSteering(float turnInput, float moveInput)
    {
        // Direction plus sensible que la voiture
        transform.Rotate(
            0f,
            turnInput * handling * speed * 0.15f * Time.deltaTime,
            0f
        );
    }

    protected override void ApplySpecialBehaviour(float turnInput, float moveInput)
    {
        ApplyMotorcycleLean(turnInput);
    }

    private void ApplyMotorcycleLean(float turnInput)
    {
        // Inclinaison latérale dans les virages
        float targetLean = -turnInput * motorcycleLeanAngle;

        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.z = Mathf.LerpAngle(
            currentRotation.z,
            targetLean,
            Time.deltaTime * 2.0f
        );

        transform.localEulerAngles = currentRotation;
    }
}