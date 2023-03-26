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

        Tile tile = DataManager.commonTiles[0];

        BlockPlacer.AddSameBlockMultiplesTimesInATilemap(positionsList.ToArray(), tile, blockMatchChecker.tileMap);

        blockMatchChecker.FullMatchCheck();

        if (blockMatchChecker.HorizontalMatchSetsFound.Count != 1)
        {
            Assert.Fail();
        } 
        
        Assert.Pass();

        yield return new WaitForFixedUpdate();

    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        yield return new ExitPlayMode();
    }
}
