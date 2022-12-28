using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ColumnsInterfaces
{
    public interface InterfaceBlockRemover
    {

        public void Init(Tilemap tilemap);
        public bool EraseBlock(Vector3Int position);

        public bool RemoveBlock(Vector3Int position);

        public bool EraseBlocks(Vector3Int[] positions);

        public bool RemoveBlocks(Vector3Int[] positions);

        public string GetEraseBlockNullErrorMessage();

        public string GetEraseBlocksNoPositionsErrorMessage();

        public string GetRemoveBlockNullErrorMessage();

        public string GetRemoveBlocksNullErrorMessage();

        public string GetRemoveBlocksNoPositionsErrorMessage();
    }
}