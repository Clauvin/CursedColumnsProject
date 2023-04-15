using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Tilemaps;

public class DataManager : MonoBehaviour
{
    public Vector3Int blockSetSpawnPoint;
    public Vector2Int gameSpace;
    public int leftLimit;
    public int rightLimit;
    public int lowerLimit;
    public int upperLimit;
    public float secondsToApplyBlockGravity;
    public float secondsToCheckPlayerInput;
    public float secondsOfDelayAfterAllMatchChecks;
    public float timeWithoutInputCheck;
    public float timeForDelayAfterAllMatchChecks;
    public bool isDelayingAfterAllMatchChecks;
    public float timeWithoutPBlockGravityBeingApplied;

    public static int currentDifficultyLevel = 0;
    public static int currentScore = 0;
    public const int MATCH3SCOREVALUE = 100;
    public const int MATCH4SCOREVALUE = MATCH3SCOREVALUE * 2;
    public const int MATCH5SCOREVALUE = MATCH3SCOREVALUE * 4;
    public const int MATCH6SCOREVALUE = MATCH3SCOREVALUE * 8;

    public List<int> difficultyScoreValues = new List<int>();
    public float[] blockGravityDiffValues = new float[11];
    public float[] playerInputTimeDiffValues = new float[11];
    public float[] delayAfterMatchDiffValues = new float[11];


    public static Tile unpassableTile;
    public static Tile[] commonTiles;
    public static bool isPaused { private set; get; }

    #region Error Messages For Tests
    public static string errorMessageDidNotLoadUnpassableTile { get; private set; }
    public static string errorMessageDidNotLoadCommonTiles { get; private set; }
    public static string errorMessageDidNotLoadAllCommonTiles { get; private set; }

    #endregion Error Messages For Tests

    public void StartManager()
    {
        isPaused = false;
        InitErrorStrings();
        InitDifficultyValues();
        StartLoadingUnpassableTile();
        StartLoadingCommonTiles();
    }

    private void InitDifficultyValues()
    {
        difficultyScoreValues = new List<int>() {
            2000, 4500, 7000, 10000,
            13000, 16000, 20000, 24000,
            30000, 40000
        };

        blockGravityDiffValues = new float[]
        {
            0.8f, 0.75f, 0.67f, 0.6f,
            0.5f, 0.4f, 0.35f, 0.3f,
            0.27f, 0.25f, 0.2f
        };

        playerInputTimeDiffValues = new float[]
        {
            0.05f, 0.048f, 0.046f, 0.042f,
            0.04f, 0.04f, 0.038f, 0.036f,
            0.034f, 0.032f, 0.03f
        };

        delayAfterMatchDiffValues = new float[]
        {
            0.3f, 0.3f, 0.3f, 0.3f,
            0.3f, 0.27f, 0.27f, 0,27f,
            0.27f, 0.27f, 0.25f
        };
    }

    private void InitErrorStrings()
    {
        errorMessageDidNotLoadUnpassableTile = "Couldn't load unpassable tile.";
        errorMessageDidNotLoadCommonTiles = "Couldn't load common tiles.";
        errorMessageDidNotLoadAllCommonTiles = "Not all common tiles were loaded.";
    }

    private bool StartLoadingUnpassableTile()
    {
        AsyncOperationHandle<Tile> opHandle;

#pragma warning disable CS0612 // Obsolete type or member
        opHandle = Addressables.LoadAsset<Tile>("Unpassable Tile");
        unpassableTile = opHandle.WaitForCompletion();
#pragma warning restore CS0612

        if (unpassableTile == null)
        {
            Debug.LogError(errorMessageDidNotLoadUnpassableTile);
            return false;
        }

        return true;
    }

    private bool StartLoadingCommonTiles()
    {
        AsyncOperationHandle<System.Collections.Generic.IList<Tile>> opHandle;

#pragma warning disable CS0612 // Obsolete type or member
        opHandle = Addressables.LoadAssets<Tile>("Common Tiles", null);
#pragma warning restore CS0612

        List<Tile> receiverOfCommonTiles = (List<Tile>)opHandle.WaitForCompletion();
        commonTiles = receiverOfCommonTiles.ToArray();

        if (commonTiles == null)
        {
            Debug.LogError(errorMessageDidNotLoadCommonTiles);
            return false;
        }

        if (commonTiles.Length != 6)
        {
            Debug.LogError(errorMessageDidNotLoadAllCommonTiles);
            return false;
        }

        return true;
    }


    public static void Pause()
    {
        isPaused = true;
    }

    public static void Unpause()
    {
        isPaused = false;
    }
}
