using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.TestTools;
using UnityEngine.Tilemaps;

public class TestDataManager : MonoBehaviour
{
    DataManager dataManager;
 
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        dataManager = new DataManager();

        yield return new EnterPlayMode();

    }

    [UnityTest]
    public IEnumerator TestDataManagerInitErrorStrings()
    {
        dataManager.StartManager();

        if ((DataManager.errorMessageDidNotLoadUnpassableTile == "Couldn't load unpassable tile.") &&
                (DataManager.errorMessageDidNotLoadCommonTiles == "Couldn't load common tiles.") &&
                (DataManager.errorMessageDidNotLoadAllCommonTiles == "Not all common tiles were loaded."))
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
    public IEnumerator TestDataManagerInitUnpassableTile()
    {
        dataManager.StartManager();

        AsyncOperationHandle<Tile> opHandle;
        Tile unpassableTile;

#pragma warning disable CS0612 // Obsolete type or member
        opHandle = Addressables.LoadAsset<Tile>("Unpassable Tile");
        unpassableTile = opHandle.WaitForCompletion();
#pragma warning restore CS0612

        if (DataManager.unpassableTile == unpassableTile)
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
    public IEnumerator TestDataManagerInitCommonTiles()
    {
        dataManager.StartManager();

        AsyncOperationHandle<System.Collections.Generic.IList<Tile>> opHandle;
        Tile[] commonTiles;

#pragma warning disable CS0612 // Obsolete type or member
        opHandle = Addressables.LoadAssets<Tile>("Common Tiles", null);
#pragma warning restore CS0612

        List<Tile> receiverOfCommonTiles = (List<Tile>)opHandle.WaitForCompletion();
        commonTiles = receiverOfCommonTiles.ToArray();

        for (int i = 0; i < DataManager.commonTiles.Length; i++)
        {
            if (DataManager.commonTiles[i] != commonTiles[i])
            {
                Assert.Fail();
            }
        }

        Assert.Pass();

        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestDifficultyScoreValuesInitialization()
    {
        dataManager.StartManager();

        for (int i = 0; i < dataManager.difficultyScoreValues.Count; i++)
        {
            if (dataManager.difficultyScoreValues[i] == 0)
            {
                Assert.Fail();
            }
        }

        Assert.Pass();

        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestBlockGravityDiffValuesInitialization()
    {
        dataManager.StartManager();

        for (int i = 0; i < dataManager.blockGravityDiffValues.Length; i++)
        {
            if (dataManager.blockGravityDiffValues[i] == 0)
            {
                Assert.Fail();
            }
        }

        Assert.Pass();

        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestPlayerInputTimeDiffValuesInitialization()
    {
        dataManager.StartManager();

        for (int i = 0; i < dataManager.playerInputTimeDiffValues.Length; i++)
        {
            if (dataManager.playerInputTimeDiffValues[i] == 0)
            {
                Assert.Fail();
            }
        }

        Assert.Pass();

        yield return new WaitForFixedUpdate();
    }

    [UnityTest]
    public IEnumerator TestDelayAfterMatchDiffValuesInitialization()
    {
        dataManager.StartManager();

        for (int i = 0; i < dataManager.delayAfterMatchDiffValues.Length; i++)
        {
            if (dataManager.delayAfterMatchDiffValues[i] == 0)
            {
                Assert.Fail();
            }
        }

        Assert.Pass();

        yield return new WaitForFixedUpdate();
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        dataManager = null;

        yield return new ExitPlayMode();
    }


}
