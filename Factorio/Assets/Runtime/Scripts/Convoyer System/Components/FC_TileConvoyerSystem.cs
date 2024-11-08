using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class FC_TileConvoyerSystem : MonoBehaviour
{

    #region Tile Animations
    [Header("Natural Animations")]
    [SerializeField] private Tile[] tileAnimUp;
    [SerializeField] private Tile[] tileAnimDown;
    [SerializeField] private Tile[] tileAnimLeft;
    [SerializeField] private Tile[] tileAnimRight;

    [Header("- Up Animations")]
    [SerializeField] private Tile[] tileAnimUpRight;
    [SerializeField] private Tile[] tileAnimUpLeft;

    [Header("- Down Animations")]
    [SerializeField] private Tile[] tileAnimDownRight;
    [SerializeField] private Tile[] tileAnimDownLeft;
    
    [Header("- Right Animations")]
    [SerializeField] private Tile[] tileAnimRightUp;
    [SerializeField] private Tile[] tileAnimRightDown;

    [Header("- Left Animations")]
    [SerializeField] private Tile[] tileAnimLeftUp;
    [SerializeField] private Tile[] tileAnimLeftDown;
    #endregion

    #region Parameters
    [Space(30)]
    [Header("Parameters")]

    [SerializeField] private float animationSpeed = 1f;
    public DIRECTION currentDirection { get; private set; }
    [SerializeField] private Color highlightColor = Color.green;
    
    public Tilemap convoyerTilemap { get; private set; }
    public Tilemap tempTilemap { get; private set; }

    private Vector3Int lastTilePosition;
    
    private float timeAccumulator = 0f;
    public Dictionary<DIRECTION, Tile[]> tilesByDirection { get; private set; }
    public Dictionary<Vector3Int, DIRECTION> placedTiles { get; private set; }
    public  Dictionary<DIRECTION, int> animationFramesByDirection   = new();
    public Dictionary<DIRECTION, Vector3Int> directionOffsets { get; private set; }
    #endregion

    private void Awake()
    {
        convoyerTilemap = GameObject.Find("ConvoyerTilemap").GetComponent<Tilemap>();
        tempTilemap = GameObject.Find("TempTilemap").GetComponent<Tilemap>();
        
        tilesByDirection = new Dictionary<DIRECTION, Tile[]>
        {
            { DIRECTION.Up, tileAnimUp },
            { DIRECTION.Down, tileAnimDown },
            { DIRECTION.Left, tileAnimLeft },
            { DIRECTION.Right, tileAnimRight },

            { DIRECTION.UpRight, tileAnimUpRight },
            { DIRECTION.UpLeft, tileAnimUpLeft },

            { DIRECTION.DownRight, tileAnimDownRight },
            { DIRECTION.DownLeft, tileAnimDownLeft },

            { DIRECTION.RightUp, tileAnimRightUp },
            { DIRECTION.RightDown, tileAnimRightDown },

            { DIRECTION.LeftUp, tileAnimLeftUp },
            { DIRECTION.LeftDown, tileAnimLeftDown }
        };
        
        placedTiles = new();
        
        directionOffsets = new Dictionary<DIRECTION, Vector3Int>
        {
            { DIRECTION.Up, Vector3Int.up },
            { DIRECTION.Down, Vector3Int.down },
            { DIRECTION.Left, Vector3Int.left },
            { DIRECTION.Right, Vector3Int.right },

            { DIRECTION.UpRight, Vector3Int.right },
            { DIRECTION.UpLeft, Vector3Int.left },

            { DIRECTION.DownRight, Vector3Int.right },
            { DIRECTION.DownLeft, Vector3Int.left },

            { DIRECTION.RightUp, Vector3Int.up },
            { DIRECTION.RightDown, Vector3Int.down },

            { DIRECTION.LeftUp, Vector3Int.up },
            { DIRECTION.LeftDown, Vector3Int.down }
        };


        currentDirection = DIRECTION.Right;

        foreach (var direction in tilesByDirection.Keys)
        {
            animationFramesByDirection[direction] = 0;
        }
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        
        UpdateConvoyerPosition(mousePosition, tilesByDirection[currentDirection][animationFramesByDirection[currentDirection]]);


        if (Input.GetMouseButtonDown(0))
        {
            PlaceConvoyer(mousePosition, tilesByDirection[currentDirection][animationFramesByDirection[currentDirection]]);
        }

        UpdateFrameAnimation();
        UpdateConvoyerAnimation();

        CheckButtonButtonsPressed();
    }

    #region Actions
    private void CheckButtonButtonsPressed()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeDirection();
            //Debug.Log($"Direction changée en: {currentDirection}");
        }
    }

    private void ChangeDirection()
    {
        currentDirection = (DIRECTION)(((int)currentDirection + 1) % System.Enum.GetValues(typeof(DIRECTION)).Length);
       
        if (tilesByDirection[currentDirection].Length == 0)
        {
            ChangeDirection();
        }
    }

    public void CreateTile(DIRECTION direction, Vector3 position)
    {
        Vector3Int tilePosition = convoyerTilemap.WorldToCell(position);
        
        if (convoyerTilemap.GetTile(tilePosition) != null)
        {
            Debug.LogWarning("Une tuile est déjà placée à cette position.");
            return;
        }

        Tile[] tileArray = tilesByDirection[direction];
        if (tileArray.Length == 0)
        {
            Debug.LogWarning("Aucune animation de tuile disponible pour cette direction.");
            return;
        }

        // Sélectionne la tuile d'animation actuelle pour la direction
        Tile currentTile = tileArray[animationFramesByDirection[direction] % tileArray.Length];

        convoyerTilemap.SetTile(tilePosition, currentTile);

        placedTiles[tilePosition] = direction;

        Debug.Log($"Tuile créée à la position {tilePosition} avec la direction {direction}");
    }
    #endregion

    #region Getter
    public Vector3Int GetNeighborTilePosition(Vector3Int currentTilePosition, DIRECTION direction)
    {
        if (directionOffsets.TryGetValue(direction, out Vector3Int offset))
        {
            return currentTilePosition + offset;
        }
        return currentTilePosition;
    }

    public DIRECTION GetCurrentDirectionAt(Vector3Int tilePosition)
    {
        if (placedTiles.TryGetValue(tilePosition, out DIRECTION direction))
        {
            return direction;
        }
        
        return DIRECTION.Right;
    }

    public bool IsTileAvailable(Vector3Int tilePosition)
    {
        Tile tile = convoyerTilemap.GetTile<Tile>(tilePosition);
        
        return tile != null;

//        return tile == null;
    }
    #endregion

    #region Update
    private void UpdateFrameAnimation()
    {
        timeAccumulator += Time.deltaTime;

        if (timeAccumulator < 1f / animationSpeed)
        {
            return;
        }

        foreach (var direction in tilesByDirection.Keys)
        {
            if (tilesByDirection[direction].Length > 0)
            {
                int frameCount = tilesByDirection[direction].Length;
                animationFramesByDirection[direction] = (animationFramesByDirection[direction] + 1) % frameCount;
            }
        }

        timeAccumulator = 0f;
    }

    private void UpdateConvoyerPosition(Vector3 position, Tile tile)
    {
        if (!tilesByDirection.ContainsKey(currentDirection))
        {
            return;
        }

        Vector3Int tilePosition = tempTilemap.WorldToCell(position);
        
        ///
        Vector3Int neighborTilePosition = GetNeighborTilePosition(tilePosition, currentDirection);
        //Debug.Log($"Position de la tuile voisine: {neighborTilePosition}");
        ///

        HighlightTile(tilePosition);
        ReplaceLastTile(tilePosition);

        Tile currentTile = tilesByDirection[currentDirection][animationFramesByDirection[currentDirection]];
        tempTilemap.SetTile(tilePosition, currentTile);
        //Debug.Log($"Grid Position: {tilePosition}");
    }

    private void UpdateConvoyerPosition(Vector3 position, Tile[] tileAnim)
    {
        if (!tilesByDirection.ContainsKey(currentDirection))
        {
            ChangeDirection();
            return;
        }

        UpdateConvoyerPosition(position, tileAnim[animationFramesByDirection[currentDirection]]);
    }

    private void UpdateConvoyerAnimation()
    {
        foreach (var position in placedTiles.Keys)
        {
            DIRECTION placedDirection = placedTiles[position];
            Tile[] tileArray = tilesByDirection[placedDirection];

            if (tileArray.Length > 0)
            {
                int frameIndex = animationFramesByDirection[placedDirection] % tileArray.Length;
                convoyerTilemap.SetTile(position, tileArray[frameIndex]);
            }
        }
    }
    #endregion
    
    #region Placing
    private void ReplaceLastTile(Vector3Int tilePosition)
    {
        if (tilePosition == lastTilePosition)
        {
            return;
        }

        //Debug.Log($"Grid LastPosition: {lastTilePosition}");
        tempTilemap.SetTile(lastTilePosition, null);
        lastTilePosition = tilePosition;
    }

    private void PlaceConvoyer(Vector3 position, Tile tile)
    {

        Vector3Int tilePosition = convoyerTilemap.WorldToCell(position);
        Tile currentTile = convoyerTilemap.GetTile<Tile>(tilePosition);

        if (currentTile != null && currentTile == tile)
        {
            Debug.Log("Le même tile est déjà placé ici.");
            return;
        }

        ResetTileColor(tilePosition);
        convoyerTilemap.SetTile(tilePosition, tile);
        
        placedTiles[tilePosition] = currentDirection;
    }

    private void PlaceConvoyer(Vector3 position, Tile[] tileAnim)
    {        
        Tile currentTile = tilesByDirection[currentDirection][animationFramesByDirection[currentDirection]];
        PlaceConvoyer(position, currentTile);

    }
    #endregion

    #region Colorimetrie
    private void HighlightTile(Vector3Int position)
    {
        tempTilemap.SetTileFlags(position, TileFlags.None); // Permet la modification de la couleur
        tempTilemap.SetColor(position, highlightColor);
    }

    private void ResetTileColor(Vector3Int position)
    {
        tempTilemap.SetColor(position, Color.white); //couleur d'origine
    }
    #endregion
}