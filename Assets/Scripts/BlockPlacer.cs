using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ColumnsInterfaces;


public class BlockPlacer : MonoBehaviour, InterfaceBlockPlacer
{
    Tilemap tileMap;
    private static string addBlockErrorMessage = "At AddBlock, tile is null.";
    private static string addBlocksZeroPositionsErrorMessage = "At AddBlocks, positions vector is empty.";
    private static string addBlocksZeroTilesErrorMessage = "At AddBlocks, tiles vector is empty.";
    private static string addBlocksPositionsAmountDifferentFromTilesErrorMessage = "At AddBlocks, positions vector does not have the same size as tiles vector.";

    void Start()
    {
        tileMap = gameObject.GetComponentInChildren<Tilemap>();
    }

    public string GetAddBlockErrorMessageString()
    {
        return addBlockErrorMessage;
    }

    public bool AddBlock(Vector3Int position, TileBase tile)
    {
        if (tile == null)
        {
            Debug.LogError(GetAddBlockErrorMessageString());
            return false;
        }

        tileMap.SetTile(position, tile);
        return true;
    }

    public string GetAddBlocksZeroPositionsErrorMessage()
    {
        return addBlocksZeroPositionsErrorMessage;
    }

    public string GetAddBlocksZeroTilesErrorMessage()
    {
        return addBlocksZeroTilesErrorMessage;
    }

    public string GetAddBlocksPositionsAmountDifferentFromTilesErrorMessage()
    {
        return addBlocksPositionsAmountDifferentFromTilesErrorMessage;
    }

    public bool AddBlocks(Vector3Int[] positions, TileBase[] tiles)
    {
        if (positions.Length == 0)
        {
            Debug.LogError(GetAddBlocksZeroPositionsErrorMessage());
            return false;
        }
        else if (tiles.Length == 0)
        {
            Debug.LogError(GetAddBlocksZeroTilesErrorMessage());
            return false;
        }
        else if (positions.Length != tiles.Length)
        {
            Debug.LogError(GetAddBlocksPositionsAmountDifferentFromTilesErrorMessage());
            return false;
        }

        int amountOfBlocksToAdd = positions.Length;

        for (int i = 0; i < amountOfBlocksToAdd; i++)
        {
            AddBlock(positions[i], tiles[i]);
        }

        return true;
    }

    public bool AddSameBlockMultiplesTimes(Vector3Int[] positions, TileBase tile)
    {
        if (positions.Length == 0)
        {
            Debug.LogError("At AddSameBlockMultiplesTimes, positions vector is empty.");
            return false;
        }
        else if (positions.Length == 1)
        {
            Debug.LogWarning("At AddSameBlockMultiplesTimes, positions vector is equal one, on this case, this is not the best function to call");
        }

        int amountOfBlocksToAdd = positions.Length;

        for (int i = 0; i < amountOfBlocksToAdd; i++)
        {
            AddBlock(positions[i], tile);
        }

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
