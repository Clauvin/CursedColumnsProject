using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static DataManager dataManager;
    public static PlayerInteractions inputManager;
    public static GameTimer gameTimer;
    public static BlockManipulator blockManipulator;

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
        Tile tileToUse = DataManager.commonTiles[0];

        blockManipulator.GetBlockPlacer().AddSameBlockMultiplesTimes(blocksToAdd, tileToUse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
