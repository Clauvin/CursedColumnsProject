using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockMatchChecker : MonoBehaviour
{
    public Tilemap tileMap;
    private Tilemap horizontalCheckTilemap;
    private Tilemap verticalCheckTilemap;
    private Tilemap leftDownCheckTilemap;
    private Tilemap rightDownCheckTilemap;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Init(Tilemap tilemap)
    {
        this.tileMap = tilemap;
    }

    public void FullMatchCheck()
    {
        horizontalCheckTilemap = tileMap;
        verticalCheckTilemap = tileMap;
        leftDownCheckTilemap = tileMap;
        rightDownCheckTilemap = tileMap;
        HorizontalCheck();

    }

    private void HorizontalCheck()
    {
        Vector2 gameSpace = GameManager.dataManager.gameSpace;

        for (int i = GameManager.dataManager.leftLimit + 1; i < GameManager.dataManager.rightLimit; i++)
        {
            for (int j = GameManager.dataManager.lowerLimit + 1; j < GameManager.dataManager.upperLimit; j++)
            {
                Debug.Log(i + " " + j + horizontalCheckTilemap.GetTile(new Vector3Int(i, j, 0)));
            }
        }
        
        /*
         * iterate through the playable area
         * for each block, check to the left and the right if there are similar blocks
         * if you found blocks or not from the color of the starting block, remove those from the horizontalCheckTilemap
         * if it found, store the match in the horizontal match list
         */
    }

    private void VerticalCheck()
    {

    }

    private void leftDownCheck()
    {

    }

    private void rightDownCheck()
    {

    }

    public void GetListOfMatches()
    {

    }

    private void ResetListOfMatches()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
