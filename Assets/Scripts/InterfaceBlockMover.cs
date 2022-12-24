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

        public bool MoveBlock(Vector3Int startPosition, Vector3Int direction, int distance);

        public bool MoveBlockDownwards(Vector3Int start, int distance);
    }
}
