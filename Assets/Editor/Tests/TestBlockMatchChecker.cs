using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

[TestFixture]
public class TestBlockMatchChecker
{
    GameObject gameManagerPrefab;
    GameManager gameManager;
    BlockMatchChecker blockMatchChecker;

    [OneTimeSetUp]
    public void NewTestSetUp()
    {
        SceneManager.LoadScene("GameScene");
    }

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        yield return new WaitForFixedUpdate();

        gameManagerPrefab = (GameObject)Resources.Load("TestResources/GameManager");
        gameManager = gameManagerPrefab.GetComponent<GameManager>();
        gameManager.SetUpStaticManagers();
        gameManager.StartManagers();
        gameManager.StartDataManagerVariables();
        gameManager.CreateStartingGameSpace();

        blockMatchChecker = gameManagerPrefab.GetComponent<BlockMatchChecker>();

       

        yield return new WaitForFixedUpdate();
     }

    [UnityTest]
    public IEnumerator TestHorizontalCheck()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(-1, 0, 0));
        positionsList.Add(new Vector3Int(0, 0, 0));
        positionsList.Add(new Vector3Int(1, 0, 0));

        if (TestCheck(positionsList, 1, BlockSets.MatchSet.Direction.HORIZONTAL))
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
    public IEnumerator TestHorizontalFailureCheck()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(-1, 0, 0));
        positionsList.Add(new Vector3Int(0, 0, 0));
        positionsList.Add(new Vector3Int(2, 0, 0));

        if (TestCheck(positionsList, 0, BlockSets.MatchSet.Direction.HORIZONTAL))
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
    public IEnumerator TestVerticalCheck()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(0, -1, 0));
        positionsList.Add(new Vector3Int(0, 0, 0));
        positionsList.Add(new Vector3Int(0, 1, 0));

        if (TestCheck(positionsList, 1, BlockSets.MatchSet.Direction.VERTICAL))
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
    public IEnumerator TestVerticalFailureCheck()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(0, 3, 0));
        positionsList.Add(new Vector3Int(0, 2, 0));
        positionsList.Add(new Vector3Int(0, 0, 0));

        if (TestCheck(positionsList, 0, BlockSets.MatchSet.Direction.VERTICAL))
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
    public IEnumerator TestLeftDownCheck()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(-1, -1, 0));
        positionsList.Add(new Vector3Int(0, 0, 0));
        positionsList.Add(new Vector3Int(1, 1, 0));

        if (TestCheck(positionsList, 1, BlockSets.MatchSet.Direction.DOWNLEFT))
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
    public IEnumerator TestLeftDownFailureCheck()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(3, 3, 0));
        positionsList.Add(new Vector3Int(2, 2, 0));
        positionsList.Add(new Vector3Int(0, 0, 0));

        if (TestCheck(positionsList, 0, BlockSets.MatchSet.Direction.DOWNLEFT))
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
    public IEnumerator TestRightDownCheck()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(1, -1, 0));
        positionsList.Add(new Vector3Int(0, 0, 0));
        positionsList.Add(new Vector3Int(-1, 1, 0));

        if (TestCheck(positionsList, 1, BlockSets.MatchSet.Direction.DOWNRIGHT))
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
    public IEnumerator TestRightDownFailureCheck()
    {
        List<Vector3Int> positionsList = new List<Vector3Int>();
        positionsList.Add(new Vector3Int(3, 1, 0));
        positionsList.Add(new Vector3Int(2, 0, 0));
        positionsList.Add(new Vector3Int(1, 0, 0));

        if (TestCheck(positionsList, 0, BlockSets.MatchSet.Direction.DOWNRIGHT))
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }

        yield return new WaitForFixedUpdate();

    }

    public bool TestCheck(List<Vector3Int> positionsList, int amountOfChecks, BlockSets.MatchSet.Direction direction)
    {
        Tile tile = DataManager.commonTiles[0];

        BlockPlacer.AddSameBlockMultiplesTimesInATilemap(positionsList.ToArray(), tile, blockMatchChecker.tileMap);

        blockMatchChecker.FullMatchCheck();

        List<BlockSets.MatchSet> matchSetsFound = null;
        switch (direction)
        {
            case BlockSets.MatchSet.Direction.HORIZONTAL:
                matchSetsFound = blockMatchChecker.HorizontalMatchSetsFound;
                break;
            case BlockSets.MatchSet.Direction.VERTICAL:
                matchSetsFound = blockMatchChecker.VerticalMatchSetsFound;
                break;
            case BlockSets.MatchSet.Direction.DOWNLEFT:
                matchSetsFound = blockMatchChecker.LeftDownMatchSetsFound;
                break;
            case BlockSets.MatchSet.Direction.DOWNRIGHT:
                matchSetsFound = blockMatchChecker.RightDownMatchSetsFound;
                break;
            default:
                break;
        }

        if (matchSetsFound.Count != amountOfChecks)
        {
            return false;
        }

        return true;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {


        yield return new ExitPlayMode();
    }
}
