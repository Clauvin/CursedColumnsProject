using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

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
        Debug.Log(GameManager.dataManager);
        GameManager.dataManager.StartManager();

        GameManager.inputManager.StartManager();
        Debug.Log(GameManager.inputManager);
        GameManager.gameTimer.StartManager();
        GameManager.blockManipulator.StartManager();

        GameManager.dataManager.currentBlockSpeedPerSecond = 1;
        GameManager.dataManager.timePassedWithoutBlockMovement = 0;

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

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        GameManager.blockManipulator.tileMap.ClearAllTiles();
        gameManagerPrefab = null;
        gameManager = null;

        yield return new WaitForFixedUpdate();
    }
}
