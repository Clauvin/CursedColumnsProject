using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using ColumnsInterfaces;


public class BlockPlacer : MonoBehaviour, InterfaceBlockPlacer
{
    Tilemap tileMap;

    void Start()
    {
        tileMap = gameObject.GetComponentInChildren<Tilemap>();
    }

    public bool AddBlock(Vector3Int position, TileBase tile)
    {
        tileMap.SetTile(position, tile);
        return true;
    }

    public bool AddBlocks(Vector3Int[] positions, TileBase[] tiles)
    {
        if (positions.Length == 0)
        {
            Debug.LogError("At AddBlocks, positions vector is empty.");
            return false;
        }
        else if (tiles.Length == 0)
        {
            Debug.LogError("At AddBlocks, tiles vector is empty.");
            return false;
        }
        else if (positions.Length != tiles.Length)
        {
            Debug.LogError("At AddBlocks, positions vector does not have the same size as tiles vector: " + positions.Length + " != " + tiles.Length + ".");
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
