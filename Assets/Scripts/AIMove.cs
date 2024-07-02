using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class AIMove : MonoBehaviour
{
    public List<Tile> selectableTiles = new List<Tile>();
    public GameObject[] tiles;
    public int moveRange = 3;
    public float jumpHeight;
    public float moveSpeed;

    protected Stack<Tile> path = new Stack<Tile>();
    protected Tile currentTile;
    protected Tile nextTile;

    public bool isMoving;
    protected Vector3 velocity = new Vector3();
    protected Vector3 heading = new Vector3();

    protected float halfHeight = 0f;

    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        halfHeight = GetComponent<Collider>().bounds.extents.y;
    }

    protected void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.currentPlayerTile = true;
        currentTile.UpdateTileColor();
    }

    protected Tile GetTargetTile(GameObject target)
    {
        Tile tile = null;
        if (Physics.Raycast(target.transform.position, Vector3.down, out RaycastHit hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();
        }

        return tile;
    }

    protected void ComputeAdjacencyList()
    {
        foreach (var obj in tiles)
        {
            Tile tile = obj.GetComponent<Tile>();
            tile.FindNeighbors(jumpHeight);
        }
    }
  
    //BFS
    protected void FindSelectableTiles()
    {
        ComputeAdjacencyList();
        GetCurrentTile();

        //Test();

        Queue<Tile> process = new Queue<Tile>();
        process.Enqueue(currentTile);
        currentTile.visited = true;

        while (process.Count > 0)
        {
            Tile tile = process.Dequeue();
            selectableTiles.Add(tile);
            tile.selectable = true;
            tile.UpdateTileColor();

            if (tile.distance < moveRange)
            {
                foreach (var t in tile.adjacencyList)
                {
                    if (!t.visited)
                    {
                        t.parent = tile;
                        t.visited = true;
                        t.distance = 1 + tile.distance;
                        t.UpdateTileColor();
                        process.Enqueue(t);
                    }
                }
            }
        }
    }

    public void MoveToTileTarget(Tile tile)
    {
        path.Clear();
        tile.target = true;
        tile.UpdateTileColor();
        isMoving = true;

        nextTile = tile;

        // while (nextTile != null)
        // {
        //     path.Push(nextTile);
        //     nextTile = nextTile.parent;
        // }
        
        while (nextTile != null)
        {
            path.Push(nextTile);
            nextTile = nextTile.parent;
        }
    }
    

    public void Move()
    {
        if (path.Count > 0)
        {
            Tile tile = path.Peek();
            Vector3 target = tile.transform.position;

            //calculate the units position on the top of the target tile
            target.y += halfHeight + tile.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                CalculateHeading(target);
                SetHorizontalVelocity();

                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            RemoveSelectableTiles();
            FindSelectableTiles();
            isMoving = false;
        }
    }

    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.currentPlayerTile = false;
            currentTile.UpdateTileColor();
            currentTile = null;
        }
        
        foreach (var tile in selectableTiles)
        {
            tile.ResetValue();
        }
        
        selectableTiles.Clear();
    }

    public void Test()
    {
        foreach (var tile in selectableTiles)
        {
            tile.ResetValue();
        }
        
        selectableTiles.Clear();
    }

    public void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    public void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }


}
