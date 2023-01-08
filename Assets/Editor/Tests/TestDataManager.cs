using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestDataManager : MonoBehaviour
{
    DataManager dataManager;

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
            if (gameObjects[i].name == "DataManager")
            {
                dataManager = gameObjects[i].GetComponent<DataManager>();
                break;
            }
        }

        if (dataManager == null)
        {
            Assert.Fail();
        }

        yield return new EnterPlayMode();

    }

    [UnityTest]
    public IEnumerator TestDataManagerStart()
    {
        yield return new WaitForSeconds(2);
        

        if (dataManager.gameTimer.GetTimer() > 0)
        {
            Assert.Pass();
        } 
        else
        {
            Debug.Log(dataManager.gameTimer.GetTimer());
            Assert.Fail();
        }

        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestDataManagerPause()
    {
        yield return new WaitForSeconds(2);

        DataManager.Pause();
        if (DataManager.isPaused)
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
    public IEnumerator TestDataManagerUnpause()
    {
        yield return new WaitForSeconds(2);

        DataManager.Pause();
        DataManager.Unpause();

        if (!DataManager.isPaused)
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
