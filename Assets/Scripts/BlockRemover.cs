using ColumnsInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockRemover : MonoBehaviour, InterfaceBlockRemover
{
    Tilemap tileMap;

    void Start()
    {
        tileMap = gameObject.GetComponentInChildren<Tilemap>();
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
            if (positions[i] == null)
            {
                return false;
            }
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
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
