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
    public float currentAmountOfSecondsToApplyBlockGravity;
    public float currentAmountOfSecondsToCheckPlayerInput;
    public float timePassedWithoutHorizontalBlockMovementCheck;
    public float timePassedWithoutPlayerBlockGravityBeingApplied;

    public static int currentScore = 0;
    public const int MATCH3SCOREVALUE = 100;
    public const int MATCH4SCOREVALUE = MATCH3SCOREVALUE * 2;
    public const int MATCH5SCOREVALUE = MATCH3SCOREVALUE * 4;
    public const int MATCH6SCOREVALUE = MATCH3SCOREVALUE * 8;

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
        StartLoadingUnpassableTile();
        StartLoadingCommonTiles();
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
