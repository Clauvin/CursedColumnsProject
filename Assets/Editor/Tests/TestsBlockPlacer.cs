using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class TestsBlockPlacer
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
        GameObject grid = null;

        yield return new WaitForFixedUpdate();

        Debug.Log(gameObjects.Length);
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Debug.Log(gameObjects[i].name);
            if (gameObjects[i].name == "Grid")
            {
                grid = gameObjects[i];
            }
        }

        if (grid == null)
        {
            Assert.Fail();
        }

        Assert.Pass();
    }
}
