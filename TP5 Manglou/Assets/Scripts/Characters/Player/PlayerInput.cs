using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Update()
    {
        InputMove();
        InputAttack();
        InputPickUp();
    }

    private void InputMove()
    {
        Vector3 vec = Vector3.zero;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))  vec.x -= 1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) vec.x += 1f; 
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))    vec.z += 1f; 
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))  vec.z -= 1f;

        Maths.NormalizeVec3(ref vec);

        player.HandleMove(vec);
    }

    private void InputAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            player.HandleAttack();
        }
    }

    private void InputPickUp()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            player.PickUp();
        }
    }
}
