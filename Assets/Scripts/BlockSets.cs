using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BlockSets
{
    public class BlockSet
    {
        public List<Tile> tiles { get; protected set; }
        public List<Vector3Int> positions { get; protected set; }

        public BlockSet()
        {

        }

        public BlockSet(Tile tile, List<Vector3Int> positions)
        {
            tiles = new List<Tile>();
            for (int i = 0; i < positions.Count; i++)
            {
                tiles.Add(tile);
            }
            this.tiles = tiles;
            this.positions = positions;
        }

        public BlockSet(List<Tile> tiles, List<Vector3Int> positions)
        {
            this.tiles = tiles;
            this.positions = positions;
        }

        public Tile[] GetTilesArray()
        {
            return tiles.ToArray();
        }

        public Vector3Int[] GetPositionsArray()
        {
            return positions.ToArray();
        }
    }

    public class MatchSet: BlockSet
    {
        public enum Direction
        {
            HORIZONTAL,
            VERTICAL,
            DOWNLEFT,
            DOWNRIGHT
        }

        public Direction matchDirection;

        public MatchSet(Direction matchDirection)
        {
            tiles = new List<Tile>();
            positions = new List<Vector3Int>();
            this.matchDirection = matchDirection;
        }

        public MatchSet(Tile tile, List<Vector3Int> positions, Direction matchDirection)
        {
            tiles = new List<Tile>();
            for (int i = 0; i < positions.Count; i++)
            {
                tiles.Add(tile);
            }
            this.tiles = tiles;
            this.positions = positions;
            this.matchDirection = matchDirection;
        }

        public void AddTile(Tile tile, Vector3Int position)
        {
            tiles.Add(tile);
            positions.Add(position);
        }
    }
}

