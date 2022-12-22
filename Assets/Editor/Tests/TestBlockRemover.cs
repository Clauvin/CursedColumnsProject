using System.Collections;
using System.Collections.Generic;
using ColumnsInterfaces;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class TestBlockRemover
{
    GameObject gridGameObject;
    InterfaceBlockPlacer interfaceBlockPlacer;
    Tile tileTest;
    Tilemap tilemap;

    [OneTimeSetUp]
    public void NewTestSetUp()
    {
        SceneManager.LoadScene("TestScene");
    }

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        yield return new WaitForFixedUpdate();

        Scene currentScene = SceneManager.GetActiveScene();
        GameObject[] gameObjects = currentScene.GetRootGameObjects();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name == "Grid")
            {
                gridGameObject = gameObjects[i];
                break;
            }
        }

        if (gridGameObject == null)
        {
            Assert.Fail();
        }

        yield return new EnterPlayMode();
    }

    [UnityTest]
    public IEnumerator TestBlockRemoverEraseBlock()
    {
        InterfaceBlockPlacer interfaceBlockPlacer = gridGameObject.GetComponent<BlockPlacer>();
        InterfaceBlockRemover interfaceBlockRemover = gridGameObject.GetComponent<BlockRemover>();
        Tile tileTest = gridGameObject.GetComponentInChildren<TileTesting>().tileTest;
        Tilemap tilemap = gridGameObject.GetComponentInChildren<Tilemap>();

        interfaceBlockPlacer.AddBlock(new Vector3Int(0, 0, 0), tileTest);
        if (tilemap.GetTile(new Vector3Int(0, 0, 0)) == tileTest)
        {
            interfaceBlockRemover.EraseBlock(new Vector3Int(0, 0, 0));
            if (tilemap.GetTile(new Vector3Int(0, 0, 0)) == null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }
        else
        {
            Assert.Fail();
        }


        yield return new WaitForFixedUpdate();
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        yield return new ExitPlayMode();
    }
}
