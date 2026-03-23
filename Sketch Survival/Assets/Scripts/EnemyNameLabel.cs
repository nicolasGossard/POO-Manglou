using TMPro;
using UnityEngine;

public class EnemyNameLabel : MonoBehaviour
{
    [Header("References")]

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private TextMeshPro textMesh;

    [Header("Display Settings")]

    [SerializeField]
    private bool faceCamera = true;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;

        if (enemy == null)
        {
            enemy = GetComponentInParent<Enemy>();
        }

        if (textMesh == null)
        {
            textMesh = GetComponent<TextMeshPro>();
        }

        UpdateLabel();
    }

    private void LateUpdate()
    {
        if (faceCamera)
        {
            FaceCamera();
        }
    }

    public void UpdateLabel()
    {
        if (enemy == null || textMesh == null)
        {
            return;
        }   

        textMesh.text = enemy.EnemyName;
    }

    private void FaceCamera()
    {
        if (mainCamera == null)
        {
            return;
        }

        transform.forward = mainCamera.transform.forward;
    }
}