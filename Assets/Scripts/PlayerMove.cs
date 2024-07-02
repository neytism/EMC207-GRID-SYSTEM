using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : AIMove
{
    
    void Start()
    {
        Init();
        FindSelectableTiles();
    }
    
    void Update()
    {
        if (!isMoving)
        {
            CheckMouse();
        }
        else
        {
            Move();
        }
        
    }

    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile tile = hit.collider.GetComponent<Tile>();
                    if (tile.selectable)
                    {
                        MoveToTileTarget(tile);
                    }
                }
            }
        }
    }
}
