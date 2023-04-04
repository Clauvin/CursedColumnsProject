using BlockSets;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class TestGameManager : MonoBehaviour
{

    GameObject gameManagerPrefab;
    GameManager gameManager;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        gameManagerPrefab = (GameObject)Resources.Load("TestResources/GameManager");
        gameManager = gameManagerPrefab.GetComponent<GameManager>();
        gameManager.SetUpStaticManagers();

        yield return new WaitForFixedUpdate();
    }
   
    [UnityTest]
    public IEnumerator TestCreatingGameSpace()
    {
        gameManager.StartManagers();

        gameManager.StartDataManagerVariables();

        gameManager.CreateStartingGameSpace();

        int leftLimit = -GameManager.dataManager.gameSpace.x / 2 - 1;
        int rightLimit = GameManager.dataManager.gameSpace.x / 2 + 1;
        int lowerLimit = -1;
        int upperLimit = GameManager.dataManager.gameSpace.y + 1;

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

        for (int i = 0; i < newBlocks.Count; i++)
        {
            if (GameManager.blockManipulator.tileMap.GetTile(newBlocks[i]) != DataManager.unpassableTile)
            {
                Assert.Fail();
            }
        }

        Assert.Pass();


        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestCreateCurrentBlockSet()
    {
        gameManager.StartManagers();

        gameManager.StartDataManagerVariables();

        gameManager.CreateCurrentBlockSet();

        BlockSet blockSet = GameManager.currentBlockSet;

        for (int i = 0; i < blockSet.tiles.Count; i++)
        {
            UnityEngine.Tilemaps.Tile tile = blockSet.tiles[i];
            bool failure = true;
            for (int j = 0; j < DataManager.commonTiles.Length; j++)
            {
                if (tile == DataManager.commonTiles[j])
                {
                    failure = false;
                    break;
                }
            }
            if (failure)
            {
                Assert.Fail();
            }
        }

        Assert.Pass();

        yield return new WaitForFixedUpdate();

    }

    [UnityTest]
    public IEnumerator TestRemoveHorizontalMatches()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(-1, 2, 0));
        positionsList.Add(new Vector3Int(0, 2, 0));
        positionsList.Add(new Vector3Int(1, 2, 0));

        if (IsRemovingMatchesWorking(positionsList))
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }

        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestRemoveVerticalMatches()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(-1, 3, 0));
        positionsList.Add(new Vector3Int(-1, 2, 0));
        positionsList.Add(new Vector3Int(-1, 1, 0));

        if (IsRemovingMatchesWorking(positionsList))
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }

        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestRemoveLeftDownMatches()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(-1, 3, 0));
        positionsList.Add(new Vector3Int(0, 4, 0));
        positionsList.Add(new Vector3Int(1, 5, 0));

        if (IsRemovingMatchesWorking(positionsList))
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }

        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestRemoveRightDownMatches()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(-1, 3, 0));
        positionsList.Add(new Vector3Int(0, 2, 0));
        positionsList.Add(new Vector3Int(1, 1, 0));

        if (IsRemovingMatchesWorking(positionsList))
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }

        yield return new WaitForFixedUpdate();
    }

    bool IsRemovingMatchesWorking(List<Vector3Int> positionsList)
    {
        gameManager.StartManagers();
        gameManager.StartDataManagerVariables();
        gameManager.CreateStartingGameSpace();

        BlockMatchChecker blockMatchChecker;
        blockMatchChecker = gameManagerPrefab.GetComponent<BlockMatchChecker>();

        Tile tile = DataManager.commonTiles[0];

        BlockPlacer.AddSameBlockMultiplesTimesInATilemap(positionsList.ToArray(), tile, blockMatchChecker.tileMap);

        blockMatchChecker.FullMatchCheck();

        gameManager.RemoveMatches();

        for (int i = 0; i < positionsList.Count; i++)
        {
            if (GameManager.blockManipulator.tileMap.GetTile(positionsList[i]) != null)
            {
                return false;
            }
        }

        return true;
    }

    [UnityTest]
    public IEnumerator TestCreateNextBlockSet()
    {
        gameManager.StartManagers();

        gameManager.StartDataManagerVariables();

        gameManager.CreateNextBlockSet();

        BlockSet blockSet = GameManager.nextBlockSet;

        for (int i = 0; i < blockSet.tiles.Count; i++)
        {
            UnityEngine.Tilemaps.Tile tile = blockSet.tiles[i];
            bool failure = true;
            for (int j = 0; j < DataManager.commonTiles.Length; j++)
            {
                if (tile == DataManager.commonTiles[j])
                {
                    failure = false;
                    break;
                }
            }
            if (failure)
            {
                Assert.Fail();
            }
        }

        Assert.Pass();

        yield return new WaitForFixedUpdate();

    }

    [UnityTest]
    public IEnumerator TestCycleCurrentBlockSet()
    {
        gameManager.StartManagers();

        gameManager.StartDataManagerVariables();

        gameManager.CreateCurrentBlockSet();

        BlockSet blockSet = GameManager.currentBlockSet;

        gameManager.CycleCurrentBlockSet();

        if (blockSet.tiles[blockSet.tiles.Count-1] != GameManager.currentBlockSet.tiles[0])
        {
            Assert.Fail();
        }

        for (int i = 0; i < blockSet.tiles.Count-1; i++)
        {
            if (blockSet.tiles[i] != GameManager.currentBlockSet.tiles[i + 1])
            {
                Assert.Fail();
            }
        }

        Assert.Pass();

        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestGravityAfterCheck()
    {
        gameManager.StartManagers();

        gameManager.StartDataManagerVariables();

        GameManager.blockManipulator.GetBlockPlacer().AddBlock(new Vector3Int(-1, 5, 0), DataManager.commonTiles[0]);

        gameManager.ApplyGravity();

        if (GameManager.blockManipulator.tileMap.GetTile(new Vector3Int(-1, 0, 0)) == null)
        {
            Assert.Fail();
        }
        else
        {
            Assert.Pass();
        }

        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestMatch3ScoreValuation()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(-1, 0, 0));
        positionsList.Add(new Vector3Int(0, 0, 0));
        positionsList.Add(new Vector3Int(1, 0, 0));

        if (TestMatchScoreValidation(positionsList, DataManager.MATCH3SCOREVALUE))
        {
            Assert.Pass();
        }

        Assert.Fail();

        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestMatch4ScoreValuation()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(-2, 0, 0));
        positionsList.Add(new Vector3Int(-1, 0, 0));
        positionsList.Add(new Vector3Int(0, 0, 0));
        positionsList.Add(new Vector3Int(1, 0, 0));

        if (TestMatchScoreValidation(positionsList, DataManager.MATCH4SCOREVALUE))
        {
            Assert.Pass();
        }

        Assert.Fail();

        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestMatch5ScoreValuation()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(-2, 0, 0));
        positionsList.Add(new Vector3Int(-1, 0, 0));
        positionsList.Add(new Vector3Int(0, 0, 0));
        positionsList.Add(new Vector3Int(1, 0, 0));
        positionsList.Add(new Vector3Int(2, 0, 0));

        if (TestMatchScoreValidation(positionsList, DataManager.MATCH5SCOREVALUE))
        {
            Assert.Pass();
        }

        Assert.Fail();

        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestMatch6ScoreValuation()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(-2, 0, 0));
        positionsList.Add(new Vector3Int(-1, 0, 0));
        positionsList.Add(new Vector3Int(0, 0, 0));
        positionsList.Add(new Vector3Int(1, 0, 0));
        positionsList.Add(new Vector3Int(2, 0, 0));
        positionsList.Add(new Vector3Int(3, 0, 0));

        if (TestMatchScoreValidation(positionsList, DataManager.MATCH6SCOREVALUE))
        {
            Assert.Pass(); 
        }

        Assert.Fail();

        yield return new WaitForFixedUpdate();
    }

    bool TestMatchScoreValidation(List<Vector3Int> positionsList, int valueToValidate)
    {
        gameManager.StartManagers();

        gameManager.StartDataManagerVariables();

        gameManager.CreateStartingGameSpace();

        Tile tile = DataManager.commonTiles[0];

        BlockMatchChecker blockMatchChecker;
        blockMatchChecker = gameManagerPrefab.GetComponent<BlockMatchChecker>();

        BlockPlacer.AddSameBlockMultiplesTimesInATilemap(positionsList.ToArray(), tile, blockMatchChecker.tileMap);

        blockMatchChecker.FullMatchCheck();
        gameManager.CalculateScorePointsFromAllMatches();

        if (DataManager.currentScore != valueToValidate)
        {
            return false;
        }

        return true;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameManager.blockManipulator.tileMap.ClearAllTiles();
        gameManagerPrefab = null;
        gameManager = null;

        DataManager.currentScore = 0;

        yield return new WaitForFixedUpdate();
    }
}
