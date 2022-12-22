using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ColumnsInterfaces
{
    public interface InterfaceBlockRemover
    {
        public bool EraseBlock(Vector3Int position);

        public bool RemoveBlock(Vector3Int position);

        public bool EraseBlocks(Vector3Int[] positions);

        public bool RemoveBlocks(Vector3Int[] positions);
    }
}