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

    public bool TeleportBlocks(Vector3Int[] startPositions, Vector3Int[] endPositions)
    {
        #region ErrorChecks
        if (startPositions.Length != endPositions.Length)
        {
            Debug.LogError("TeleportsBlocks - startPositions and endPositions havve different sizes.");
            return false;
        }

        if (startPositions.Length == 1 && endPositions.Length == 1)
        {
            Debug.LogWarning("TeleportBlocks - TeleportBlock should be used instead.");
        }

        if (startPositions.Length == 0)
        {
            Debug.LogError("TeleportBlocks - startPositions does not have positions.");
            return false;
        }

        if (endPositions.Length == 0)
        {
            Debug.LogError("TeleportBlocks - endPositions does not have positions.");
            return false;
        }

        for (int i = 0; i < startPositions.Length; i++)
        {
            if (tileMap.GetTile(startPositions[i]) == null)
            {
                Debug.LogError("TeleportBlocks - startPositions[" + i.ToString() + "] does not have a block.");
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
            Debug.LogError("MoveBlock - startPosition does not have a block.");
            return false;
        }

        if (direction == new Vector3Int(0, 0, 0))
        {
            Debug.LogError("MoveBlock - the direction points to nowhere.");
            return false;
        }

        if (distance == 0)
        {
            Debug.LogError("MoveBlock - Distance == 0, it should be different of zero.");
            return false;
        }

        Vector3Int lastStep = startPosition;
        Vector3Int nextStep = startPosition + direction;

        for (int i = 0; i < distance; i++)
        {
            if (tileMap.GetTile(nextStep) != null)
            {
                Debug.Log("MoveBlock - found obstacle at " + nextStep + ", stopping movement.");
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
        return false;
    }

    public bool MoveBlockDownwards(Vector3Int startPosition, int distance)
    {
        return MoveBlock(startPosition, new Vector3Int(0, -1, 0), distance);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
