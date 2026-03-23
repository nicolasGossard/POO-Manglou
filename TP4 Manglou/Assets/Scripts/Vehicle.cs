using UnityEngine;

// Classe abstraite de base pour tous les véhicules
public abstract class Vehicle : MonoBehaviour
{
    [Header("Statistiques communes")]
    [SerializeField] protected float speed = 0f;
    [SerializeField] protected float maxSpeed = 20f;
    [SerializeField] protected float acceleration = 10f;
    [SerializeField] protected float handling = 5f;
    [SerializeField] protected float brakeForce = 8f;

    protected virtual void Update()
    {
        // Lecture des entrées communes
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Gestion commune du mouvement
        HandleAcceleration(moveInput);
        HandleSteering(turnInput, moveInput);

        // Clamp de la vitesse pour éviter les valeurs incohérentes
        speed = Mathf.Clamp(speed, 0f, maxSpeed);

        // Déplacement commun de base
        Move();

        // Comportement spécifique supplémentaire
        ApplySpecialBehaviour(turnInput, moveInput);
    }

    // Gestion commune accélération / freinage
    protected virtual void HandleAcceleration(float moveInput)
    {
        if (moveInput > 0f)
        {
            // L'intensité réelle de l'accélération dépend du véhicule
            speed += GetAccelerationMultiplier() * acceleration * moveInput * Time.deltaTime;
        }
        else if (moveInput < 0f)
        {
            // Même idée pour le freinage
            speed -= GetBrakeMultiplier() * brakeForce * Mathf.Abs(moveInput) * Time.deltaTime;
        }
    }

    // Déplacement commun
    protected virtual void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    // Méthodes abstraites : chaque véhicule doit définir son propre comportement
    protected abstract void HandleSteering(float turnInput, float moveInput);
    protected abstract void ApplySpecialBehaviour(float turnInput, float moveInput);
    protected abstract float GetAccelerationMultiplier();
    protected abstract float GetBrakeMultiplier();
}