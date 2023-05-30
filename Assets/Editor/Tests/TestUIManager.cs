using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestUIManager : MonoBehaviour
{
    GameObject gameManager;
    GameSceneUIManager uiManager;

    [OneTimeSetUp]
    public void NewTestSetUp()
    {
        SceneManager.LoadScene("GameScene");
    }

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        yield return new WaitForFixedUpdate();

        Scene currentScene = SceneManager.GetActiveScene();
        GameObject[] gameObjects = currentScene.GetRootGameObjects();

        for (int i = 0; i < gameObjects.Length; i++)
        {
            Debug.Log(gameObjects[i].name);
            if (gameObjects[i].name == "GameManager")
            {
                gameManager = gameObjects[i];
                break;
            }
        }

        if (gameManager == null)
        {
            Assert.Fail();
        }

        yield return new EnterPlayMode();
    }

    [UnityTest]
    public IEnumerator TestPause()
    {
        uiManager = gameManager.GetComponent<GameSceneUIManager>();

        GameSceneUIManager.Pause();

        if (!GameSceneUIManager.pauseText.enabled)
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
    public IEnumerator TestUnpause()
    {
        uiManager = gameManager.GetComponent<GameSceneUIManager>();

        GameSceneUIManager.Pause();
        GameSceneUIManager.Unpause();

        if (GameSceneUIManager.pauseText.enabled)
        {
            Assert.Fail();
        }
        else
        {
            Assert.Pass();
        }

        yield return new WaitForFixedUpdate();
    }
}
