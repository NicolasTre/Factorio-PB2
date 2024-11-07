using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FC_BuildingSystem : MonoBehaviour
{
   /* [SerializeField] private FC_ItemV1 _item;

    [SerializeField] private TileBase _highlightTile;
    [SerializeField] private Tilemap  _mainTilemap;
    [SerializeField] private Tilemap  _tempTilemap;

    private Vector3Int  _playerPosition;
    private Vector3Int  _highlightedTilePos;
    private bool        _isHighlighted;

    private void Update()
    {
        _playerPosition = _mainTilemap.WorldToCell(transform.position);

        if (_item != null)
        {
            HighlightTile(_item);
        }
    }

    private Vector3Int GetMouseOnGridPos()
    {
        return _mainTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private void HighlightTile(FC_Item currentItem)
    {
        Vector3Int mouseOnGridPos = GetMouseOnGridPos();
        
        _tempTilemap.SetTile(_highlightedTilePos, null);

        TileBase tile = _mainTilemap.GetTile(mouseOnGridPos);

        if (InRange(_playerPosition, mouseOnGridPos, (Vector3Int)currentItem.range))
        {
            if (tile)
            {
                _tempTilemap.SetTile(mouseOnGridPos, _highlightTile);
                _highlightedTilePos = mouseOnGridPos;

                _isHighlighted = true;
            }
            else 
                _isHighlighted = false;
        }
    }

    private bool InRange(Vector3Int PositionA, Vector3Int PositionB, Vector3Int Range)
    {
        Debug.Log($"In Range Result : {Mathf.Abs(PositionA.x - PositionB.x) <= Range.x || Mathf.Abs(PositionA.y - PositionB.y) <= Range.y}");

        return Mathf.Abs(PositionA.x - PositionB.x) <= Range.x || Mathf.Abs(PositionA.y - PositionB.y) <= Range.y;
    }

    private bool CheckCondition(FC_RuleTileWithData tile, FC_Item currentItem)
    {
        if (currentItem.itemType == FC_ItemType.BuildingBlock)
            if (!tile)
                return false;
            else if (currentItem.itemType == FC_ItemType.Tool)
                if (tile)
                    if (tile.item.actionType == currentItem.actionType)
                        return true;
        return false;
    }*/
}