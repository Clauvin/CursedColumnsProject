using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public interface InterfaceBlockPlacer
{
    public bool AddBlock(Vector3Int position, TileBase tile);

    public bool AddBlocks(Vector3Int[] positions, TileBase[] tiles);


}
