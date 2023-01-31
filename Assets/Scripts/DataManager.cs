using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Tilemaps;

public class DataManager : MonoBehaviour
{
    public Vector2Int blockSetSpawn;
    public Vector2Int gameSpace;
    private Vector2Int startingCoordinates;
    public GameTimer gameTimer;

    public static Tile unpassable_tile;
    public static Tile[] common_tiles;
    public static bool isPaused { private set; get; }

    public static string errorMessageDidNotLoadUnpassableTile { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        AsyncOperationHandle<GameObject> opHandle;

        isPaused = false;
        opHandle = Addressables.LoadAsset("Unpassable Tile");

        if (opHandle.Status == AsyncOperationStatus.Succeeded)
        {
            unpassable_tile = opHandle.Result;
        }

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
