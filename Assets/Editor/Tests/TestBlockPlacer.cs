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
    [OneTimeSetUp]
    public void NewTestSetUp()
    {
        SceneManager.LoadScene("TestScene");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator TestsBlockPlacerPlacingABlock()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        GameObject[] gameObjects = currentScene.GetRootGameObjects();
        GameObject gridGameObject = null;

        yield return new WaitForFixedUpdate();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            Debug.Log(gameObjects[i].name);
            if (gameObjects[i].name == "Grid")
            {
                gridGameObject = gameObjects[i];
            }
        }

        if (gridGameObject == null)
        {
            Assert.Fail();
        }

        InterfaceBlockPlacer interfaceBlockPlacer = gridGameObject.GetComponent<InterfaceBlockPlacer>();
        TileBase tileTest = gridGameObject.GetComponentInChildren<TileTesting>().tileTest;
        Tilemap tilemap = gridGameObject.GetComponentInChildren<Tilemap>();

        interfaceBlockPlacer.AddBlock(new Vector3Int(0, 0, 0), tileTest);

        if (tilemap.GetTile(new Vector3Int(0, 0, 0)) == tileTest)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
    }
}
