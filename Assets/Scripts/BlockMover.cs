using ColumnsInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockMover : MonoBehaviour, InterfaceBlockMover
{

    Tilemap tileMap;
    InterfaceBlockPlacer interfaceBlockPlacer;
    InterfaceBlockRemover interfaceBlockRemover;

    // Start is called before the first frame update
    void Start()
    {
        tileMap = gameObject.GetComponentInChildren<Tilemap>();
        interfaceBlockPlacer = new BlockPlacer();
        interfaceBlockPlacer.Init(tileMap);

        interfaceBlockRemover = new BlockRemover();
        interfaceBlockRemover.Init(tileMap);
    }

    public void Init(Tilemap tilemap)
    {
        this.tileMap = tilemap;
        interfaceBlockPlacer.Init(tileMap);
        interfaceBlockRemover.Init(tileMap);
    }

    public bool TeleportBlock(Vector3Int startPosition, Vector3Int endPosition)
    {
        if (tileMap.GetTile(startPosition) == null)
        {
            Debug.LogError("TeleportBlock - startPosition does not have a block.");
            return false;
        }

        if (tileMap.GetTile(endPosition) != null)
        {
            Debug.LogError("TeleportBlock - endPosition already has a block.");
            return false;
        }

        TileBase block = tileMap.GetTile(startPosition);
        interfaceBlockRemover.RemoveBlock(startPosition);
        interfaceBlockPlacer.AddBlock(endPosition, block);

        return true;
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
