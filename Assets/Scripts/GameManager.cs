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
    public static BlockMatchChecker blockMatchChecker;

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
        blockMatchChecker = GetComponent<BlockMatchChecker>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartManagers();
        StartDataManagerVariables();

        blockManipulator.tileMap.ClearAllTiles();
        CreateStartingGameSpace();
        CreateCurrentBlockSet();
        CreateNextBlockSet();

        PlaceCurrentBlockSetAtGameArea();
    }

    public void StartManagers()
    {
        dataManager.StartManager();
        inputManager.StartManager();
        gameTimer.StartManager();
        blockManipulator.StartManager();
    }

    public void StartDataManagerVariables()
    {
        dataManager.secondsToApplyBlockGravity = dataManager.blockGravityDiffValues[DataManager.currentDifficultyLevel];
        dataManager.secondsToCheckPlayerInput = dataManager.playerInputTimeDiffValues[DataManager.currentDifficultyLevel];
        dataManager.secondsOfDelayAfterAllMatchChecks = dataManager.delayAfterMatchDiffValues[DataManager.currentDifficultyLevel];
        dataManager.timeWithoutPBlockGravityBeingApplied = 0;
        
        dataManager.isDelayingAfterAllMatchChecks = false;
    }

    public void UpdateDifficultyValues()
    {
        dataManager.secondsToApplyBlockGravity = dataManager.blockGravityDiffValues[DataManager.currentDifficultyLevel];
        dataManager.secondsToCheckPlayerInput = dataManager.playerInputTimeDiffValues[DataManager.currentDifficultyLevel];
        dataManager.secondsOfDelayAfterAllMatchChecks = dataManager.delayAfterMatchDiffValues[DataManager.currentDifficultyLevel];
    }

    public void CreateStartingGameSpace()
    {
        blockManipulator.tileMap.ClearAllTiles();

        int leftLimit = -dataManager.gameSpace.x / 2 - 1;
        int rightLimit = dataManager.gameSpace.x / 2 + 1;
        int lowerLimit = -1;
        int upperLimit = dataManager.gameSpace.y + 1;

        dataManager.leftLimit = leftLimit;
        dataManager.rightLimit = rightLimit;
        dataManager.lowerLimit = lowerLimit;
        dataManager.upperLimit = upperLimit;

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
        List<Vector3Int> positions = CreateTilePositionList(3);

        currentBlockSet = new BlockSet(tiles, positions);
    }

    public void CreateNextBlockSet()
    {
        List<Tile> tiles = CreateRandomizedTileList(3);
        List<Vector3Int> positions = CreateTilePositionList(3);

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

    public List<Vector3Int> CreateTilePositionList(int amountOfTiles)
    {
        List<Vector3Int> positions = new List<Vector3Int>();
        for (int i = 0; i < amountOfTiles; i++)
        {
            positions.Add(new Vector3Int(0, i, 0));

        }
        return positions;
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
            float deltaTime = Time.deltaTime * 1f;

            if (dataManager.isDelayingAfterAllMatchChecks)
            {
                dataManager.timeForDelayAfterAllMatchChecks += deltaTime;
                if (dataManager.timeForDelayAfterAllMatchChecks >= dataManager.secondsOfDelayAfterAllMatchChecks)
                {
                    dataManager.timeForDelayAfterAllMatchChecks = 0.0f;
                    dataManager.timeWithoutInputCheck = 0.0f;
                    dataManager.timeWithoutPBlockGravityBeingApplied = 0.0f;
                    dataManager.isDelayingAfterAllMatchChecks = false;
                }
                else
                {
                    return;
                }
            }


            if (inputManager.pauseIsCurrentlyPressed)
            {
                inputManager.OnPause();
            }
            else
            {
                if (inputManager.moveAmount.y <= 0 && inputManager.cycleButtonIsCurrentlyPressedAfterApplyingItsEffect)
                {
                    inputManager.cycleButtonIsCurrentlyPressedAfterApplyingItsEffect = false;
                }

                if (inputManager.moveButtonIsCurrentlyPressed)
                {
                    dataManager.timeWithoutInputCheck += deltaTime;
                    if (dataManager.timeWithoutInputCheck >= dataManager.secondsToCheckPlayerInput)
                    {
                        dataManager.timeWithoutInputCheck = 0.0f;
                        inputManager.moveButtonIsCurrentlyPressed = false;
                    }
                }
                else
                {
                    if (inputManager.moveAmount.x < 0)
                    {
                        MoveBlockSetToTheLeft();
                        inputManager.moveButtonIsCurrentlyPressed = true;
                    }
                    else if (inputManager.moveAmount.x > 0)
                    {
                        MoveBlockSetToTheRight();
                        inputManager.moveButtonIsCurrentlyPressed = true;
                    }
                    else if (inputManager.moveAmount.y > 0 && !inputManager.cycleButtonIsCurrentlyPressedAfterApplyingItsEffect)
                    {
                        CycleCurrentBlockSet();
                        inputManager.cycleButtonIsCurrentlyPressedAfterApplyingItsEffect = true;
                    }
                    else if (inputManager.moveAmount.y < 0)
                    {
                        MoveBlockSetStraightDown();
                        inputManager.moveButtonIsCurrentlyPressed = true;
                    }
                }
            }



            dataManager.timeWithoutPBlockGravityBeingApplied += deltaTime;
            if (dataManager.timeWithoutPBlockGravityBeingApplied >= dataManager.secondsToApplyBlockGravity)
            {
                dataManager.timeWithoutPBlockGravityBeingApplied %= dataManager.secondsToApplyBlockGravity;

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
                    bool haveAMatchBeenFound = false;
                    do
                    {
                        haveAMatchBeenFound = blockMatchChecker.FullMatchCheck();
                        CalculateScorePointsFromAllMatches();
                        UIManager.UpdateScoreUI();
                        RemoveMatches();
                        ApplyGravity();
                        ApplyDifficultyCheck();
                    } while (haveAMatchBeenFound);
                    CurrentBlockSetReceivesNextBlockSet();
                    CreateNextBlockSet();
                    PlaceCurrentBlockSetAtGameArea();
                    dataManager.isDelayingAfterAllMatchChecks = true;
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

    public void CalculateScorePointsFromAllMatches()
    {
        CalculateScorePointsFromMatches(blockMatchChecker.HorizontalMatchSetsFound);
        CalculateScorePointsFromMatches(blockMatchChecker.VerticalMatchSetsFound);
        CalculateScorePointsFromMatches(blockMatchChecker.LeftDownMatchSetsFound);
        CalculateScorePointsFromMatches(blockMatchChecker.RightDownMatchSetsFound);
    }


    public void CalculateScorePointsFromMatches(List<MatchSet> matches)
    {
        for (int i = 0; i < matches.Count; i++)
        {
            int matchPointValue = 0;
            switch (matches[i].positions.Count)
            {
                case 3:
                    matchPointValue += DataManager.MATCH3SCOREVALUE;
                    break;
                case 4:
                    matchPointValue += DataManager.MATCH4SCOREVALUE;
                    break;
                case 5:
                    matchPointValue += DataManager.MATCH5SCOREVALUE;
                    break;
                case 6:
                    matchPointValue += DataManager.MATCH6SCOREVALUE;
                    break;
                default:
                    break;
            }
            DataManager.currentScore += matchPointValue;
        }
    }

    public void RemoveMatches()
    {
        RemoveMatchBlocks(blockMatchChecker.HorizontalMatchSetsFound);
        RemoveMatchBlocks(blockMatchChecker.VerticalMatchSetsFound);
        RemoveMatchBlocks(blockMatchChecker.LeftDownMatchSetsFound);
        RemoveMatchBlocks(blockMatchChecker.RightDownMatchSetsFound);
    }

    public void ApplyGravity()
    {
        for (int i = GameManager.dataManager.leftLimit + 1; i < GameManager.dataManager.rightLimit; i++)
        {
            Vector2Int startingPosition = new Vector2Int(i, GameManager.dataManager.lowerLimit + 1);
            List<Tile> tiles = new List<Tile>();

            for (int j = GameManager.dataManager.lowerLimit + 1; j < GameManager.dataManager.upperLimit; j++)
            {
                Tile tile = (Tile)GameManager.blockManipulator.tileMap.GetTile(new Vector3Int(i, j, 0));
                if (tile != null)
                {
                    tiles.Add(tile);
                }
            }

            int tilesListPosition = 0;
            for (int j = GameManager.dataManager.lowerLimit + 1; j < GameManager.dataManager.upperLimit; j++)
            {
                if (tilesListPosition < tiles.Count)
                {
                    GameManager.blockManipulator.GetBlockPlacer().AddBlock(new Vector3Int(i, j, 0), tiles[tilesListPosition]);
                    tilesListPosition++;
                }
                else
                {
                    GameManager.blockManipulator.GetBlockRemover().EraseBlock(new Vector3Int(i, j, 0));
                }
                
            }
        }
    }

    public void ApplyDifficultyCheck()
    {
        if (DataManager.currentDifficultyLevel < dataManager.difficultyScoreValues.Count)
        {
            if (dataManager.difficultyScoreValues[DataManager.currentDifficultyLevel] <= DataManager.currentScore)
            {
                DataManager.currentDifficultyLevel += 1;
                UpdateDifficultyValues();
                UIManager.UpdateDifficultyLevelUI();
            }
        }
    }

    private void RemoveMatchBlocks(List<MatchSet> matches)
    {
        for (int i = 0; i < matches.Count; i++)
        {
            blockManipulator.GetBlockRemover().EraseBlocks(matches[i].positions.ToArray());
        }
    }

    public void CycleCurrentBlockSet()
    {
        BlockSet originalBlockSet = currentBlockSet;
        List<Tile> originalTileList = originalBlockSet.tiles;
        List<Tile> newTileList = new List<Tile>();

        newTileList.Add(originalTileList[originalTileList.Count - 1]);
        for (int i = 0; i < originalTileList.Count-1; i++)
        {
            newTileList.Add(originalTileList[i]);
        }
        
        currentBlockSet = new BlockSet(newTileList, originalBlockSet.positions);
        for (int i = 0; i < currentBlockSet.tiles.Count; i++)
        {
            blockManipulator.tileMap.SetTile(currentBlockSet.GetPositionsArray()[i], currentBlockSet.tiles[i]); 
        }
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
