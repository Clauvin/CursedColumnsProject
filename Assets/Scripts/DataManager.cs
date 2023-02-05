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
    }

    private void StartLoadingUnpassableTile()
    {
#pragma warning disable CS0612 // O tipo ou membro é obsoleto
        Addressables.LoadAsset<Tile>("Unpassable Tile").Completed += OnUnpassableTileLoadDone;
#pragma warning restore CS0612
    }

    private void OnUnpassableTileLoadDone(AsyncOperationHandle<Tile> opHandle)
    {
        if (opHandle.Status == AsyncOperationStatus.Succeeded)
        {
            unpassableTile = opHandle.Result;
            Debug.Log("Ufs");
        }
        else if (opHandle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError(errorMessageDidNotLoadUnpassableTile);
        }
        else if (opHandle.Status == AsyncOperationStatus.None)
        {
            Debug.LogError(errorMessageDidNotLoadUnpassableTile);
        }
    }

    private void StartLoadingCommonTiles()
    {
#pragma warning disable CS0612 // O tipo ou membro ï¿½ obsoleto
        Addressables.LoadAsset<Tile[]>("Common Tiles").Completed += OnCommonTilesLoadDone;
#pragma warning restore CS0612
    }

    private void OnCommonTilesLoadDone(AsyncOperationHandle<Tile[]> opHandle)
    {
        if (opHandle.Status == AsyncOperationStatus.Succeeded)
        {
            commonTiles = opHandle.Result;
            Debug.Log(commonTiles[0]);
        }
        else if (opHandle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError(errorMessageDidNotLoadCommonTiles);
        }
        else if (opHandle.Status == AsyncOperationStatus.None)
        {
            Debug.LogError(errorMessageDidNotLoadCommonTiles);
        }
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
