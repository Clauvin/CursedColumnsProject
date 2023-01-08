using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DataManager : MonoBehaviour
{
    public Vector2Int blockSetSpawn;
    public Vector2Int gameSpace;
    private Vector2Int startingCoordinates;
    public GameTimer gameTimer;

    public Tile unpassable_tile;
    public Tile[] common_tiles;
    public static bool isPaused { private set; get; }

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
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
