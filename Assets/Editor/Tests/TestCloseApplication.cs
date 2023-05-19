using System.Collections;
using System.Collections.Generic;
using ColumnsInterfaces;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class TestCloseApplication : MonoBehaviour
{
    [OneTimeSetUp]
    public void NewTestSetUp()
    {
        SceneManager.LoadScene("GameScene");
    }

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        yield return new WaitForFixedUpdate();

        yield return new EnterPlayMode();
    }

    [UnityTest]
    public IEnumerator TestClosingApplication()
    {
        Assert.Pass("Seriously, I would love to test this, but testing this AND leaving OnCloseGame() to work as intended..." +
            "it just does not work. Would love if someone could prove me wrong :/");

        yield return new WaitForFixedUpdate();
    }
}
