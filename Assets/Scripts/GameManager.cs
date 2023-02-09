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
        //For 0.3, change commonTiles[0] to commonTiles[random], and add random tiles.
        Tile tile = DataManager.commonTiles[0];
        List<Vector3Int> positions = new List<Vector3Int>();
        positions.Add(new Vector3Int(0, 0, 0));
        positions.Add(new Vector3Int(0, 1, 0));
        positions.Add(new Vector3Int(0, 2, 0));

        currentBlockSet = new BlockSet(tile, positions);
    }

    public void CreateNextBlockSet()
    {
        //For 0.3, change commonTiles[0] to commonTiles[random], and add random tiles.
        Tile tile = DataManager.commonTiles[0];
        List<Vector3Int> positions = new List<Vector3Int>();
        positions.Add(new Vector3Int(0, 0, 0));
        positions.Add(new Vector3Int(0, 1, 0));
        positions.Add(new Vector3Int(0, 2, 0));

        nextBlockSet = new BlockSet(tile, positions);
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
        //Game cycle
        //Check player's input
        //If left or right, try to move to the left or to the right
        //If down, try to speed it down
        //If it collided, make a new currentBlockSet and control that.
        //Move the set one block down
        //If it collided, make a new currentBlockSet and control that.
    }
}
