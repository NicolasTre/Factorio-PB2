using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TagRef
{
    Default,
    Convoyer
}

public class FC_ItemMoverOnConvoyer : MonoBehaviour
{
    public FC_TileConvoyerSystem convoyerSystem;
    
    
    public float durationTravelling = 1f;
    private float timer = 0f;


    private Vector3 targetPosition;

    private bool isMoving;
    [SerializeField] private TagRef convoyerTag = TagRef.Convoyer;

    private Vector3 offsetToPutItInTheMiddleCaseByTileMap;
    private Vector3Int currentTilePos;

    private CircleCollider2D collider;

    private void Start()
    {
        targetPosition = transform.position;
        isMoving = false;
        offsetToPutItInTheMiddleCaseByTileMap = new Vector3(0.5f, 0.5f, 0);
        
        collider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
/*        if (isMoving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) >= 0.01f)
            {
                return;
            }
            transform.position = targetPosition;
            isMoving = false;
        }
        else
        {
            Vector3Int currentTilePos = convoyerSystem.mainTilemap.WorldToCell(transform.position);
            DIRECTION currentDirection = convoyerSystem.GetCurrentDirectionAt(currentTilePos);
            Vector3Int nextTilePos = convoyerSystem.GetNeighborTilePosition(currentTilePos, currentDirection);

            if (!convoyerSystem.IsTileAvailable(nextTilePos))
            {
                return;
            }
            targetPosition = convoyerSystem.mainTilemap.CellToWorld(nextTilePos);
            isMoving = true;
        }*/

        if (isMoving)
        {
            MoveItem();
        }
        else
        {
            CheckIfOnConvoyer();
        }
    }


    private void CheckIfOnConvoyer()
    {
        Tilemap tilemap = convoyerSystem.convoyerTilemap;
        currentTilePos = tilemap.WorldToCell(transform.position);
        //Debug.Log("Position de la cellule touchée: " + currentTilePos);

        if (!convoyerSystem.IsTileAvailable(currentTilePos)) { return; }

        ChangeTargetPosition(tilemap.CellToWorld(currentTilePos));
        CenterImmadiateItemOnTarget();

        DIRECTION currentDirection = convoyerSystem.GetCurrentDirectionAt(currentTilePos);
        Vector3Int nextTilePos = convoyerSystem.GetNeighborTilePosition(currentTilePos, currentDirection);

        if (!convoyerSystem.IsTileAvailable(nextTilePos)) { return; }

        if (HasOppositeDirection(currentDirection, convoyerSystem.placedTiles[nextTilePos])) { return; }

        if (HasAnotherItemInTheNextTile(nextTilePos)) { return; }

        ChangeTargetPosition(tilemap.CellToWorld(nextTilePos));

        //Debug.Log("Movement Enable");
        timer = 0f;
        isMoving = true;
    }

    private void ChangeTargetPosition(Vector3 newTarget)
    {
        targetPosition = newTarget + offsetToPutItInTheMiddleCaseByTileMap;
    }

    private bool HasOppositeDirection(DIRECTION dir1, DIRECTION dir2)
    {
        if (!convoyerSystem.directionOffsets.TryGetValue(dir1, out Vector3Int vectorDir1))
        {
            return false;
        }

        if (!convoyerSystem.directionOffsets.TryGetValue(dir2, out Vector3Int vectorDir2))
        {
            return false;
        }

        return vectorDir1 == -vectorDir2;
        if (vectorDir1 != -vectorDir2)
        {
            return false;
        }

        //Debug.Log("Has Opposite Direction by the next tile");
        return true;
    }
    private bool HasAnotherItemInTheNextTile(Vector3Int nextTilePos) 
    {       
        FC_ItemData itemAtNextPosition = GetItemAtTile<FC_ItemData>(nextTilePos);

        if (itemAtNextPosition == null)
        {
            return false;
        }

        return true;
    }

    private T GetItemAtTile<T>(Vector3Int tilePos) where T : FC_ItemData
    {
        // Convertir la position de la tuile en coordonnées du monde pour la comparaison
        Vector3 worldPosition = convoyerSystem.convoyerTilemap.CellToWorld(tilePos) + new Vector3(0.5f, 0.5f, 0);
        
        FC_ItemData[] itemFound = FindObjectsOfType<FC_ItemData>();
        foreach (var item in itemFound)
        {
            // Vérifier si l'objet est du type T et si sa position correspond à celle de la tuile
            if (Vector3.Distance(item.transform.position, worldPosition) <= collider.radius)
            {
                return item as T;
            }
        }

        return null;
    }

    private void CenterImmadiateItemOnTarget()
    {
        transform.position = targetPosition;
    }

    private void MoveItem()
    {
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(currentTilePos + offsetToPutItInTheMiddleCaseByTileMap, targetPosition, timer / durationTravelling);

        if (Vector3.Distance(transform.position, targetPosition) >= 0.01f)
        {
            return;
        }
        isMoving = false;
        transform.position = targetPosition;
    }
}