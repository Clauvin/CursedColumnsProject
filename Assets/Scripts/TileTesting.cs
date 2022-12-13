using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTesting : MonoBehaviour
{
    public Tile tileTest;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Tilemap>().SetTile(new Vector3Int(-1, 0, 0), tileTest);
        GetComponent<Tilemap>().SetTile(new Vector3Int(0, -1, 0), tileTest);
        GetComponent<Tilemap>().SetTile(new Vector3Int(0, 0, 0), tileTest);
        GetComponent<Tilemap>().SetTile(new Vector3Int(1, 0, 0), tileTest);
        GetComponent<Tilemap>().SetTile(new Vector3Int(0, 1, 0), tileTest);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
