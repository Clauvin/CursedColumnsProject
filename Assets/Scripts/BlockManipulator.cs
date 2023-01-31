using ColumnsInterfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

#region UpdateLog
//v1.1 - Added error strings
//v1.2 - BlockMover now is called BlockManipulator
#endregion UpdateLog

public class BlockManipulator : MonoBehaviour, InterfaceBlockManipulator
{

    Tilemap tileMap;
    InterfaceBlockPlacer interfaceBlockPlacer;
    InterfaceBlockRemover interfaceBlockRemover;

    #region Strings for Error Messages
    private const string teleportBlockNullStartPositionErrorMessage = "TeleportBlock - startPosition does not have a block.";
    private const string teleportBlockNullEndPositionErrorMessage = "TeleportBlock - endPosition already has a block.";
    private const string teleportBlocksStartPositionsNotSizedAsEndPositionsErrorMessage = "TeleportsBlocks - startPositions and endPositions have different sizes.";
    private const string teleportBlocksSingularFunctionShouldBeUsedInsteadErrorMessage = "TeleportBlocks - TeleportBlock should be used instead.";
    private const string teleportBlocksStartPositionsErrorMessage = "TeleportBlocks - startPositions does not have positions.";
    private const string teleportBlocksEndPositionsErrorMessage = "TeleportBlocks - endPositions does not have positions.";
    private const string teleportBlocksStartPositionsNullCellErrorMessageFirst = "TeleportBlocks - startPositions[";
    private const string teleportBlocksStartPositionsNullCellErrorMessageSecond = "] does not have a block.";

    private const string moveBlockNullStartPositionErrorMessage = "MoveBlock - startPosition does not have a block.";
    private const string moveBlockNullDirectionErrorMessage = "MoveBlock - the direction points to nowhere.";
    private const string moveBlockDistanceEqualsZeroErrorMessage = "MoveBlock - Distance == 0, it should be different of zero.";
    private const string moveBlockFoundObstacleMessageFirstPart = "MoveBlock - found obstacle at ";
    private const string moveBlockFoundObstacleMessageSecondPart = ", stopping movement.";

    private const string moveBlocksStartPositionsEqualsZeroErrorMessage = "MoveBlocks - startPositions does not have positions.";
    private const string moveBlocksNullDirectionErrorMessage = "MoveBlocks - the direction points to nowhere.";
    private const string moveBlocksZeroDistanceErrorMessageFirstPart = "MoveBlocks - Distance == 0, it should be different of zero.";
    private const string moveBlocksNoBlockErrorMessageFirstPart = "MoveBlocks - startPositions[";
    private const string moveBlocksNoBlockErrorMessageSecondPart = "] does not have a block.";
    #endregion Strings for Error Messages


    // Start is called before the first frame update
    void Start()
    {
        tileMap = gameObject.GetComponentInChildren<Tilemap>();
        interfaceBlockPlacer = new BlockPlacer();
        interfaceBlockPlacer.Init(tileMap);

        interfaceBlockRemover = new BlockRemover();
        interfaceBlockRemover.Init(tileMap);
    }

    public void StartManager()
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

    public InterfaceBlockPlacer GetBlockPlacer()
    {
        return interfaceBlockPlacer;
    }

    public InterfaceBlockRemover GetBlockRemover()
    {
        return interfaceBlockRemover;
    }

    public bool TeleportBlock(Vector3Int startPosition, Vector3Int endPosition)
    {
        if (tileMap.GetTile(startPosition) == null)
        {
            Debug.LogError(teleportBlockNullStartPositionErrorMessage);
            return false;
        }

        if (tileMap.GetTile(endPosition) != null)
        {
            Debug.LogError(teleportBlockNullEndPositionErrorMessage);
            return false;
        }

        TileBase block = tileMap.GetTile(startPosition);
        interfaceBlockRemover.RemoveBlock(startPosition);
        interfaceBlockPlacer.AddBlock(endPosition, block);

        return true;
    }

    public bool TeleportBlocks(Vector3Int[] startPositions, Vector3Int[] endPositions)
    {
        #region ErrorChecks
        if (startPositions.Length != endPositions.Length)
        {
            Debug.LogError(teleportBlocksStartPositionsNotSizedAsEndPositionsErrorMessage);
            return false;
        }

        if (startPositions.Length == 1 && endPositions.Length == 1)
        {
            Debug.LogWarning(teleportBlocksSingularFunctionShouldBeUsedInsteadErrorMessage);
        }

        if (startPositions.Length == 0)
        {
            Debug.LogError(teleportBlocksStartPositionsErrorMessage);
            return false;
        }

        if (endPositions.Length == 0)
        {
            Debug.LogError(teleportBlocksEndPositionsErrorMessage);
            return false;
        }

        for (int i = 0; i < startPositions.Length; i++)
        {
            if (tileMap.GetTile(startPositions[i]) == null)
            {
                Debug.LogError(teleportBlocksStartPositionsNullCellErrorMessageFirst + i.ToString() + teleportBlocksStartPositionsNullCellErrorMessageSecond);
                return false;
            }
        }
        #endregion ErrorChecks

        #region TestPositionsAndTeleportBlocksAround
        List<TileBase> blocks = new List<TileBase>();
        for (int i = 0; i < startPositions.Length; i++)
        {
            blocks.Add(tileMap.GetTile(startPositions[i]));
            interfaceBlockRemover.RemoveBlock(startPositions[i]);
        }

        bool canTeleportBlocks = true;

        for (int i = 0; i < endPositions.Length; i++)
        {
            if (tileMap.GetTile(endPositions[i]) != null)
            {
                canTeleportBlocks = false;
                break;
            }
        }

        if (canTeleportBlocks)
        {
            for (int i = 0; i < endPositions.Length; i++)
            {
                interfaceBlockPlacer.AddBlock(endPositions[i], blocks[i]);
            }
            return true;
        }
        else
        {
            for (int i = 0; i < startPositions.Length; i++)
            {
                interfaceBlockPlacer.AddBlock(startPositions[i], blocks[i]);
            }
            return false;
        }
        #endregion TestPositionsAndTeleportBlocksAround
    }

    public bool MoveBlock(Vector3Int startPosition, Vector3Int direction, int distance)
    {
        if (tileMap.GetTile(startPosition) == null)
        {
            Debug.LogError(moveBlockNullStartPositionErrorMessage);
            return false;
        }

        if (direction == new Vector3Int(0, 0, 0))
        {
            Debug.LogError(moveBlockNullDirectionErrorMessage);
            return false;
        }

        if (distance == 0)
        {
            Debug.LogError(moveBlockDistanceEqualsZeroErrorMessage);
            return false;
        }

        Vector3Int lastStep = startPosition;
        Vector3Int nextStep = startPosition + direction;

        for (int i = 0; i < distance; i++)
        {
            if (tileMap.GetTile(nextStep) != null)
            {
                Debug.Log(moveBlockFoundObstacleMessageFirstPart + nextStep + moveBlockFoundObstacleMessageSecondPart);
                break;
            }
            TeleportBlock(lastStep, nextStep);

            lastStep = nextStep;
            nextStep += direction;
        }

        return true;
    }

    public bool MoveBlocks(Vector3Int[] startPositions, Vector3Int direction, int distance)
    {
        #region ErrorChecks
        if (startPositions.Length == 0)
        {
            Debug.LogError(moveBlocksStartPositionsEqualsZeroErrorMessage);
            return false;
        }

        if (direction == new Vector3Int(0, 0, 0))
        {
            Debug.LogError(moveBlocksNullDirectionErrorMessage);
            return false;
        }

        if (distance == 0)
        {
            Debug.LogError(moveBlocksZeroDistanceErrorMessageFirstPart);
            return false;
        }

        for (int i = 0; i < startPositions.Length; i++)
        {
            if (tileMap.GetTile(startPositions[i]) == null)
            {
                Debug.LogError(moveBlocksNoBlockErrorMessageFirstPart + i.ToString() + moveBlocksNoBlockErrorMessageSecondPart);
                return false;
            }
        }
        #endregion ErrorChecks

        List<TileBase> tiles = new List<TileBase>();

        for (int i = 0; i < startPositions.Length; i++)
        {
            tiles.Add(tileMap.GetTile(startPositions[i]));
        }

        Vector3Int[] lastSteps = startPositions;
        List<Vector3Int> nextSteps = new List<Vector3Int>();
        for (int i = 0; i < distance; i++)
        {
            for (int j = 0; j < lastSteps.Length; j++)
            {
                nextSteps.Add(lastSteps[j] + direction);
                interfaceBlockRemover.RemoveBlock(lastSteps[j]);
            }

            bool canMoveOneStep = true;

            for (int j = 0; j < nextSteps.Count; j++)
            {
                if (tileMap.GetTile(nextSteps[j]) != null)
                {
                    canMoveOneStep = false;
                    break;
                }
            }

            if (canMoveOneStep)
            {
                for (int j = 0; j < nextSteps.Count; j++)
                {
                    tileMap.SetTile(nextSteps[j], tiles[j]);
                }
            }
            else
            {
                for (int j = 0; j < lastSteps.Length; j++)
                {
                    tileMap.SetTile(lastSteps[j], tiles[j]);
                }
                return false;
            }

            lastSteps = nextSteps.ToArray();
            nextSteps = new List<Vector3Int>();
        }
        return true;
    }

    public bool MoveBlockDownwards(Vector3Int startPosition, int distance)
    {
        return MoveBlock(startPosition, new Vector3Int(0, -1, 0), distance);
    }

    public bool MoveBlocksDownwards(Vector3Int[] startPositions, int distance)
    {
        return MoveBlocks(startPositions, new Vector3Int(0, -1, 0), distance);
    }

    #region FunctionsForTests
    public string GetTeleportBlockNullStartPositionErrorMessage()
    {
        return teleportBlockNullStartPositionErrorMessage;
    }

    public string GetTeleportBlockNullEndPositionErrorMessage()
    {
        return teleportBlockNullEndPositionErrorMessage;
    }

    public string GetTeleportBlocksStartPositionsNotSizedAsEndPositionsErrorMessage()
    {
        return teleportBlocksStartPositionsNotSizedAsEndPositionsErrorMessage;
    }

    public string GetTeleportBlocksSingularFunctionShouldBeUsedInsteadErrorMessage()
    {
        return teleportBlocksSingularFunctionShouldBeUsedInsteadErrorMessage;
    }

    public string GetTeleportBlocksStartPositionsErrorMessage()
    {
        return teleportBlocksStartPositionsErrorMessage;
    }

    public string GetTeleportBlocksEndPositionsErrorMessage()
    {
        return teleportBlocksEndPositionsErrorMessage;
    }

    public string GetTeleportBlocksStartPositionsNullCellErrorMessageFirst()
    {
        return teleportBlocksStartPositionsNullCellErrorMessageFirst;
    }

    public string GetTeleportBlocksStartPositionsNullCellErrorMessageSecond()
    {
        return teleportBlocksStartPositionsNullCellErrorMessageSecond;
    }

    public string GetMoveBlockNullStartPositionErrorMessage()
    {
        return moveBlockNullStartPositionErrorMessage;
    }

    public string GetMoveBlockNullDirectionErrorMessage()
    {
        return moveBlockNullDirectionErrorMessage;
    }

    public string GetMoveBlockDistanceEqualsZeroErrorMessage()
    {
        return moveBlockDistanceEqualsZeroErrorMessage;
    }

    public string GetMoveBlockFoundObstacleMessageFirstPart()
    {
        return moveBlockFoundObstacleMessageFirstPart;
    }

    public string GetMoveBlockFoundObstacleMessageSecondPart()
    {
        return moveBlockFoundObstacleMessageSecondPart;
    }

    public string GetMoveBlocksStartPositionsEqualsZeroErrorMessage()
    {
        return moveBlocksStartPositionsEqualsZeroErrorMessage;
    }

    public string GetMoveBlocksNullDirectionErrorMessage()
    {
        return moveBlocksNullDirectionErrorMessage;
    }

    public string GetMoveBlocksZeroDistanceErrorMessageFirstPart()
    {
        return moveBlocksZeroDistanceErrorMessageFirstPart;
    }

    public string GetMoveBlocksNoBlockErrorMessageFirstPart()
    {
        return moveBlocksNoBlockErrorMessageFirstPart;
    }

    public string GetMoveBlocksNoBlockErrorMessageSecondPart()
    {
        return moveBlocksNoBlockErrorMessageSecondPart;
    }

    #endregion FunctionsForTests
}
