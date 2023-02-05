using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Tilemaps;

public class DataManager : MonoBehaviour
{
    public Vector2Int blockSetSpawn;
    public Vector2Int gameSpace;
    private Vector2Int startingCoordinates;

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
        AsyncOperationHandle<Tile[]> opHandle;

#pragma warning disable CS0612 // Obsolete type or member
        opHandle = Addressables.LoadAsset<Tile[]>("Common Tiles");
#pragma warning restore CS0612
        commonTiles = opHandle.WaitForCompletion();

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


    // Update is called once per frame
    void Update()
    {

    }
}
