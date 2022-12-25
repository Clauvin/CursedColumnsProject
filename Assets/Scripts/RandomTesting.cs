using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Tilemap>().SetTile(new Vector3Int(0, 3, 0), GetComponent<TileTesting>().tileTest);
        GetComponent<Tilemap>().SetTile(new Vector3Int(0, 0, 0), GetComponent<TileTesting>().tileTest);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
