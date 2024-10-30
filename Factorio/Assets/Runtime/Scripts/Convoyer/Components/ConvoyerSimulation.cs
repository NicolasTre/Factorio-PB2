using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ConvoyerSimulation : MonoBehaviour
{
    private Tilemap mainTilemap;
    private Tilemap tempTilemap;

    private Vector3Int lastTilePosition;
    
    [SerializeField] private float animationSpeed = 1f;
    private float timeAccumulator = 0f;

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

    private Dictionary<DIRECTION, Tile[]> tilesByDirection;
    private Dictionary<Vector3Int, DIRECTION> placedTiles           = new();
    private Dictionary<DIRECTION, int> animationFramesByDirection   = new();

    [SerializeField] private DIRECTION currentDirection = DIRECTION.Up;

    private void Awake()
    {
        mainTilemap = GameObject.Find("MainTilemap").GetComponent<Tilemap>();
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

    private void CheckButtonButtonsPressed()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ChangeDirection();
            Debug.Log($"Direction changée en: {currentDirection}");
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
        Vector3Int tilePosition = tempTilemap.WorldToCell(position);

        ReplaceLastTile(tilePosition);

        if (!tilesByDirection.ContainsKey(currentDirection))
        {
            return;
        }

        Tile currentTile = tilesByDirection[currentDirection][animationFramesByDirection[currentDirection]];
        tempTilemap.SetTile(tilePosition, currentTile);
        //Debug.Log($"Grid Position: {tilePosition}");
    }

    private void ReplaceLastTile(Vector3Int tilePosition)
    {
        if (tilePosition == lastTilePosition)
        {
            return;
        }

        tempTilemap.SetTile(lastTilePosition, null);
        lastTilePosition = tilePosition;
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


    private void PlaceConvoyer(Vector3 position, Tile tile)
    {
        if (!tilesByDirection.ContainsKey(currentDirection))
        {
            Debug.LogWarning("Aucune animation trouvée pour cette direction.");
        }

        Vector3Int tilePosition = mainTilemap.WorldToCell(position);
        Tile currentTile = mainTilemap.GetTile<Tile>(tilePosition);

        if (currentTile != null && currentTile == tile)
        {
            Debug.Log("Le même tile est déjà placé ici.");
            return;
        }

        mainTilemap.SetTile(tilePosition, tile);
        placedTiles[tilePosition] = currentDirection;
    }

    private void PlaceConvoyer(Vector3 position, Tile[] tileAnim)
    {        
        Tile currentTile = tilesByDirection[currentDirection][animationFramesByDirection[currentDirection]];
        PlaceConvoyer(position, currentTile);

    }

    private void UpdateConvoyerAnimation()
    {
        if (!tilesByDirection.TryGetValue(currentDirection, out Tile[] tiles))
        {
            return;
        }

        foreach (var position in placedTiles.Keys)
        {
            DIRECTION placedDirection = placedTiles[position];
            Tile[] tileArray = tilesByDirection[placedDirection];

            if (tileArray.Length > 0)
            {
                int frameIndex = animationFramesByDirection[placedDirection] % tileArray.Length;
                mainTilemap.SetTile(position, tileArray[frameIndex]);
            }
        }
    }
}