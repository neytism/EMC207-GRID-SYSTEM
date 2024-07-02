using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool currentPlayerTile = false;
    public bool target = false;
    public bool selectable = false;
    public bool walkable = true;

    public Color playerColor = Color.blue;
    public Color targetColor = Color.green;
    public Color selectableColor = Color.gray;
    public Color normalColor = Color.white;
    
    private Renderer _rend;
    
    //BFS
    public bool visited = false;
    public Tile parent = null;
    public float distance = 0;

    public List<Tile> adjacencyList = new List<Tile>();

    void Awake()
    {
        _rend = GetComponent<Renderer>();
    }

    void Update()
    {
        
    }

    public void UpdateTileColor()
    {
        if (currentPlayerTile)
        {
            _rend.material.color = playerColor;
        }
        else if (target)
        {
            _rend.material.color = targetColor;
        }
        else if (selectable)
        {
            _rend.material.color = selectableColor;
        }
        else
        {
            _rend.material.color = normalColor;
        }
    }

    public void ResetValue()
    {
        adjacencyList.Clear();
        currentPlayerTile = false;
        target = false;
        selectable = false;

        visited = false;
        distance = 0;
        UpdateTileColor();
    }

    public void FindNeighbors( float jumpHeight)
    {
        ResetValue();
        CheckTiles(Vector3.forward, jumpHeight);
        CheckTiles(Vector3.back, jumpHeight);
        CheckTiles(Vector3.right, jumpHeight);
        CheckTiles(Vector3.left, jumpHeight);
    }

    public void CheckTiles(Vector3 direction, float jumpHeight)
    {
        Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);
        foreach (var col in colliders)
        {
            Tile tile = col.GetComponent<Tile>();
            if (tile != null && tile.walkable)
            {
                RaycastHit hit;
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1))
                {
                    adjacencyList.Add(tile);
                }
            }
        }
    }
}
