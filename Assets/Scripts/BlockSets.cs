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
}

