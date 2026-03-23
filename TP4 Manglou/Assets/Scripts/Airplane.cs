using UnityEngine;

// Classe concrète représentant un avion
public class Airplane : Vehicle
{
    [Header("Spécifique avion")]
    [SerializeField] private float airplaneLift = 5f;

    protected override float GetAccelerationMultiplier()
    {
        return 0.8f;
    }

    protected override float GetBrakeMultiplier()
    {
        return 0.4f;
    }

    protected override void HandleSteering(float turnInput, float moveInput)
    {
        // Rotation sur plusieurs axes : pitch, yaw, roll
        transform.Rotate(
            turnInput * handling * 0.5f * Time.deltaTime,
            moveInput * handling * 0.3f * Time.deltaTime,
            -turnInput * handling * Time.deltaTime
        );
    }

    protected override void ApplySpecialBehaviour(float turnInput, float moveInput)
    {
        ApplyAirplaneLift();
    }

    private void ApplyAirplaneLift()
    {
        // L'avion commence à prendre de l'altitude à partir d'une certaine vitesse
        if (speed > maxSpeed * 0.3f)
        {
            float liftForce = airplaneLift * (speed / maxSpeed);

            transform.Translate(
                Vector3.up * liftForce * Time.deltaTime,
                Space.World
            );
        }
    }
}