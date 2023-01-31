using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Tilemaps;

public class DataManager : MonoBehaviour
{
    public Vector2Int blockSetSpawn;
    public Vector2Int gameSpace;
    private Vector2Int startingCoordinates;
    public GameTimer gameTimer;

    public static Tile unpassableTile;
    public static Tile[] commonTiles;
    public static bool isPaused { private set; get; }

    public static string errorMessageDidNotLoadUnpassableTile { get; private set; }
    public static string errorMessageDidNotLoadCommonTiles { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        InitErrorStrings();
        LoadAddressables();
    }

    private bool LoadAddressables()
    {
        AsyncOperationHandle<Tile> opHandle;

        isPaused = false;
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

         return true;
    }

    private void InitErrorStrings()
    {
        errorMessageDidNotLoadUnpassableTile = "Couldn't load unpassable tile.";
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
