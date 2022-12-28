using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ColumnsInterfaces;

#region UpdateLog
//v1.2 - Removed error message functions and made error string consts
//v1.1 - Added Init function
#endregion UpdateLog

public class BlockPlacer : MonoBehaviour, InterfaceBlockPlacer
{
    Tilemap tileMap;

    private const string addBlockPositionErrorMessage = "At AddBlock, position is null.";
    private const string addBlockTileErrorMessage = "At AddBlock, tile is null.";
    private const string addBlocksZeroPositionsErrorMessage = "At AddBlocks, positions vector is empty.";
    private const string addBlocksZeroTilesErrorMessage = "At AddBlocks, tiles vector is empty.";
    private const string addBlocksPositionsAmountDifferentFromTilesErrorMessage = "At AddBlocks, positions vector does not have the same size as tiles vector.";
    private const string addSameBlockMultiplesTimesZeroPositionsErrorMessage = "At AddSameBlockMultiplesTimes, positions vector is empty.";
    private const string addSameBlockMultiplesTimesOnePositionWarningMessage = "At AddSameBlockMultiplesTimes, positions vector is equal one, on this case, this is not the best function to call.";

    void Start()
    {
        Init(gameObject.GetComponentInChildren<Tilemap>());
    }

    public void Init(Tilemap tilemap)
    {
        this.tileMap = tilemap;
    }

    public bool AddBlock(Vector3Int position, TileBase tile)
    {
        //position can't be nullable, but just in case
        if (position == null)
        {
            Debug.LogError(addBlockPositionErrorMessage);
            return false;
        }
        if (tile == null)
        {
            Debug.LogError(addBlockTileErrorMessage);
            return false;
        }

        tileMap.SetTile(position, tile);
        return true;
    }

    public bool AddBlocks(Vector3Int[] positions, TileBase[] tiles)
    {
        if (positions.Length == 0)
        {
            Debug.LogError(addBlocksZeroPositionsErrorMessage);
            return false;
        }
        else if (tiles.Length == 0)
        {
            Debug.LogError(addBlocksZeroTilesErrorMessage);
            return false;
        }
        else if (positions.Length != tiles.Length)
        {
            Debug.LogError(addBlocksPositionsAmountDifferentFromTilesErrorMessage);
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
            Debug.LogError(addSameBlockMultiplesTimesZeroPositionsErrorMessage);
            return false;
        }
        else if (positions.Length == 1)
        {
            Debug.LogWarning(addSameBlockMultiplesTimesOnePositionWarningMessage);
        }

        int amountOfBlocksToAdd = positions.Length;

        for (int i = 0; i < amountOfBlocksToAdd; i++)
        {
            AddBlock(positions[i], tile);
        }

        return true;
    }
}
