using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMove playerMove;
    [SerializeField] private PlayerCombat playerCombat;

    private void Update()
    {
        HandleMovementInput();
        HandleCombatInput();
    }

    private void HandleMovementInput()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.A)) input.x -= 1f;
        if (Input.GetKey(KeyCode.D)) input.x += 1f;
        if (Input.GetKey(KeyCode.W)) input.y += 1f;
        if (Input.GetKey(KeyCode.S)) input.y -= 1f;

        playerMove.SetMoveInput(input);
    }

    private void HandleCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playerCombat.TryAttack();
        }
    }
}