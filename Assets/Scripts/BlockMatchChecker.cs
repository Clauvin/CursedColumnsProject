using BlockSets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockMatchChecker : MonoBehaviour
{
    public Tilemap tileMap;
    public int matchSize = 3;
    private Tilemap horizontalCheckTilemap;
    private Tilemap verticalCheckTilemap;
    private Tilemap leftDownCheckTilemap;
    private Tilemap rightDownCheckTilemap;
    private List<MatchSet> horizontalMatchSetsFound;
    private List<MatchSet> verticalMatchSetsFound;
    private List<MatchSet> leftDownMatchSetsFound;
    private List<MatchSet> rightDownMatchSetsFound;

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
        horizontalCheckTilemap = new Tilemap();
        verticalCheckTilemap = new Tilemap();
        leftDownCheckTilemap = new Tilemap();
        rightDownCheckTilemap = new Tilemap();
        horizontalMatchSetsFound = new List<MatchSet>();
        verticalMatchSetsFound = new List<MatchSet>();
        leftDownMatchSetsFound = new List<MatchSet>();
        rightDownMatchSetsFound = new List<MatchSet>();
        HorizontalChecks();
        VerticalChecks();
        LeftDownChecks();
        RightDownChecks();
        Debug.Log(horizontalMatchSetsFound.Count);
        Debug.Log(verticalMatchSetsFound.Count);
        Debug.Log(leftDownMatchSetsFound.Count);
        Debug.Log(rightDownMatchSetsFound.Count);
    }

    private void HorizontalChecks()
    {
        Vector2 gameSpace = GameManager.dataManager.gameSpace;

        for (int i = GameManager.dataManager.leftLimit + 1; i < GameManager.dataManager.rightLimit; i++)
        {
            for (int j = GameManager.dataManager.lowerLimit + 1; j < GameManager.dataManager.upperLimit; j++)
            {
                Tile tile = tileMap.GetTile<Tile>(new Vector3Int(i, j, 0));
                Tile tileCheck;
                try
                {
                    tileCheck = horizontalCheckTilemap.GetTile<Tile>(new Vector3Int(i, j, 0));
                }
                catch(NullReferenceException nre)
                {
                    tileCheck = null;
                }
                if (tile != null && tile != DataManager.unpassableTile && tileCheck == null)
                {
                    MatchSet matchSet = new MatchSet(MatchSet.Direction.HORIZONTAL);
                    matchSet.AddTile(tile, new Vector3Int(i, j, 0));

                    matchSet = HorizontalDynamicDoubleCheck(matchSet);
                    if (matchSet.positions.Count >= matchSize)
                    {
                        horizontalMatchSetsFound.Add(matchSet);
                    }
                    BlockPlacer.AddSameBlockMultiplesTimesInATilemap(matchSet.GetPositionsArray(), tile, horizontalCheckTilemap);
                }
            }
        }
    }

    private MatchSet HorizontalDynamicDoubleCheck(MatchSet startingMatchSet)
    {
        MatchSet endingMatchSet = DynamicCheck(startingMatchSet, startingMatchSet.positions[0], new Vector3Int(-1, 0, 0),
            tileMap);
        endingMatchSet = DynamicCheck(endingMatchSet, startingMatchSet.positions[0], new Vector3Int(1, 0, 0),
            tileMap);
        return endingMatchSet;
    }

    private void VerticalChecks()
    {
        Vector2 gameSpace = GameManager.dataManager.gameSpace;

        for (int i = GameManager.dataManager.leftLimit + 1; i < GameManager.dataManager.rightLimit; i++)
        {
            for (int j = GameManager.dataManager.lowerLimit + 1; j < GameManager.dataManager.upperLimit; j++)
            {
                Tile tile = verticalCheckTilemap.GetTile<Tile>(new Vector3Int(i, j, 0));
                Tile tileCheck;
                try
                {
                    tileCheck = verticalCheckTilemap.GetTile<Tile>(new Vector3Int(i, j, 0));
                }
                catch (NullReferenceException nre)
                {
                    tileCheck = null;
                }
                if (tile != null && tile != DataManager.unpassableTile && tileCheck == null)
                {
                    MatchSet matchSet = new MatchSet(MatchSet.Direction.VERTICAL);
                    matchSet.AddTile(tile, new Vector3Int(i, j, 0));

                    matchSet = VerticalDynamicDoubleCheck(matchSet);
                    if (matchSet.positions.Count >= matchSize)
                    {
                        verticalMatchSetsFound.Add(matchSet);
                    }
                    BlockPlacer.AddSameBlockMultiplesTimesInATilemap(matchSet.GetPositionsArray(), tile, verticalCheckTilemap);
                }
            }
        }
    }

    private MatchSet VerticalDynamicDoubleCheck(MatchSet startingMatchSet)
    {
        MatchSet endingMatchSet = DynamicCheck(startingMatchSet, startingMatchSet.positions[0], new Vector3Int(0, -1, 0),
            tileMap);
        endingMatchSet = DynamicCheck(endingMatchSet, startingMatchSet.positions[0], new Vector3Int(0, 1, 0),
            tileMap);
        return endingMatchSet;
    }

    private void LeftDownChecks()
    {
        Vector2 gameSpace = GameManager.dataManager.gameSpace;

        for (int i = GameManager.dataManager.leftLimit + 1; i < GameManager.dataManager.rightLimit; i++)
        {
            for (int j = GameManager.dataManager.lowerLimit + 1; j < GameManager.dataManager.upperLimit; j++)
            {
                Tile tile = leftDownCheckTilemap.GetTile<Tile>(new Vector3Int(i, j, 0));
                Tile tileCheck;
                try
                {
                    tileCheck = leftDownCheckTilemap.GetTile<Tile>(new Vector3Int(i, j, 0));
                }
                catch (NullReferenceException nre)
                {
                    tileCheck = null;
                }
                if (tile != null && tile != DataManager.unpassableTile && tileCheck == null)
                {
                    MatchSet matchSet = new MatchSet(MatchSet.Direction.DOWNLEFT);
                    matchSet.AddTile(tile, new Vector3Int(i, j, 0));

                    matchSet = LeftDownDynamicDoubleCheck(matchSet);
                    if (matchSet.positions.Count >= matchSize)
                    {
                        leftDownMatchSetsFound.Add(matchSet);
                    }
                    BlockPlacer.AddSameBlockMultiplesTimesInATilemap(matchSet.GetPositionsArray(), tile, leftDownCheckTilemap);
                }
            }
        }
    }

    private MatchSet LeftDownDynamicDoubleCheck(MatchSet startingMatchSet)
    {
        MatchSet endingMatchSet = DynamicCheck(startingMatchSet, startingMatchSet.positions[0], new Vector3Int(-1, -1, 0),
            tileMap);
        endingMatchSet = DynamicCheck(endingMatchSet, startingMatchSet.positions[0], new Vector3Int(1, 1, 0),
            tileMap);
        return endingMatchSet;
    }

    private void RightDownChecks()
    {
        Vector2 gameSpace = GameManager.dataManager.gameSpace;

        for (int i = GameManager.dataManager.leftLimit + 1; i < GameManager.dataManager.rightLimit; i++)
        {
            for (int j = GameManager.dataManager.lowerLimit + 1; j < GameManager.dataManager.upperLimit; j++)
            {
                Tile tile = rightDownCheckTilemap.GetTile<Tile>(new Vector3Int(i, j, 0));
                Tile tileCheck;
                try
                {
                    tileCheck = rightDownCheckTilemap.GetTile<Tile>(new Vector3Int(i, j, 0));
                }
                catch (NullReferenceException nre)
                {
                    tileCheck = null;
                }
                if (tile != null && tile != DataManager.unpassableTile && tileCheck == null)
                {
                    MatchSet matchSet = new MatchSet(MatchSet.Direction.DOWNRIGHT);
                    matchSet.AddTile(tile, new Vector3Int(i, j, 0));

                    matchSet = RightDownDynamicDoubleCheck(matchSet);
                    if (matchSet.positions.Count >= matchSize)
                    {
                        rightDownMatchSetsFound.Add(matchSet);
                    }
                    BlockPlacer.AddSameBlockMultiplesTimesInATilemap(matchSet.GetPositionsArray(), tile, rightDownCheckTilemap);
                }
            }
        }
    }

    private MatchSet RightDownDynamicDoubleCheck(MatchSet startingMatchSet)
    {
        MatchSet endingMatchSet = DynamicCheck(startingMatchSet, startingMatchSet.positions[0], new Vector3Int(1, -1, 0),
            tileMap);
        endingMatchSet = DynamicCheck(endingMatchSet, startingMatchSet.positions[0], new Vector3Int(-1, 1, 0),
            tileMap);
        return endingMatchSet;
    }

    private MatchSet DynamicCheck(MatchSet matchSet, Vector3Int currentPosition, Vector3Int toWhere, Tilemap tileMap)
    {
        Vector3Int positionToCheck = currentPosition + toWhere;
        //Change this part to get the correct tileset instead of the horizontal one
        if (matchSet.tiles[0] == tileMap.GetTile<Tile>(positionToCheck))
        {
            matchSet.AddTile(matchSet.tiles[0], currentPosition);
            return DynamicCheck(matchSet, positionToCheck, toWhere, tileMap);
        }

        return matchSet;
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
