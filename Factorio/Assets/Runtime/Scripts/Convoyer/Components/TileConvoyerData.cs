using UnityEngine;
using UnityEngine.Tilemaps;

public struct TileConvoyerData
{
    public static int animationFrame = 0;
    
    public DIRECTION direction;
    public Vector3Int position;
    
    public Tile[] tileAnimList;
}

public enum DIRECTION
{
    Up,
    Down,
    Left,
    Right,

    UpRight,
    UpLeft,

    DownRight,
    DownLeft,

    RightUp,
    RightDown,
    
    LeftUp,
    LeftDown
}



/*public struct TileConvoyerData : IComponentData
{
    public float3 position;                // Position de la tile
    public float3 direction;               // Direction du convoyeur
    public int animationFrame;             // Frame d'animation actuelle
    public float lerpSpeed;                // Vitesse du déplacement entre les tiles
    public Sprite[] spriteVariants;        // Différentes sprites pour l'animation et les directions
    public int inventory;                  // Inventaire de la tile
    public static int animationFrameGlobal; // Animation globale pour synchronisation

    public bool HasAdjacentConvoyer;       // S'il y a un autre convoyeur adjacent
}*/