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
    InterfaceBlockRemover interfaceBlockRemover;
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

        interfaceBlockPlacer = gridGameObject.GetComponent<BlockPlacer>();
        interfaceBlockRemover = gridGameObject.GetComponent<BlockRemover>();
        tileTest = gridGameObject.GetComponentInChildren<TileTesting>().tileTest;
        tilemap = gridGameObject.GetComponentInChildren<Tilemap>();

        yield return new EnterPlayMode();
    }

    [UnityTest]
    public IEnumerator TestBlockRemoverEraseBlock()
    {
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

    [UnityTest]
    public IEnumerator TestBlockRemoverEraseBlocks()
    {
        Vector3Int[] positions = new Vector3Int[] { new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0) };

        interfaceBlockPlacer.AddSameBlockMultiplesTimes(positions, tileTest);

        if (tilemap.GetTile(new Vector3Int(0, 0, 0)) == tileTest && tilemap.GetTile(new Vector3Int(1, 0, 0)) == tileTest)
        {
            interfaceBlockRemover.EraseBlocks(positions);
            if (tilemap.GetTile(new Vector3Int(0, 0, 0)) == null && tilemap.GetTile(new Vector3Int(1, 0, 0)) == null)
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

    [UnityTest]
    public IEnumerator TestBlockRemoverRemoveBlock()
    {
        Vector3Int position = new Vector3Int(0, 0, 0);

        interfaceBlockPlacer.AddBlock(position, tileTest);

        if (tilemap.GetTile(new Vector3Int(0, 0, 0)) == tileTest)
        {
            interfaceBlockRemover.RemoveBlock(position);
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

    [UnityTest]
    public IEnumerator TestBlockRemoverRemoveBlockFailed()
    {
        bool result = interfaceBlockRemover.RemoveBlock(new Vector3Int(0, 0, 0));

        UnityEngine.TestTools.LogAssert.Expect(LogType.Error, interfaceBlockRemover.GetRemoveBlockNullErrorMessage());

        if (!result)
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
    public IEnumerator TestBlockRemoverRemoveBlocks()
    {
        Vector3Int[] positions = new Vector3Int[] { new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0) };

        interfaceBlockPlacer.AddSameBlockMultiplesTimes(positions, tileTest);

        if (tilemap.GetTile(new Vector3Int(0, 0, 0)) == tileTest && tilemap.GetTile(new Vector3Int(1, 0, 0)) == tileTest)
            {
            interfaceBlockRemover.RemoveBlocks(positions);
            if (tilemap.GetTile(new Vector3Int(0, 0, 0)) == null && tilemap.GetTile(new Vector3Int(1, 0, 0)) == null)
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

    [UnityTest]
    public IEnumerator TestBlockRemoverRemoveBlocksFailed()
    {
        bool result = interfaceBlockRemover.RemoveBlocks(new Vector3Int[] { new Vector3Int(0, 0, 0), new Vector3Int(0, 1, 0) });

        UnityEngine.TestTools.LogAssert.Expect(LogType.Error, interfaceBlockRemover.GetRemoveBlocksNullErrorMessage());

        if (!result)
        {
            Assert.Pass();
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
