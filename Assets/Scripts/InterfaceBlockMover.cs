using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ColumnsInterfaces
{
    public interface InterfaceBlockMover
    {

        public void Init(Tilemap tilemap);
        public bool TeleportBlock(Vector3Int startPosition, Vector3Int endPosition);

        public bool TeleportBlocks(Vector3Int[] startPositions, Vector3Int[] endPositions);

        public bool MoveBlock(Vector3Int startPosition, Vector3Int direction, int distance);

        public bool MoveBlockDownwards(Vector3Int start, int distance);

        public bool MoveBlocks(Vector3Int[] startPositions, Vector3Int direction, int distance);
        public bool MoveBlocksDownwards(Vector3Int[] startPositions, int distance);
    }
}
