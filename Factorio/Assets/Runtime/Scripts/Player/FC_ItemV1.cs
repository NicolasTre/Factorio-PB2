using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "GameObject/Item")]
public class FC_ItemV1 : ScriptableObject
{
    public TileBase      tile;
    public Sprite        spriteTile;
    public FC_ItemType   itemType;
    public FC_ActionType actionType;
    public Vector2Int    range = new(5, 4);
}

public enum FC_ItemType
{
    BuildingBlock,
    Tool
}

public enum FC_ActionType
{
    Dig, // Creuser
    Mine
}