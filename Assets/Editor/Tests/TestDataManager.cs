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
         Assert.Pass();

         yield return new WaitForFixedUpdate();
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        yield return new ExitPlayMode();
    }


}
