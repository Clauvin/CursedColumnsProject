using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using BlockSets;

public class GameManager : MonoBehaviour
{
    public static DataManager dataManager;
    public static PlayerInteractions inputManager;
    public static GameTimer gameTimer;
    public static BlockManipulator blockManipulator;

    public static BlockSet currentBlockSet;
    public static BlockSet nextBlockSet;

    void Awake()
    {
        SetUpStaticManagers();
    }

    public void SetUpStaticManagers()
    {
        dataManager = GetComponent<DataManager>();
        inputManager = GetComponent<PlayerInteractions>();
        gameTimer = GetComponent<GameTimer>();
        blockManipulator = GetComponent<BlockManipulator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dataManager.StartManager();
        inputManager.StartManager();
        gameTimer.StartManager();
        blockManipulator.StartManager();

        dataManager.currentBlockSpeedPerSecond = 1;
        dataManager.timePassedWithoutBlockMovement = 0;
        blockManipulator.tileMap.ClearAllTiles();
        CreateStartingGameSpace();
        CreateCurrentBlockSet();
        CreateNextBlockSet();

        PlaceCurrentBlockSetAtGameArea();
    }

    public void CreateStartingGameSpace()
    {
        int leftLimit = -dataManager.gameSpace.x / 2 - 1;
        int rightLimit = dataManager.gameSpace.x / 2 + 1;
        int lowerLimit = -1;
        int upperLimit = dataManager.gameSpace.y + 1;

        List<Vector3Int> newBlocks = new List<Vector3Int>();

        for (int i = leftLimit; i <= rightLimit; i++)
        {
            newBlocks.Add(new Vector3Int(i, lowerLimit, 0));
        }

        for (int i = lowerLimit; i < upperLimit; i++)
        {
            newBlocks.Add(new Vector3Int(leftLimit, i, 0));
            newBlocks.Add(new Vector3Int(rightLimit, i, 0));
        }

        Vector3Int[] blocksToAdd = newBlocks.ToArray();
        Tile tileToUse = DataManager.unpassableTile;

        blockManipulator.GetBlockPlacer().AddSameBlockMultiplesTimes(blocksToAdd, tileToUse);
    }

    public void CreateCurrentBlockSet()
    {
        List<Tile> tiles = CreateRandomizedTileList(3);
        List<Vector3Int> positions = new List<Vector3Int>();
        positions.Add(new Vector3Int(0, 0, 0));
        positions.Add(new Vector3Int(0, 1, 0));
        positions.Add(new Vector3Int(0, 2, 0));

        currentBlockSet = new BlockSet(tiles, positions);
    }

    public void CreateNextBlockSet()
    {
        List<Tile> tiles = CreateRandomizedTileList(3);
        List<Vector3Int> positions = new List<Vector3Int>();
        positions.Add(new Vector3Int(0, 0, 0));
        positions.Add(new Vector3Int(0, 1, 0));
        positions.Add(new Vector3Int(0, 2, 0));

        nextBlockSet = new BlockSet(tiles, positions);
    }

    public List<Tile> CreateRandomizedTileList(int amountOfTiles)
    {
        List<Tile> tiles = new List<Tile>();
        for (int i = 0; i < amountOfTiles; i++)
        {
            tiles.Add(DataManager.commonTiles[Random.Range(0, DataManager.commonTiles.Length)]);
        }
        return tiles;
    }

    private void PlaceCurrentBlockSetAtGameArea()
    {
        for (int i = 0; i < currentBlockSet.positions.Count; i++)
        {
            currentBlockSet.positions[i] += dataManager.blockSetSpawnPoint;
        }

        blockManipulator.GetBlockPlacer().AddBlocks(currentBlockSet.GetPositionsArray(), currentBlockSet.GetTilesArray());
    }

    // Update is called once per frame
    void Update()
    {
        if (!DataManager.isPaused)
        {
            dataManager.timePassedWithoutBlockMovement += Time.deltaTime;
            if (dataManager.timePassedWithoutBlockMovement >= dataManager.currentBlockSpeedPerSecond)
            {
                dataManager.timePassedWithoutBlockMovement %= dataManager.currentBlockSpeedPerSecond;

                if (inputManager.pauseIsCurrentlyPressed)
                {
                    inputManager.OnPause();
                }

                if (inputManager.moveAmount.x < 0)
                {
                    MoveBlockSetToTheLeft();
                }
                else if (inputManager.moveAmount.x > 0)
                {
                    MoveBlockSetToTheRight();
                }
                else if (inputManager.moveAmount.y < 0)
                {
                    MoveBlockSetStraightDown();
                }

                bool blockWentDown = blockManipulator.MoveBlocksDownwards(currentBlockSet.GetPositionsArray(), 1);
                if (blockWentDown)
                {
                    for (int i = 0; i < currentBlockSet.positions.Count; i++)
                    {
                        currentBlockSet.positions[i] += new Vector3Int(0, -1, 0);
                    }
                }
                else
                {
                    CurrentBlockSetReceivesNextBlockSet();
                    CreateNextBlockSet();
                    PlaceCurrentBlockSetAtGameArea();
                }
            }
        }
        else
        {
            if (inputManager.pauseIsCurrentlyPressed && inputManager.pauseJustPressed)
            {
                inputManager.OnPause();
            }
        }
        
    }

    private bool MoveBlockSetToTheLeft()
    {
        bool blocksWentLeft = blockManipulator.MoveBlocks(currentBlockSet.GetPositionsArray(), new Vector3Int(-1, 0, 0), 1);
        if (blocksWentLeft)
        {
            for (int i = 0; i < currentBlockSet.positions.Count; i++)
            {
                currentBlockSet.positions[i] += new Vector3Int(-1, 0, 0);
            }
        }

        return blocksWentLeft;
    }

    private bool MoveBlockSetToTheRight()
    {
        bool blocksWentRight = blockManipulator.MoveBlocks(currentBlockSet.GetPositionsArray(), new Vector3Int(1, 0, 0), 1);
        if (blocksWentRight)
        {
            for (int i = 0; i < currentBlockSet.positions.Count; i++)
            {
                currentBlockSet.positions[i] += new Vector3Int(1, 0, 0);
            }
        }

        return blocksWentRight;
    }

    private void MoveBlockSetStraightDown()
    {
        bool blocksCanStillGoDown = true;
        while (blocksCanStillGoDown)
        {
            bool blockWentDown = blockManipulator.MoveBlocksDownwards(currentBlockSet.GetPositionsArray(), 1);
            if (blockWentDown)
            {
                for (int i = 0; i < currentBlockSet.positions.Count; i++)
                {
                    currentBlockSet.positions[i] += new Vector3Int(0, -1, 0);
                }
            }
            blocksCanStillGoDown = blockWentDown;
        }
    }

    private void CurrentBlockSetReceivesNextBlockSet()
    {
        currentBlockSet = nextBlockSet;
    }
}
