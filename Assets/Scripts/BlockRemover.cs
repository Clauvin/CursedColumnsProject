using ColumnsInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


// BlockRemover, v1.1 - Added Init function
public class BlockRemover : MonoBehaviour, InterfaceBlockRemover
{
    Tilemap tileMap;

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
            return false;
        }

        tileMap.SetTile(position, null);
        return true;
    }

    public bool EraseBlocks(Vector3Int[] positions)
    {
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
            return false;
        }

        tileMap.SetTile(position, null);
        return true;
    }

    public bool RemoveBlocks(Vector3Int[] positions)
    {
        for (int i = 0; i < positions.Length; i++)
        {
            if (tileMap.GetTile(positions[i]) == null)
            {
                return false;
            }
            tileMap.SetTile(positions[i], null);
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
