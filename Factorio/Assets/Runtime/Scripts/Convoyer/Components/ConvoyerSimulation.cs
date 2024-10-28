using Unity.Entities;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConvoyerSimulation : MonoBehaviour
{
    public GameObject? tilePrefab; // Préfab pour le convoyeur
    public Tilemap mainTilemap;
    public Tilemap tempTilemap;
    private EntityManager entityManager;

    private void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        mainTilemap = GameObject.Find("MainTilemap").GetComponent<Tilemap>();
        tempTilemap = GameObject.Find("TempTilemap").GetComponent<Tilemap>();
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        UpdateConvoyerPosition(mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            PlaceConvoyer(mousePosition);
        }
    }

    private void UpdateConvoyerPosition(Vector3 position)
    {
        Vector3Int tilePosition = tempTilemap.WorldToCell(position);
        //tempTilemap.SetTile(tilePosition, ScriptableObject.CreateInstance<Tile>()); // Remplacer par votre sprite
        Debug.Log($"Grid Position: {tilePosition}");
    }

    private void PlaceConvoyer(Vector3 position)
    {
        Vector3Int tilePosition = mainTilemap.WorldToCell(position);

        //mainTilemap.SetTile(tilePosition, ScriptableObject.CreateInstance<Tile>());

        Entity convoyerEntity = entityManager.CreateEntity(typeof(TileConvoyerData));
    }
}