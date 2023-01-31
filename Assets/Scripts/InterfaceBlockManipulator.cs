using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ColumnsInterfaces
{
    public interface InterfaceBlockManipulator
    {

        public void Init(Tilemap tilemap);

        public InterfaceBlockPlacer GetBlockPlacer();

        public InterfaceBlockRemover GetBlockRemover();

        public bool TeleportBlock(Vector3Int startPosition, Vector3Int endPosition);

        public bool TeleportBlocks(Vector3Int[] startPositions, Vector3Int[] endPositions);

        public bool MoveBlock(Vector3Int startPosition, Vector3Int direction, int distance);

        public bool MoveBlockDownwards(Vector3Int start, int distance);

        public bool MoveBlocks(Vector3Int[] startPositions, Vector3Int direction, int distance);
        public bool MoveBlocksDownwards(Vector3Int[] startPositions, int distance);

        public string GetTeleportBlockNullStartPositionErrorMessage();

        public string GetTeleportBlockNullEndPositionErrorMessage();

        public string GetTeleportBlocksStartPositionsNotSizedAsEndPositionsErrorMessage();

        public string GetTeleportBlocksSingularFunctionShouldBeUsedInsteadErrorMessage();

        public string GetTeleportBlocksStartPositionsErrorMessage();

        public string GetTeleportBlocksEndPositionsErrorMessage();

        public string GetTeleportBlocksStartPositionsNullCellErrorMessageFirst();

        public string GetTeleportBlocksStartPositionsNullCellErrorMessageSecond();

        public string GetMoveBlockNullStartPositionErrorMessage();

        public string GetMoveBlockNullDirectionErrorMessage();

        public string GetMoveBlockDistanceEqualsZeroErrorMessage();

        public string GetMoveBlockFoundObstacleMessageFirstPart();

        public string GetMoveBlockFoundObstacleMessageSecondPart();

        public string GetMoveBlocksStartPositionsEqualsZeroErrorMessage();
        public string GetMoveBlocksNullDirectionErrorMessage();

        public string GetMoveBlocksZeroDistanceErrorMessageFirstPart();

        public string GetMoveBlocksNoBlockErrorMessageFirstPart();

        public string GetMoveBlocksNoBlockErrorMessageSecondPart();
    }
}
