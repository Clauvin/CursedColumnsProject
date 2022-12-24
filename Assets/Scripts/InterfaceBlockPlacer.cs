using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ColumnsInterfaces
{
    public interface InterfaceBlockPlacer
    {
        public void Init(Tilemap tilemap);
        public string GetAddBlockTileErrorMessageString();

        public bool AddBlock(Vector3Int position, TileBase tile);

        public string GetAddBlocksZeroPositionsErrorMessage();

        public string GetAddBlocksZeroTilesErrorMessage();
        
        public string GetAddBlocksPositionsAmountDifferentFromTilesErrorMessage();

        public bool AddBlocks(Vector3Int[] positions, TileBase[] tiles);

        public string GetAddSameBlockMultiplesTimesZeroPositionsErrorMessage();

        public string GetAddSameBlockMultiplesTimesOnePositionWarningMessage();

        public bool AddSameBlockMultiplesTimes(Vector3Int[] positions, TileBase tile);
    }
}


