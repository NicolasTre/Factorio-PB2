using Unity.Entities;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConvoyerSimulation : MonoBehaviour
{
    public Tilemap mainTilemap;
    public Tilemap tempTilemap;
    public Tile[] tileAnimList_Test;

    private EntityManager entityManager;

    private Vector3Int lastTilePosition;


    private void Awake()
    {
        mainTilemap = GameObject.Find("MainTilemap").GetComponent<Tilemap>();
        tempTilemap = GameObject.Find("TempTilemap").GetComponent<Tilemap>();
    }

    private void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        UpdateConvoyerPosition(mousePosition, tileAnimList_Test);

        if (Input.GetMouseButtonDown(0))
        {
            PlaceConvoyer(mousePosition, tileAnimList_Test);
        }

        TileConvoyerData.animationFrame++;
        UpdateConvoyerAnimation();
    }

    private void UpdateConvoyerPosition(Vector3 position, Tile tileAnim)
    {
        Vector3Int tilePosition = tempTilemap.WorldToCell(position);

        if (tilePosition != lastTilePosition)
        {
            tempTilemap.SetTile(lastTilePosition, null);
            lastTilePosition = tilePosition;
        }

        tempTilemap.SetTile(tilePosition, tileAnim);

        Debug.Log($"Grid Position: {tilePosition}");
    }

    private void UpdateConvoyerPosition(Vector3 position, Tile[] tileAnim)
    {
        Vector3Int tilePosition = tempTilemap.WorldToCell(position);

        if (tilePosition != lastTilePosition)
        {
            tempTilemap.SetTile(lastTilePosition, null);
            lastTilePosition = tilePosition;
        }

        int frameIndex = TileConvoyerData.animationFrame % tileAnim.Length;
        tempTilemap.SetTile(tilePosition, tileAnim[frameIndex]);

        Debug.Log($"Grid Position: {tilePosition}");
    }


    private void PlaceConvoyer(Vector3 position, Tile tile)
    {
        Vector3Int tilePosition = mainTilemap.WorldToCell(position);

        Tile currentTile = mainTilemap.GetTile<Tile>(tilePosition);
        if (currentTile != null && currentTile == tile)
        {
            Debug.Log("Le même tile est déjà placé ici.");
            return;
        }

        mainTilemap.SetTile(tilePosition, tile);

        Entity convoyerEntity = entityManager.CreateEntity(typeof(TileConvoyerData));
    }

    private void PlaceConvoyer(Vector3 position, Tile[] tileAnim)
    {
        Vector3Int tilePosition = mainTilemap.WorldToCell(position);

        Tile currentTile = mainTilemap.GetTile<Tile>(tilePosition);
        if (currentTile != null)
        {
            foreach (Tile tile in tileAnim)
            {
                if (currentTile == tile)
                {
                    Debug.Log("L'un des tiles de l'animation est déjà placé ici.");
                    return;
                }
            }
        }

        if (tileAnim != null && tileAnim.Length > 0)
        {
            // Assure que l'index ne dépasse pas la taille du tableau
            int frameIndex = TileConvoyerData.animationFrame % tileAnim.Length;
            mainTilemap.SetTile(tilePosition, tileAnim[frameIndex]);
        }
        else
            Debug.LogWarning("Le tableau de tiles est vide ou nul.");

        Entity convoyerEntity = entityManager.CreateEntity(typeof(TileConvoyerData));
    }

    private void UpdateConvoyerAnimation()
    {
        if (tileAnimList_Test != null && tileAnimList_Test.Length > 0)
        {
            foreach (var position in mainTilemap.cellBounds.allPositionsWithin)
            {
                Tile currentTile = mainTilemap.GetTile<Tile>(position);
                if (currentTile != null)
                {
                    int frameIndex = TileConvoyerData.animationFrame % tileAnimList_Test.Length;
                    mainTilemap.SetTile(position, tileAnimList_Test[frameIndex]);
                }
            }
        }
    }
}