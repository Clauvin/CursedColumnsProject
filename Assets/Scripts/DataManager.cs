using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Vector2Int blockSetSpawn;
    public Vector2Int gameSpace;
    public GameTimer gameTimer;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
