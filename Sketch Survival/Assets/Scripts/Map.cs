using UnityEngine;

public class Map : MonoBehaviour
{
    [System.Serializable]
    public class SpawnableObject
    {
        [Header("Prefab à faire apparaître")]
        public GameObject prefab;

        [Header("Nom juste pour s’y retrouver dans l’inspecteur")]
        public string objectName;

        [Header("1 chance sur X")]
        [Min(1)]
        public int chanceSur = 2;

        [Header("Décalage aléatoire à l’intérieur de la case")]
        public bool randomOffsetInCell = true;

        [Header("Autoriser plusieurs exemplaires du même type par case ?")]
        public bool onePerCell = true;
    }

    [Header("Dimensions de la map en nombre de cases")]
    [Min(1)] public int mapWidth = 50;
    [Min(1)] public int mapHeight = 50;

    [Header("Taille d’une case en unités Unity")]
    [Min(0.1f)] public float cellSize = 1f;

    [Header("Origine de la map dans le monde")]
    public Vector2 mapOrigin = Vector2.zero;

    [Header("Objets à générer")]
    public SpawnableObject[] objectsToSpawn;

    [Header("Parent optionnel pour ranger la hiérarchie")]
    public Transform spawnedObjectsParent;

    private void Start()
    {
        GenerateMap();
    }

    /*
        Génère toute la map.
    */
    public void GenerateMap()
    {
        // Double boucle : on parcourt toutes les cases de la map
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                SpawnInCell(x, y);
            }
        }
    }

    /*
        Essaie de spawn les différents objets dans une case donnée.
    */
    private void SpawnInCell(int cellX, int cellY)
    {
        // Position du coin bas-gauche de la case
        Vector2 cellBottomLeft = new Vector2(
            mapOrigin.x + cellX * cellSize,
            mapOrigin.y + cellY * cellSize
        );

        foreach (SpawnableObject spawnable in objectsToSpawn)
        {
            // Sécurité : si aucun prefab n’est assigné, on ignore
            if (spawnable.prefab == null)
                continue;

            /*
                Random.Range avec des int :
                - borne min incluse
                - borne max exclue

                Donc Random.Range(0, chanceSur) == 0
                donne bien 1 chance sur chanceSur.
            */
            bool shouldSpawn = Random.Range(0, spawnable.chanceSur) == 0;

            if (!shouldSpawn)
                continue;

            Vector2 spawnPosition;

            if (spawnable.randomOffsetInCell)
            {
                // Position aléatoire à l’intérieur de la case
                float offsetX = Random.Range(0f, cellSize);
                float offsetY = Random.Range(0f, cellSize);

                spawnPosition = cellBottomLeft + new Vector2(offsetX, offsetY);
            }
            else
            {
                // Centre de la case
                spawnPosition = cellBottomLeft + new Vector2(cellSize * 0.5f, cellSize * 0.5f);
            }

            GameObject instance = Instantiate(
                spawnable.prefab,
                spawnPosition,
                Quaternion.identity
            );

            // Range l’objet dans un parent si fourni
            if (spawnedObjectsParent != null)
            {
                instance.transform.SetParent(spawnedObjectsParent);
            }
        }
    }

    /*
        Dessine la zone de la map dans l’éditeur pour mieux la visualiser.
    */
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Vector3 size = new Vector3(mapWidth * cellSize, mapHeight * cellSize, 0f);
        Vector3 center = new Vector3(
            mapOrigin.x + size.x * 0.5f,
            mapOrigin.y + size.y * 0.5f,
            0f
        );

        Gizmos.DrawWireCube(center, size);
    }
}