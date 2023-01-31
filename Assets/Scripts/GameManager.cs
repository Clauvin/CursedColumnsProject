using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static DataManager dataManager;
    public static PlayerInteractions inputManager;
    public static GameTimer gameTimer;
    public static BlockManipulator blockManipulator;

    void Awake()
    {
        dataManager = GetComponent<DataManager>();
        inputManager = GetComponent<PlayerInteractions>();
        gameTimer = GetComponent<GameTimer>();
        blockManipulator = GetComponent<BlockManipulator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        dataManager.StartManager();
        inputManager.StartManager();
        gameTimer.StartManager();
        blockManipulator.StartManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
