using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Character character;

    private Vector2 moveInput;

    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }

    private void Update()
    {
        MovePlayer();
        OrieantePlayer();
    }

    private void OrieantePlayer()
    {
        // Récupère la position de la souris en coordonnées écran
        Vector3 mouseScreenPos = Input.mousePosition;

        // Donne la bonne profondeur pour la conversion écran -> monde
        mouseScreenPos.z = -Camera.main.transform.position.z;

        // Convertit la position de la souris en position monde
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        // Calcule la direction du joueur vers la souris
        Vector2 direction = mouseWorldPos - transform.position;

        // Calcule l'angle à partir de cette direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Applique la rotation sur Z
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }


    private void MovePlayer()
    {
        Vector2 vec = moveInput;

        NormaliseVector2(ref vec);

        Vector2 move = vec * character.MoveSpeed * Time.deltaTime;

        transform.position += new Vector3(move.x, move.y, 0f);
    }

    private void NormaliseVector2(ref Vector2 vec)
    {
        float lenSq = vec.x * vec.x + vec.y * vec.y;

        if (lenSq < 1e-12f)
        {
            return;
        }

        float invLen = 1f / (float)Math.Sqrt(lenSq);

        vec.x *= invLen;
        vec.y *= invLen;
    }
}