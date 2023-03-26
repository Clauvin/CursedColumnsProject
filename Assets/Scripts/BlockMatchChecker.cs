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
    public Tilemap horizontalCheckTilemap;
    public Tilemap verticalCheckTilemap;
    public Tilemap leftDownCheckTilemap;
    public Tilemap rightDownCheckTilemap;
    
    private List<MatchSet> horizontalMatchSetsFound;
    public List<MatchSet> HorizontalMatchSetsFound
    { get { return horizontalMatchSetsFound; } }

    private List<MatchSet> verticalMatchSetsFound;
    public List<MatchSet> VerticalMatchSetsFound
    { get { return verticalMatchSetsFound; } }

    private List<MatchSet> leftDownMatchSetsFound;
    public List<MatchSet> LeftDownMatchSetsFound
    { get { return leftDownMatchSetsFound; } }

    private List<MatchSet> rightDownMatchSetsFound;
    public List<MatchSet> RightDownMatchSetsFound
    { get {  return rightDownMatchSetsFound; } }

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
        ClearDirectionalTilemaps();
        ResetListOfMatches();
        HorizontalChecks();
        VerticalChecks();
        LeftDownChecks();
        RightDownChecks();
    }

    private void ClearDirectionalTilemaps()
    {
        horizontalCheckTilemap.ClearAllTiles();
        verticalCheckTilemap.ClearAllTiles();
        leftDownCheckTilemap.ClearAllTiles();
        rightDownCheckTilemap.ClearAllTiles();
    }

    public void ResetListOfMatches()
    {
        horizontalMatchSetsFound = new List<MatchSet>();
        verticalMatchSetsFound = new List<MatchSet>();
        leftDownMatchSetsFound = new List<MatchSet>();
        rightDownMatchSetsFound = new List<MatchSet>();
    }

    public void HorizontalChecks()
    {
        DirectionalChecks(horizontalCheckTilemap, horizontalMatchSetsFound, MatchSet.Direction.HORIZONTAL);
    }

    public void VerticalChecks()
    {
        DirectionalChecks(verticalCheckTilemap, verticalMatchSetsFound, MatchSet.Direction.VERTICAL);
    }

    public void LeftDownChecks()
    {
        DirectionalChecks(leftDownCheckTilemap, leftDownMatchSetsFound, MatchSet.Direction.DOWNLEFT);
    }

    public void RightDownChecks()
    {
        DirectionalChecks(rightDownCheckTilemap, rightDownMatchSetsFound, MatchSet.Direction.DOWNRIGHT);
    }

    private void DirectionalChecks(Tilemap directionalTilemap, List<MatchSet> directionalMatchList, MatchSet.Direction direction)
    {
        Vector2 gameSpace = GameManager.dataManager.gameSpace;

        for (int i = GameManager.dataManager.leftLimit + 1; i < GameManager.dataManager.rightLimit; i++)
        {
            for (int j = GameManager.dataManager.lowerLimit + 1; j < GameManager.dataManager.upperLimit; j++)
            {
                Tile tile = tileMap.GetTile<Tile>(new Vector3Int(i, j, 0));
                Tile tileCheck = null;
                try
                {
                    tileCheck = directionalTilemap.GetTile<Tile>(new Vector3Int(i, j, 0));
                }
                catch (NullReferenceException nre)
                {
                    tileCheck = null;
                }
                if (tile != null && tile != DataManager.unpassableTile && tileCheck == null)
                {
                    MatchSet matchSet = new MatchSet(direction);
                    matchSet.AddTile(tile, new Vector3Int(i, j, 0));

                    Vector3Int directionVector = new Vector3Int(0, 0, 0);
                    switch (direction)
                    {
                        case MatchSet.Direction.HORIZONTAL:
                            directionVector = new Vector3Int(-1, 0, 0);
                            break;
                        case MatchSet.Direction.VERTICAL:
                            directionVector = new Vector3Int(0, -1, 0);
                            break;
                        case MatchSet.Direction.DOWNLEFT:
                            directionVector = new Vector3Int(-1, -1, 0);
                            break;
                        case MatchSet.Direction.DOWNRIGHT:
                            directionVector = new Vector3Int(1, -1, 0);
                            break;
                        default:
                            Debug.LogError("BlockMatchChecker - Ok, how we got another direction here .-.");
                            break;
                    }
                    matchSet = DynamicDoubleCheck(matchSet, directionVector);

                    if (matchSet.positions.Count >= matchSize)
                    {
                        directionalMatchList.Add(matchSet);
                    }
                    BlockPlacer.AddSameBlockMultiplesTimesInATilemap(matchSet.GetPositionsArray(), tile, directionalTilemap);
                }
            }
        }
    }

    private MatchSet DynamicDoubleCheck(MatchSet startingMatchSet, Vector3Int direction)
    {
        MatchSet endingMatchSet = DynamicCheck(startingMatchSet, startingMatchSet.positions[0], direction, tileMap);
        endingMatchSet = DynamicCheck(endingMatchSet, startingMatchSet.positions[0], direction*-1, tileMap);
        return endingMatchSet;
    }

    private MatchSet DynamicCheck(MatchSet matchSet, Vector3Int currentPosition, Vector3Int toWhere, Tilemap tileMap)
    {
        Vector3Int positionToCheck = currentPosition + toWhere;
        //Change this part to get the correct tileset instead of the horizontal one
        if (matchSet.tiles[0] == tileMap.GetTile<Tile>(positionToCheck))
        {
            matchSet.AddTile(matchSet.tiles[0], positionToCheck);
            return DynamicCheck(matchSet, positionToCheck, toWhere, tileMap);
        }

        return matchSet;
    }
}
