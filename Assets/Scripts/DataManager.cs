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
        LoadUnpassableTile();
        LoadCommonTiles();
    }

    private void InitErrorStrings()
    {
        errorMessageDidNotLoadUnpassableTile = "Couldn't load unpassable tile.";
        errorMessageDidNotLoadCommonTiles = "Couldn't load common tiles.";
    }

    private bool LoadUnpassableTile()
    {
        AsyncOperationHandle<Tile> opHandle;

#pragma warning disable CS0612 // O tipo ou membro é obsoleto
        opHandle = Addressables.LoadAsset<Tile>("Unpassable Tile");
#pragma warning restore CS0612

        if (opHandle.Status == AsyncOperationStatus.Succeeded)
        {
            unpassableTile = opHandle.Result;
        }
        else if (opHandle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError(errorMessageDidNotLoadUnpassableTile);
            return false;
        }
        else if (opHandle.Status == AsyncOperationStatus.None)
        {
            Debug.LogError(errorMessageDidNotLoadUnpassableTile);
            return false;
        }

        return true;
    }

    private bool LoadCommonTiles()
    {
        AsyncOperationHandle<Tile[]> opHandle;

#pragma warning disable CS0612 // O tipo ou membro ï¿½ obsoleto
        opHandle = Addressables.LoadAsset<Tile[]>("Common Tiles");
#pragma warning restore CS0612

        if (opHandle.Status == AsyncOperationStatus.Succeeded)
        {
            commonTiles = opHandle.Result;
            Debug.Log(commonTiles[0]);
        }
        else if (opHandle.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogError(errorMessageDidNotLoadCommonTiles);
            return false;
        }
        else if (opHandle.Status == AsyncOperationStatus.None)
        {
            Debug.LogError(errorMessageDidNotLoadCommonTiles);
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
