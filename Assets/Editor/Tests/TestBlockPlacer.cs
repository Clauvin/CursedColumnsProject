using System;
using System.Collections;
using ColumnsInterfaces;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

[TestFixture]
public class TestBlockPlacer
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

        interfaceBlockPlacer = gridGameObject.GetComponent<BlockPlacer>();
        tileTest = gridGameObject.GetComponentInChildren<TileTesting>().tileTest;
        tilemap = gridGameObject.GetComponentInChildren<Tilemap>();

        yield return new EnterPlayMode();
    }

    [UnityTest]
    public IEnumerator TestsBlockPlacerPlacingABlock()
    {
        interfaceBlockPlacer.AddBlock(new Vector3Int(0, 0, 0), tileTest);

        if (tilemap.GetTile(new Vector3Int(0, 0, 0)) == tileTest)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
        Assert.Fail();
        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestsBlockPlacerPlacingANullBlock()
    {
        bool result = interfaceBlockPlacer.AddBlock(new Vector3Int(0, 0, 0), null);
        UnityEngine.TestTools.LogAssert.Expect(LogType.Error, interfaceBlockPlacer.GetAddBlockTileErrorMessage());

        if (result == false)
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
    public IEnumerator TestsBlockPlacerPlacingBlocks()
    {
        Vector3Int[] positions = new [] { new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0) };
        Tile[] tiles = new[] { tileTest, tileTest, tileTest };

        interfaceBlockPlacer.AddBlocks(positions, tiles);

        var succeeded = true;

        for (int i = 0; i < positions.Length; i++)
        {
            if (tilemap.GetTile(positions[i]) != tiles[i])
            {
                succeeded = false;
                break;
            }
        }

        if (succeeded == true)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
        Assert.Fail();
        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestsBlockPlacerPlacingZeroBlocks()
    {
        Vector3Int[] positions = Array.Empty<Vector3Int>();
        Tile[] tiles = new[] { tileTest, tileTest, tileTest };

        bool result = interfaceBlockPlacer.AddBlocks(positions, tiles);
        UnityEngine.TestTools.LogAssert.Expect(LogType.Error, interfaceBlockPlacer.GetAddBlocksZeroPositionsErrorMessage());

        if (result == false)
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
    public IEnumerator TestsBlockPlacerPlacingZeroTiles()
    {
        Vector3Int[] positions = new[] { new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0) };
        Tile[] tiles = Array.Empty<Tile>();

        bool result = interfaceBlockPlacer.AddBlocks(positions, tiles);
        UnityEngine.TestTools.LogAssert.Expect(LogType.Error, interfaceBlockPlacer.GetAddBlocksZeroTilesErrorMessage());

        if (result == false)
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
    public IEnumerator TestsBlockPlacerWithMismatchBetweenPositionsAndTilesAmounts()
    {
        Vector3Int[] positions = new[] { new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0) };
        Tile[] tiles = new[] { tileTest, tileTest };

        bool result = interfaceBlockPlacer.AddBlocks(positions, tiles);
        UnityEngine.TestTools.LogAssert.Expect(LogType.Error, interfaceBlockPlacer.GetAddBlocksPositionsAmountDifferentFromTilesErrorMessage());

        if (result == false)
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
    public IEnumerator TestsBlockPlacerAddingSameBlockMultiplesTimes()
    {
        Vector3Int[] positions = new[] { new Vector3Int(-1, 0, 0), new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0) };

        interfaceBlockPlacer.AddSameBlockMultiplesTimes(positions, tileTest);

        var succeeded = true;

        for (int i = 0; i < positions.Length; i++)
        {
            if (tilemap.GetTile(positions[i]) != tileTest)
            {
                succeeded = false;
                break;
            }
        }

        if (succeeded == true)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
        Assert.Fail();
        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestsBlockPlacerAddingSameBlockMultiplesTimesWithZeroPositions()
    {
        Vector3Int[] positions = Array.Empty<Vector3Int>();

        bool result = interfaceBlockPlacer.AddSameBlockMultiplesTimes(positions, tileTest);
        UnityEngine.TestTools.LogAssert.Expect(LogType.Error, interfaceBlockPlacer.GetAddSameBlockMultiplesTimesZeroPositionsErrorMessage());

        if (result == false)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
        Assert.Fail();
        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestsBlockPlacerAddingSameBlockMultiplesTimesWithOnePosition()
    {
        Vector3Int[] positions = new[] { new Vector3Int(0, 1, 0) };

        bool result = interfaceBlockPlacer.AddSameBlockMultiplesTimes(positions, tileTest);
        UnityEngine.TestTools.LogAssert.Expect(LogType.Warning, interfaceBlockPlacer.GetAddSameBlockMultiplesTimesOnePositionWarningMessage());

        if (result == true)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
        Assert.Fail();
        yield return new WaitForFixedUpdate();
    }


    [UnityTearDown]
    public IEnumerator TearDown()
    {
        yield return new ExitPlayMode();
    }
}
