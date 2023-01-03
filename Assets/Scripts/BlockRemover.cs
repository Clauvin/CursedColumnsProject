using ColumnsInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#region UpdateLog
// v1.3 - Create error functions for tests
// v1.2 - Created error messages
// v1.1 - Added Init function
#endregion UpdateLog
public class BlockRemover : MonoBehaviour, InterfaceBlockRemover
{
    Tilemap tileMap;

    public const string eraseBlockNullErrorMessage = "At EraseBlock - position is null.";
    public const string eraseBlocksNoPositionsErrorMessage = "At EraseBlocks - positions array have zero positions.";
    public const string removeBlockNullErrorMessage = "At RemoveBlock - position is null.";
    public const string removeBlocksNullErrorMessage = "At RemoveBlocks - positions array have zero positions.";
    public const string removeBlocksNoPositionsErrorMessage = "At RemoveBlocks - positions array have zero positions.";

    void Start()
    {
        Init(gameObject.GetComponentInChildren<Tilemap>());
    }

    public void Init(Tilemap tilemap)
    {
        this.tileMap = tilemap;
    }

    public bool EraseBlock(Vector3Int position)
    {
        if (position == null)
        {
            Debug.LogError(eraseBlockNullErrorMessage);
            return false;
        }

        tileMap.SetTile(position, null);
        return true;
    }

    public bool EraseBlocks(Vector3Int[] positions)
    {
        if (positions.Length == 0)
        {
            Debug.LogError(eraseBlocksNoPositionsErrorMessage);
            return false;
        }

        for (int i = 0; i < positions.Length; i++)
        {
            tileMap.SetTile(positions[i], null);
        }
        return true;
    }

    public bool RemoveBlock(Vector3Int position)
    {
        if (tileMap.GetTile(position) == null)
        {
            Debug.LogError(removeBlockNullErrorMessage);
            return false;
        }

        tileMap.SetTile(position, null);
        return true;
    }

    public bool RemoveBlocks(Vector3Int[] positions)
    {
        if (positions.Length == 0)
        {
            Debug.LogError(removeBlocksNoPositionsErrorMessage);
            return false;
        }

        for (int i = 0; i < positions.Length; i++)
        {
            if (tileMap.GetTile(positions[i]) == null)
            {
                Debug.LogError(removeBlocksNullErrorMessage);
                return false;
            }
            tileMap.SetTile(positions[i], null);
        }
        return true;
    }

    #region Functions For Tests

    public string GetEraseBlockNullErrorMessage()
    {
        return eraseBlockNullErrorMessage;
    }

    public string GetEraseBlocksNoPositionsErrorMessage()
    {
        return eraseBlocksNoPositionsErrorMessage;
    }

    public string GetRemoveBlockNullErrorMessage()
    {
        return removeBlockNullErrorMessage;
    }

    public string GetRemoveBlocksNullErrorMessage()
    {
        return removeBlocksNullErrorMessage;
    }

    public string GetRemoveBlocksNoPositionsErrorMessage()
    {
        return removeBlocksNoPositionsErrorMessage;
    }

    #endregion Functions For Tests
}
