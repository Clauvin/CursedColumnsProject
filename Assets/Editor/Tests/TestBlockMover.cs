using System.Collections;
using System.Collections.Generic;
using ColumnsInterfaces;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class TestBlockMovement
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
    public IEnumerator TestBlockMovementTeleportBlock()
    {
        InterfaceBlockPlacer interfaceBlockPlacer = gridGameObject.GetComponent<BlockPlacer>();
        InterfaceBlockMover interfaceBlockMover = gridGameObject.GetComponent<BlockMover>();
        Tile tileTest = gridGameObject.GetComponentInChildren<TileTesting>().tileTest;
        Tilemap tilemap = gridGameObject.GetComponentInChildren<Tilemap>();

        interfaceBlockMover.Init(tilemap);

        interfaceBlockPlacer.AddBlock(new Vector3Int(0, 0, 0), tileTest);
        interfaceBlockMover.TeleportBlock(new Vector3Int(0, 0, 0), new Vector3Int(3, 3, 0));

        if (tilemap.GetTile(new Vector3Int(3, 3, 0)) == tileTest)
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
    public IEnumerator TestBlockMovementTeleportBlocks()
    {
        InterfaceBlockPlacer interfaceBlockPlacer = gridGameObject.GetComponent<BlockPlacer>();
        InterfaceBlockMover interfaceBlockMover = gridGameObject.GetComponent<BlockMover>();
        Tile tileTest = gridGameObject.GetComponentInChildren<TileTesting>().tileTest;
        Tilemap tilemap = gridGameObject.GetComponentInChildren<Tilemap>();

        interfaceBlockMover.Init(tilemap);

        Vector3Int[] blocks = new Vector3Int[] { new Vector3Int(0, 0, 0), new Vector3Int(0, 1, 0) };
        Vector3Int[] finalPositions = new Vector3Int[] { new Vector3Int(2,2,0), new Vector3Int(2,3,0) };

        interfaceBlockPlacer.AddBlocks(blocks, new TileBase[] { tileTest, tileTest });

        interfaceBlockMover.TeleportBlocks(blocks, finalPositions);

        if (tilemap.GetTile(new Vector3Int(2, 2, 0)) == tileTest && tilemap.GetTile(new Vector3Int(2, 3, 0)) == tileTest)
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
    public IEnumerator TestBlockMovementMoveBlock()
    {
        InterfaceBlockPlacer interfaceBlockPlacer = gridGameObject.GetComponent<BlockPlacer>();
        InterfaceBlockMover interfaceBlockMover = gridGameObject.GetComponent<BlockMover>();
        Tile tileTest = gridGameObject.GetComponentInChildren<TileTesting>().tileTest;
        Tilemap tilemap = gridGameObject.GetComponentInChildren<Tilemap>();

        interfaceBlockMover.Init(tilemap);

        interfaceBlockPlacer.AddBlock(new Vector3Int(0, 0, 0), tileTest);
        interfaceBlockMover.MoveBlock(new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0), 3);

        if (tilemap.GetTile(new Vector3Int(3, 0, 0)) == tileTest)
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
    public IEnumerator TestBlockMovementMoveBlockButItCollides()
    {
        InterfaceBlockPlacer interfaceBlockPlacer = gridGameObject.GetComponent<BlockPlacer>();
        InterfaceBlockMover interfaceBlockMover = gridGameObject.GetComponent<BlockMover>();
        Tile tileTest = gridGameObject.GetComponentInChildren<TileTesting>().tileTest;
        Tilemap tilemap = gridGameObject.GetComponentInChildren<Tilemap>();

        interfaceBlockMover.Init(tilemap);

        interfaceBlockPlacer.AddBlock(new Vector3Int(0, 0, 0), tileTest);
        interfaceBlockPlacer.AddBlock(new Vector3Int(2, 0, 0), tileTest);
        interfaceBlockMover.MoveBlock(new Vector3Int(0, 0, 0), new Vector3Int(1, 0, 0), 3);

        if (tilemap.GetTile(new Vector3Int(1, 0, 0)) == tileTest)
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
    public IEnumerator TestBlockMovementMoveBlocks()
    {
        InterfaceBlockPlacer interfaceBlockPlacer = gridGameObject.GetComponent<BlockPlacer>();
        InterfaceBlockMover interfaceBlockMover = gridGameObject.GetComponent<BlockMover>();
        Tile tileTest = gridGameObject.GetComponentInChildren<TileTesting>().tileTest;
        Tilemap tilemap = gridGameObject.GetComponentInChildren<Tilemap>();

        interfaceBlockMover.Init(tilemap);

        Vector3Int[] blocks = new Vector3Int[] { new Vector3Int(0, 0, 0), new Vector3Int(0, 1, 0) };
        Vector3Int[] finalPositions = new Vector3Int[] { new Vector3Int(0, 2, 0), new Vector3Int(0, 3, 0) };

        interfaceBlockPlacer.AddSameBlockMultiplesTimes(blocks, tileTest);
        interfaceBlockMover.MoveBlocks(blocks, new Vector3Int(0, 1, 0), 2);

        if ((tilemap.GetTile(new Vector3Int(0, 2, 0)) == tileTest) && (tilemap.GetTile(new Vector3Int(0, 3, 0)) == tileTest))
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
    public IEnumerator TestBlockMovementMoveBlocksButItCollides()
    {
        InterfaceBlockPlacer interfaceBlockPlacer = gridGameObject.GetComponent<BlockPlacer>();
        InterfaceBlockMover interfaceBlockMover = gridGameObject.GetComponent<BlockMover>();
        Tile tileTest = gridGameObject.GetComponentInChildren<TileTesting>().tileTest;
        Tilemap tilemap = gridGameObject.GetComponentInChildren<Tilemap>();

        interfaceBlockMover.Init(tilemap);

        Vector3Int[] blocks = new Vector3Int[] { new Vector3Int(0, 0, 0), new Vector3Int(0, 1, 0) };
        Vector3Int[] finalPositions = new Vector3Int[] { new Vector3Int(0, 2, 0), new Vector3Int(0, 3, 0) };

        interfaceBlockPlacer.AddSameBlockMultiplesTimes(blocks, tileTest);
        interfaceBlockPlacer.AddBlock(new Vector3Int(0, 2, 0), tileTest);
        

        if (!interfaceBlockMover.MoveBlocks(blocks, new Vector3Int(0, 1, 0), 2))
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
    public IEnumerator TestBlockMovementMoveBlockDownwards()
    {
        InterfaceBlockPlacer interfaceBlockPlacer = gridGameObject.GetComponent<BlockPlacer>();
        InterfaceBlockMover interfaceBlockMover = gridGameObject.GetComponent<BlockMover>();
        Tile tileTest = gridGameObject.GetComponentInChildren<TileTesting>().tileTest;
        Tilemap tilemap = gridGameObject.GetComponentInChildren<Tilemap>();

        interfaceBlockMover.Init(tilemap);

        interfaceBlockPlacer.AddBlock(new Vector3Int(0, 3, 0), tileTest);
        interfaceBlockMover.MoveBlockDownwards(new Vector3Int(0, 3, 0), 3);

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
    public IEnumerator TestBlockMovementMoveBlockDownwardsButItCollides()
    {
        InterfaceBlockPlacer interfaceBlockPlacer = gridGameObject.GetComponent<BlockPlacer>();
        InterfaceBlockMover interfaceBlockMover = gridGameObject.GetComponent<BlockMover>();
        Tile tileTest = gridGameObject.GetComponentInChildren<TileTesting>().tileTest;
        Tilemap tilemap = gridGameObject.GetComponentInChildren<Tilemap>();

        interfaceBlockMover.Init(tilemap);

        interfaceBlockPlacer.AddBlock(new Vector3Int(0, 3, 0), tileTest);
        interfaceBlockPlacer.AddBlock(new Vector3Int(0, 0, 0), tileTest);
        interfaceBlockMover.MoveBlockDownwards(new Vector3Int(0, 3, 0), 3);

        if (tilemap.GetTile(new Vector3Int(0, 1, 0)) == tileTest)
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
    public IEnumerator TestBlockMovementMoveBlocksDownwards()
    {
        InterfaceBlockPlacer interfaceBlockPlacer = gridGameObject.GetComponent<BlockPlacer>();
        InterfaceBlockMover interfaceBlockMover = gridGameObject.GetComponent<BlockMover>();
        Tile tileTest = gridGameObject.GetComponentInChildren<TileTesting>().tileTest;
        Tilemap tilemap = gridGameObject.GetComponentInChildren<Tilemap>();

        interfaceBlockMover.Init(tilemap);

        Vector3Int[] blocks = new Vector3Int[] { new Vector3Int(0, 4, 0), new Vector3Int(0, 5, 0), new Vector3Int(0, 6, 0) };
        Vector3Int[] finalPositions = new Vector3Int[] { new Vector3Int(0, 0, 0), new Vector3Int(0, 1, 0), new Vector3Int(0, 2, 0) };

        interfaceBlockPlacer.AddSameBlockMultiplesTimes(blocks, tileTest);

        interfaceBlockMover.MoveBlocksDownwards(blocks, 4);

        
        interfaceBlockMover.MoveBlockDownwards(new Vector3Int(0, 3, 0), 3);

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
}
