using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private int gridSizeX;
    [SerializeField] private int gridSizeY;
    
    [SerializeField] private GameObject tilePrefab;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateTiles()
    {
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                Vector3 pos = new Vector3(i, 0, j);
                GameObject obj = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
                obj.tag = "Tile";
            }
        }
    }

    public void ClearTiles()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (var tile in tiles)
        {
            DestroyImmediate(tile);
        }
    }

    public void AssignTileMaterials()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        Material mat = Resources.Load<Material>("Tile-Red");    

        foreach (var tile in tiles)
        {
            tile.GetComponent<Renderer>().material = mat;
        }
    }
}
